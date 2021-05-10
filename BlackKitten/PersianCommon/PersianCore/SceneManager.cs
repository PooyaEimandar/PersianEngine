﻿/*
 * Copyright (c) BaziPardaz.Co Ltd. All rights reserved.
 * 
 * File Name        : SceneManager.cs
 * File Description : The manager of scene
 * Generated by     : Pooya Eimandar
 * Last modified by : Pooya Eimandar on 5/15/2013
 * Comment          : If you want to add object as inner object, just add it to IsAllowToCreateInstance method,
 *                    also add it to the GetValueFromElement method. Finally make sure add it to the ObjectTypeID dictionary
 */

using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;

namespace PersianCore
{
    public static class SceneManager
    {
        #region Fields & Properties

        static Dictionary<sbyte, Type> ObjectTypeID;

        #endregion

        #region Constructor

        static SceneManager()
        {
            ObjectTypeID = new Dictionary<sbyte, Type>()
            {
                { 0, typeof(Cameras.FreeCamera) },
                { 1, typeof(Cameras.ChaseCamera) },
                { 2, typeof(Meshes.Mesh) },
                { 3, typeof(PersianCore.Graphics.Particles.ParticleSystem) },
                { 4, typeof(PersianCore.Graphics.Lights.Light) },
                { 5, typeof(PersianCore.Framework.Animation.CutScene) },
                { 6, typeof(MixerInfo)},
                { 7, typeof(PersianCore.Graphics.PostProcessing.GodRay)},
                { 8, typeof(PersianCore.Graphics.Environment.Sky)},
            };
        }

        #endregion

        #region Save Scene
        
        /// <summary>
        /// Saving Scene data, on returning null , send error to screen
        /// </summary>
        /// <param name="path"></param>
        /// <param name="coreFrameWork"></param>
        /// <param name="EditorVersion"></param>
        /// <returns></returns>
        public static XDocument ProcessSaving(string path, CoreFrameWork coreFrameWork, ref string HResult)
        {
            string Extension = System.IO.Path.GetExtension(path);
            bool Generate4Engine = Extension.Equals(".PEC");

            #region Set Comment

            string Comment = string.Empty;
            if (Extension.Equals(".PES"))
            {
                Comment = "Persian Editor Save Scene";
            }
            else if (Extension.Equals(".PEP"))
            {
                Comment = "Persian Editor Save Project";
            }
            else
            {
                //is PEC
                Comment = "Persian Engine configuration";
            }

            #endregion

            var xDoc = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                    new XComment(Comment),
                    new XProcessingInstruction("Processing_Instruction",
                        "CreatedDate = '" + System.DateTime.Now.ToShortDateString() + "'" + " " +
                        "CreatedTime = '" + System.DateTime.Now.ToShortTimeString() + "'"));

            var xElement = new XElement("Sources",
                new XAttribute("Title", System.IO.Path.GetFileNameWithoutExtension(path)),
                new XAttribute("Type", Extension),
                new XAttribute("Version", Persian.EVersion));

            #region Save Camera

            if (!Generate4Engine)
            {
                //Save for loading later inside editor
                HResult = SaveObjectProperties(Persian.Camera.freeCamera, ref xElement);
                if (HResult != null) return null;
            }
            else
            {
                //Save config for engine
                if (Persian.Camera.GetCurrentCamera() == CameraManager.ActiveCamera.Chase)
                {
                    HResult = SaveObjectProperties(Persian.Camera.chaseCamera, ref xElement);
                }
                else
                {
                    HResult = SaveObjectProperties(Persian.Camera.freeCamera, ref xElement);
                }
                if (HResult != null) return null;
            }

            #endregion
            
            #region Save particles

            foreach (var obj in coreFrameWork.ObjectsManager.ParticlesManager.ParticleSystems)
            {
                HResult = SaveObjectProperties(obj, ref xElement);
                if (HResult != null) return null;
            }

            #endregion

            #region Save lights

            foreach (var obj in coreFrameWork.RenderManager.Lights)
            {
                HResult = SaveObjectProperties(obj, ref xElement);
                if (HResult != null) return null;
            }

            #endregion

            #region Save PostProcess

            if (coreFrameWork.Post.GodRay != null)
            {
                HResult = SaveObjectProperties(coreFrameWork.Post.GodRay, ref xElement);
                if (HResult != null) return null;
            }

            #endregion

            #region Save CutScenes

            foreach (var obj in Persian.CutScenes)
            {
                HResult = SaveObjectProperties(obj, ref xElement);
                if (HResult != null) return null;
            }

            #endregion

            #region Save Environment

            HResult = SaveObjectProperties(coreFrameWork.ObjectsManager.EnvManager.Sky, ref xElement);
            if (HResult != null) return null;

            #endregion

            #region Save meshes

            foreach (var obj in coreFrameWork.ObjectsManager.Meshes)
            {
                HResult = SaveObjectProperties(obj, ref xElement);
                if (HResult != null) return null;
            }

            #endregion

            xDoc.Add(xElement);
            xDoc.Save(path);

            return xDoc;
        }

        public static string SaveObjectProperties(object Obj, ref XElement XDoc)
        {
            string HResult = null;
            try
            {
                var pInfo = Obj.GetType().GetProperties();

                var xElement = new XElement(Obj.GetType().ToString());

                sbyte key = GetKeyOfObject(Obj, ref HResult);
                if (HResult != null) return HResult;

                xElement.Add(new XAttribute("ObjectTypeID", key));
                foreach (var info in pInfo)
                {
                    if (info.GetCustomAttributes(typeof(DoNotSave), true).Length > 0)
                    {
                        //On Properties with DoNotSave attributes, continue anyway
                        continue;
                    }
                    var value = info.GetValue(Obj, new object[] { });
                    if (value != null)
                    {
                        var el = new XElement(info.Name);
                        SaveUpponItsType(value, ref el);
                        xElement.Add(el);
                    }
                    else
                    {
                        xElement.Add(new XElement(info.Name, "NULL"));
                    }
                }
                XDoc.Add(xElement);
            }
            catch (Exception ex)
            {
                HResult = ex.ToStandardString();
            }
            return HResult;
        }

        private static sbyte GetKeyOfObject(object Obj, ref string HResult)
        {
            //Rerurn if object type is not available in ObjectTypeID
            var key = SByte.MaxValue;
            var type = Obj.GetType();


            if (!(type == typeof(Cameras.FreeCamera) || 
                type == typeof(Cameras.ChaseCamera) ||
                type == typeof(Meshes.Mesh) ||
                type == typeof(PersianCore.Graphics.Particles.ParticleSystem) ||
                type == typeof(PersianCore.Framework.Animation.CutScene) ||
                type == typeof(PersianCore.Graphics.Lights.Light) ||
                type == typeof(MixerInfo) ||
                type == typeof(PersianCore.Graphics.PostProcessing.GodRay) || 
                type == typeof(PersianCore.Graphics.Environment.Sky)))
            {
                HResult = "Could not find type of object";
                return key;
            }
            
            //If ObjectTypeID is avaiable, then get the key of it
            try
            {
                key = ObjectTypeID.KeysFromValue<sbyte, System.Type>(Obj.GetType()).First();
                if (key == SByte.MaxValue)
                {
                    HResult = "Could not find type of object";
                }
            }
            catch (Exception ex)
            {
                //key must stay on MaxValue
                HResult = ex.ToStandardString();
            }
            return key;
        }

        private static void SaveUpponItsType(object value, ref XElement el)
        {
            string HResult = null;

            //First check for Array, Generics and Enums
            if (Check4Array(value, ref el)) return;
            if (Check4Generic(value, ref el)) return;
            if (CheckEnum(value, ref el)) return;

            //Then check for allowable class
            var key = GetKeyOfObject(value, ref HResult);
            if (HResult == null)
            {
                var result = SaveObjectProperties(value, ref el);
                return;
            }
            el.Add(new XElement(value.GetType().Name, value));
        }

        private static bool Check4Generic(object value, ref XElement el)
        {
            if (value.GetType().IsGenericType)
            {
                #region Save Generic
                string name = value.GetType().Name.Replace("`", "");
                var InnerEl = new XElement(name);
                if (value.GetType().GetGenericTypeDefinition().Name == "Dictionary`2")
                {
                    #region If is Dictionary

                    IDictionary IDic = (IDictionary)value;
                    foreach (object k in IDic.Keys)
                    {
                        var item = new XElement("Items");
                        SaveUpponItsType(k, ref item);
                        SaveUpponItsType(IDic[k], ref item);
                        InnerEl.Add(item);
                    }

                    #endregion
                }
                else if (value.GetType().GetGenericTypeDefinition().Name == "List`1")
                {
                    #region If is List
                    IList iList = (System.Collections.IList)value;
                    foreach (object obj in iList)
                    {
                        var item = new XElement("Items");
                        SaveUpponItsType(obj, ref item);
                        InnerEl.Add(item);
                    }
                    #endregion
                }
                el.Add(InnerEl);
                #endregion

                return true;
            }
            return false;
        }

        /// <summary>
        /// Save Array
        /// </summary>
        /// <param name="value"></param>
        /// <param name="el"></param>
        /// <returns></returns>
        private static bool Check4Array(object value, ref XElement el)
        {
            if (value.GetType().IsArray)
            {
                //[ , ] can not add az name parameters
                string name = value.GetType().Name.Replace("[", "_");
                name = name.Replace("]", "_");

                var InnerEl = new XElement(name);
                foreach (object ob in (Array)value)
                {
                    InnerEl.Add(new XElement(ob.GetType().Name, ob));
                }
                el.Add(InnerEl);
                return true;
            }
            return false;
        }

        private static bool CheckEnum(object value, ref XElement el)
        {
            if (value.GetType().IsEnum)
            {
                var InnerEl = new XElement("Enum");
                InnerEl.Add(new XElement(value.GetType().Name, value));
                el.Add(InnerEl);
                return true;
            }
            return false;
        }

        #endregion

        #region Open Scene

        public static string Open(System.IO.Stream stream, ref CoreFrameWork coreFrameWork)
        {
            XDocument XDoc = null;
            try
            {
                XDoc = XDocument.Load(stream);
                stream.Dispose();
            }
            catch (Exception ex)
            {
                Logger.WriteLine(ex.ToStandardString());
            }
            return Openning(XDoc, false, ref coreFrameWork);
        }

        public static string Open(string path, ref CoreFrameWork coreFrameWork)
        {
            return ProcessOpenning(path, false, ref coreFrameWork);
        }

        public static string Merge(System.IO.Stream stream, ref CoreFrameWork coreFrameWork)
        {
            XDocument XDoc = null;
            try
            {
                XDoc = XDocument.Load(stream);
                stream.Dispose();
            }
            catch (Exception ex)
            {
                Logger.WriteLine(ex.ToStandardString());
            }
            return Openning(XDoc, true, ref coreFrameWork);
        }

        public static string Merge(string path, ref CoreFrameWork coreFrameWork)
        {
            return ProcessOpenning(path, true, ref coreFrameWork);
        }

        public static string ProcessOpenning(string path, bool isMerging, ref CoreFrameWork coreFrameWork)
        {
            var XDoc = XDocument.Load(path);
            return Openning(XDoc, isMerging, ref coreFrameWork);
        }

        private static string Openning(XDocument XDoc, bool isMerging, ref CoreFrameWork coreFrameWork)
        {
            string HResult = null;
            if (!isMerging)
            {
                New(ref coreFrameWork);
            }

            string SourceInfo = "Source file has been loaded successfully with following info : \r\n";

            #region First Get Information Attributes

            foreach (var attr in XDoc.Elements().First().Attributes())
            {
                SourceInfo += attr.Name.LocalName + " : " + attr.Value.ToString() + "\r\n";
            }

            #endregion

            #region Now get all elements of first node("Source") then assign properties and import to editor

            //Used for importing data
            object Obj = null;
#if DEBUG
            var stop = System.Diagnostics.Stopwatch.StartNew();
#endif
            var localCoreFrameWork = coreFrameWork;

            var elements = XDoc.Elements().First().Elements();
            var count = elements.Count();

            //Todo : More parallel
            ParallelThreading.Parallel.Do(delegate
            {
                for (int i = 0; i < count; ++i)
                {
                    HResult = CreateObject(elements.ElementAt(i), ref Obj);
                    if (HResult != null)
                    {
                        Logger.WriteError(HResult);
                        //return HResult;
                    }
                    if (Obj != null)
                    {
                        HResult = Import(Obj, ref localCoreFrameWork);
                        if (HResult != null)
                        {
                            Logger.WriteError(HResult);
                            // return HResult;
                        }
                    }
                }
            });

            coreFrameWork = localCoreFrameWork;
#if DEBUG
            stop.Stop();
#endif
            #endregion

            #region Time for apply Bindings

            //Bind Camera
            if (Persian.Camera.GuidBind != Guid.Empty)
            {
                var Q = from q in coreFrameWork.ObjectsManager.Meshes where q.GUID == Persian.Camera.GuidBind select q;
                var BindedMesh = Q.FirstOrDefault();
                if (BindedMesh != null)
                {
                    if (Persian.Camera.GetCurrentCamera() == CameraManager.ActiveCamera.Chase)
                    {
                        //Chase camera must be behind mesh
                        BindedMesh.Rotation = new Vector3(0, MathHelper.Pi, 0);
                    }
                    Persian.Camera.BindTo = BindedMesh;
                }
            }

            //Binding Physically...this can be done in editor for editing bindings
            foreach (var mesh in coreFrameWork.ObjectsManager.Meshes)
            {
                if (mesh.BindingGuid != Guid.Empty)
                {
                    var Q = from q in coreFrameWork.ObjectsManager.Meshes where q.GUID == mesh.BindingGuid select q;
                    var BindedMesh = Q.FirstOrDefault();
                    if (BindedMesh != null)
                    {
                        mesh.BindTo = BindedMesh;
                    }
                }
            }

            #endregion

            coreFrameWork.ObjectsManager.StartLoading();

            return HResult;
        }

        private static string Import(object Obj, ref CoreFrameWork coreFrameWork)
        {
            var GDevice = Persian.GDevice;

            var type = Obj.GetType();
            string HResult = null;
            if (type == typeof(Meshes.Mesh))
            {
                try
                {
                    HResult = (string)type.GetMethod("Load").Invoke(Obj, new object[] { null });
                    if (HResult != null)
                    {
                        return HResult;
                    }
                    coreFrameWork.ObjectsManager.AddMesh(Obj as Meshes.Mesh);
                }
                catch (OutOfMemoryException)
                {
                    HResult = "Error OUT OF MEMORY";
                }
                catch (Exception ex)
                {
                    HResult = string.Format(
                        "Error on loading scene file. (from SceneManager) : e.what() : ", ex.ToStandardString());
                }
            }
            else if (type == typeof(PersianCore.Graphics.Particles.ParticleSystem))
            {
                HResult = coreFrameWork.ObjectsManager.ParticlesManager.AddParticle(Obj as PersianCore.Graphics.Particles.ParticleSystem);
            }
            else if (type == typeof(PersianCore.Graphics.Lights.Light))
            {
                coreFrameWork.AddLight(Obj as PersianCore.Graphics.Lights.Light);
            }
            else if (type == typeof(PersianCore.Framework.Animation.CutScene))
            {
                var cs = Obj as PersianCore.Framework.Animation.CutScene;
                cs.LoadAnimatedModelsFromGuids(ref coreFrameWork);
                Persian.CutScenes.Add(cs);
                Persian.ActiveCutScene = 0;
            }
            else if (type == typeof(PersianCore.Graphics.Environment.Sky))
            {
                //TODO : Add texturePath and Scale
                var sky = Obj as PersianCore.Graphics.Environment.Sky;
                coreFrameWork.ObjectsManager.EnvManager.Sky.Scale = sky.Scale;
            }
            return HResult;
        }

        private static string CreateObject(XElement xelement, ref object Obj)
        {
            string HResult = null;
            string Name = xelement.Name.LocalName;
            Obj = null;
            try
            {
                #region Get attribute for setting type

                var xAttribute = xelement.Attribute("ObjectTypeID");
                if (xAttribute == null || xAttribute.Value.Equals("-1"))
                {
                    return "Invalid ObjectType ID";
                }

                Type typeOfObj;
                ObjectTypeID.TryGetValue(Convert.ToSByte(xAttribute.Value), out typeOfObj);

                #endregion

                IsAllowToCreateInstance(typeOfObj, ref Obj);

                #region Find all properties and set to specific object

                var nodes = from q in xelement.Elements().AsParallel().WithDegreeOfParallelism(
                                System.Environment.ProcessorCount)
                            where q != null
                            select q;

                var Nodes = nodes.ToList();
                Nodes.Sort(delegate(XElement p1, XElement p2)
                {
                    return string.Compare(p1.Name.ToString() , p2.Name.ToString());
                });
                dynamic value = null;
                foreach (var element in Nodes)
                {
                    bool? HR = GetValueFromElement(element, ref value , ref HResult);
                    if (HR == null)
                    {
                        //is null so continue
                        continue;
                    }
                    else if (HR == true)
                    {
                        #region Set value of property

                        if (Obj != null)
                        {
                            //Set properties of created object
                            SetProperty(Obj, element.Name.LocalName, value);
                        }
                        else
                        {
                            //Set properties uppon it's type
                            SetProperty(typeOfObj, element.Name.LocalName, value);
                        }

                        #endregion
                    }
                    else
                    {
                        return HResult;
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                HResult = String.Format("Could not create object named {0} because : {1}", Name, ex.ToStandardString());
            }
            return HResult;
        }

        private static void IsAllowToCreateInstance(Type typeOfObj, ref object Obj)
        {
            if (typeOfObj.Equals(typeof(Meshes.Mesh)) ||
                typeOfObj.Equals(typeof(PersianCore.Graphics.Particles.ParticleSystem)) ||
                typeOfObj.Equals(typeof(PersianCore.Graphics.Lights.Light)) || 
                typeOfObj.Equals(typeof(PersianCore.Framework.Animation.CutScene)) ||
                typeOfObj.Equals(typeof(PersianCore.Graphics.Environment.Sky)) ||
                typeOfObj.Equals(typeof(MixerInfo)))
            {
                Obj = Activator.CreateInstance(typeOfObj, new object[] { });
            }
            #region Else Just Set Active Camera
            else if (typeOfObj.Equals(typeof(Cameras.FreeCamera)))
            {
                Persian.Camera.SetActiveCamera(CameraManager.ActiveCamera.Free);
            }
            else if (typeOfObj.Equals(typeof(Cameras.ChaseCamera)))
            {
                Persian.Camera.SetActiveCamera(CameraManager.ActiveCamera.Chase);
            }
            #endregion
        }

        private static void SetProperty(Type type, string PropertyName, object Value)
        {
            PropertyInfo pInfo;

            if (type.Equals(typeof(Cameras.FreeCamera)))
            {
                if (PropertyName == "BindingGuid")
                {
                    pInfo = Persian.Camera.chaseCamera.GetType().GetProperty(PropertyName);
                    if (pInfo.CanWrite)
                    {
                        pInfo.SetValue(Persian.Camera.chaseCamera, Value, new object[] { });
                    }
                }
                else
                {
                    pInfo = Persian.Camera.freeCamera.GetType().GetProperty(PropertyName);
                    if (pInfo.CanWrite)
                    {
                        pInfo.SetValue(Persian.Camera.freeCamera, Value, new object[] { });
                    }
                }
            }
            else if (type.Equals(typeof(Cameras.ChaseCamera)))
            {
                pInfo = Persian.Camera.chaseCamera.GetType().GetProperty(PropertyName);
                if (pInfo.CanWrite)
                {
                    pInfo.SetValue(Persian.Camera.chaseCamera, Value, new object[] { });
                }
            }
        }

        private static void SetProperty(object Obj, string PropertyName, object Value)
        {
            if (Obj.GetType().GetProperty(PropertyName).CanWrite)
            {
                Obj.GetType().GetProperty(PropertyName).SetValue(Obj, Value, new object[] { });
            }
        }

        private static bool? GetValueFromElement(XElement element, ref dynamic value, ref string HResult)
        {
            int Lenght = 0;
            IEnumerable<XElement> ArrayX;
            IEnumerable<XElement> elements = element.Elements();
            if (elements.Count() == 0)
            {
                //value is null so continue
                return null; ;
            }

            var firstElement = elements.ElementAt(0);
            var lName = firstElement.Name.ToString().ToLower();
            switch (lName)
            {
                default:
                    return false;
                case "mixerinfo":
                    CreateObject(firstElement, ref value);
                    break;
                case "timespan":
                    value = XmlConvert.ToTimeSpan(element.Value.ToString());
                    break;
                case "vector4":
                    var vec4 = new Vector4();
                    value = vec4.StringToVector(element.Value);
                    break;
                case "vector3":
                    var vec3 = new Vector3();
                    value = vec3.StringToVector(element.Value);
                    break;
                case "vector2":
                    var vec = new Vector2();
                    value = vec.StringToVector(element.Value);
                    break;
                case "int32":
                    value = Convert.ToInt32(element.Value);
                    break;
                case "byte":
                    value = Convert.ToByte(element.Value);
                    break;
                case "string":
                    value = element.Value.ToString();
                    break;
                case "single":
                    value = Convert.ToSingle(element.Value);
                    break;
                case "boolean":
                    value = Convert.ToBoolean(element.Value);
                    break;
                case "enum":
                    #region Enum
                    var firstEL = element.Elements().ElementAt(0).Elements().ElementAt(0);
                    string enumName = firstEL.Name.ToString();
                    var _enum = ENUMS.GetEnum(enumName);
                    if (_enum == null)
                    {
                        HResult = string.Format("Can not recognize enum named {0} ", enumName);
                        return false;
                    }
                    value = ENUMS.StringToEnum(_enum.GetType(), firstEL.Value.ToString());
                    if (value == null)
                    {
                        HResult = string.Format("Can not recognize value of enum named {0} ", enumName);
                        return false;
                    }
                    #endregion
                    break;
                case "guid":
                    #region GUID
                    Guid guid;
                    if (Guid.TryParse(element.Value, out guid))
                    {
                        value = guid;
                    }
                    else
                    {
                        HResult = "Could not parse Guid";
                        return false;
                    }
                    #endregion
                    break;
                case "string__":
                    #region Array of string
                    ArrayX = element.Elements().First().Elements();
                    Lenght = ArrayX.Count();
                    value = new string[Lenght];
                    for (int i = 0; i < Lenght; i++)
                    {
                        value[i] = ArrayX.ElementAt(i).Value.ToString();
                    }
                    #endregion
                    break;
                case "single__":
                    #region Array of float
                    ArrayX = element.Elements().First().Elements();
                    Lenght = ArrayX.Count();
                    value = new float[Lenght];
                    for (int i = 0; i < Lenght; i++)
                    {
                        value[i] = Convert.ToSingle(ArrayX.ElementAt(i).Value);
                    }
                    #endregion
                    break;
                case "int32__":
                    #region Array of int
                    ArrayX = element.Elements().First().Elements();
                    Lenght = ArrayX.Count();
                    value = new int[Lenght];
                    for (int i = 0; i < Lenght; i++)
                    {
                        value[i] = Convert.ToInt32(ArrayX.ElementAt(i).Value);
                    }
                    #endregion
                    break;
                case "double__":
                    #region Array of double
                    ArrayX = element.Elements().First().Elements();
                    Lenght = ArrayX.Count();
                    value = new int[Lenght];
                    for (int i = 0; i < Lenght; i++)
                    {
                        value[i] = Convert.ToDouble(ArrayX.ElementAt(i).Value);
                    }
                    #endregion
                    break;
                case "list1":
                    #region Read List

                    ArrayX = element.Elements().First().Elements();
                    value = null;
                    dynamic innerVal = null;
                    for (int i = 0; i < ArrayX.Count(); i++)
                    {
                        bool? HR = GetValueFromElement(ArrayX.ElementAt(i), ref innerVal, ref HResult);
                        if (HResult != null) return false;
                        if (HR != true)
                        {
                            HResult = "Unknown type of dictionary";
                            return false;
                        }
                        if (value == null)
                        {
                            Type[] typeArgs = { (innerVal as object).GetType() };
                            Type makeme = typeof(List<>).MakeGenericType(typeArgs);
                            value = Activator.CreateInstance(makeme);
                        }
                        value.Add(innerVal);
                    }

                    #endregion
                    break;
                case "dictionary2":
                    #region Read Dictionary

                    ArrayX = element.Elements().ElementAt(0).Elements();
                    value = null;
                    dynamic innerKey = null, innerValue = null;
                    for (int i = 0; i < ArrayX.Count(); i++)
                    {
                        #region GetKey
                        var Item = new XElement("Item", ArrayX.ElementAt(i).Elements().ElementAt(0));
                        bool? HR0 = GetValueFromElement(Item, ref innerKey, ref HResult);
                        if (HResult != null) return false;
                        #endregion

                        #region Get Value
                        Item = new XElement("Item", ArrayX.ElementAt(i).Elements().ElementAt(1));
                        bool? HR1 = GetValueFromElement(Item, ref innerValue, ref HResult);
                        if (HResult != null) return false;
                        #endregion

                        if (HR0 != true || HR1 != true)
                        {
                            HResult = "Unknown type of dictionary";
                            return false;
                        }

                        if (value == null)
                        {
                            Type[] typeArgs = { (innerKey as object).GetType(), (innerValue as object).GetType() };
                            Type makeme = typeof(Dictionary<,>).MakeGenericType(typeArgs);
                            value = Activator.CreateInstance(makeme);
                        }
                        value.Add(innerKey, innerValue);
                    }

                    #endregion
                    break;
            }
            return true;
        }

        #endregion

        #region New Scene

        public static void New(ref CoreFrameWork coreFrameWork)
        {
            coreFrameWork.RemoveAll();
        }

        #endregion

        #region Play Scene

        /// <summary>
        /// Start palying scene, if type of game is third person shooter and no character is assign to chase camera, then add default character
        /// </summary>
        /// <param name="coreFrameWork"></param>
        /// <param name="isFPS"></param>
        /// <returns></returns>
        public static string Play(CoreFrameWork coreFrameWork)
        {
            string HResult = null;

            #region Create config file

            string path = string.Format(@"{0}\..\{1}", Persian.EngineContentDir, CoreShared.ConfigName);
            XDocument xDoc = ProcessSaving(path, coreFrameWork, ref HResult);
            if (HResult != null) return HResult;

            #endregion

            if (xDoc != null)
            {
                #region Start Scene Process
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.FileName = Persian.EngineContentDir + @"\..\PersianEngine.exe";
                string ArgsCode = GenerateCodecArgs(xDoc);
                if (ArgsCode == null)
                {
                    HResult = "Can not create args code";
                    return HResult;
                }
                process.StartInfo.Arguments = ArgsCode;
                process.Start();
                while (!process.HasExited)
                {
                    System.Threading.Thread.Sleep(1000);
                }
                process.Dispose();
                #endregion
            }

            #region Check 4 Removing Character from Scene


            #endregion

            return null;
        }

        private static string GenerateCodecArgs(XDocument xDoc)
        {
            //string Code = null, SpaceCode = null, CotationCode = null, DoubleCotationcode = null;
            //Coding.Data.TryGetValue(" ", out SpaceCode);
            //Coding.Data.TryGetValue("\"", out CotationCode);
            //Coding.Data.TryGetValue("'", out DoubleCotationcode);
            //if (SpaceCode != null && CotationCode != null && DoubleCotationcode != null)
            //{
            //    Code = xDoc.ToString();
            //    //Replace space code with specific code
            //    Code = Code.Replace(" ", SpaceCode);
            //    Code = Code.Replace("\"", CotationCode);
            //    Code = Code.Replace("'", DoubleCotationcode);
            //}
            //return Code;
            return "Call From Editor";
        }

        #endregion
    }
}
