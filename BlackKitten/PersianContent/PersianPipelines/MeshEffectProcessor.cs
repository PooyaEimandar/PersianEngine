//using System;
//using System.IO;
//using Microsoft.Xna.Framework.Content.Pipeline;
//using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
//using Microsoft.Xna.Framework.Content.Pipeline.Processors;

//namespace Pipelines
//{
//    [ContentProcessor(DisplayName = "MeshEffectProcessor")]
//    internal class MeshEffectProcessor : MaterialProcessor
//    {
//        public override MaterialContent Process(MaterialContent input, ContentProcessorContext context)
//        {
//            EffectMaterialContent effectMaterialContent = new EffectMaterialContent();

//            string ContentDir = Path.GetFullPath(Directory.GetCurrentDirectory() + @"..\..\..\PersianEditor\PersianEditor\bin\x86\Debug\Content\EditorBrowser\");
//            string effectPath = string.Empty;

//            #region Compile Effect

//            //Load effect
//            effectPath = @"Shaders\MaterialEffect.fx";

//            string Path4Load = Path.Combine(ContentDir, effectPath);
//            effectMaterialContent.Effect = new ExternalReference<EffectContent>(Path4Load);
//            //effectMaterialContent.CompiledEffect = context.BuildAsset<EffectContent, CompiledEffectContent>(effectMaterialContent.Effect, typeof(FXProcessor).Name);

//            #endregion

//            BasicMaterialContent basicMaterial = (BasicMaterialContent)input;
//            string PrePath = Path.GetDirectoryName(input.Identity.SourceFilename);

//            if (basicMaterial.Texture != null)
//            {
//                #region Assign Textures to Effect Parameters

//                string TextureName = Path.GetFileNameWithoutExtension(basicMaterial.Texture.Filename);

//                //Add texture
//                effectMaterialContent.Textures.Add("DiffuseMap", basicMaterial.Texture);
//                ExternalReference<TextureContent> Normal = null;

//                #region Find Normal

//                Search4Map(Path.Combine(ContentDir, @"Textures\NormalMaps\"), TextureName, MapType.Normal, ref Normal);
//                if (Normal == null)
//                {
//                    throw new Exception("Could Not Find NormalMap [Message from MeshEffectProcessor]");
//                }

//                #endregion

//                #region Find Specular

//                ExternalReference<TextureContent> Specular = null;
//                Search4Map(Path.Combine(ContentDir, @"Textures\SpecularMaps\"), TextureName, MapType.Specular, ref Specular);
//                if (Specular == null)
//                {
//                    throw new Exception("Could Not Find SpecularMap [Message from MeshEffectProcessor]");
//                }

//                #endregion

//                #region Find Emissive

//                ExternalReference<TextureContent> Emissive = null;
//                Search4Map(Path.Combine(ContentDir, @"Textures\"), TextureName, MapType.Emissive, ref Emissive);
//                if (Emissive == null)
//                {
//                    throw new Exception("Could Not Find EmissiveMap [Message from MeshEffectProcessor]");
//                }

//                #endregion

//                #region Set Parameters to Effect

//                effectMaterialContent.Textures.Add("NormalMap", Normal);
//                effectMaterialContent.Textures.Add("SpecularMap", Specular);
//                effectMaterialContent.Textures.Add("EmissiveMap", Emissive);
//                effectMaterialContent.Effect.OpaqueData["NormalMap"] = Normal;
//                effectMaterialContent.Effect.OpaqueData["SpecularMap"] = Specular;
//                effectMaterialContent.Effect.OpaqueData["EmissiveMap"] = Emissive;

//                #endregion

//                #endregion
//            }

//            // Chain to the base material processor.
//            return base.Process(effectMaterialContent, context);
//        }

//        //protected override ExternalReference<CompiledEffectContent> BuildEffect(ExternalReference<EffectContent> effect, ContentProcessorContext context)
//        //{
//        //    OpaqueDataDictionary processorParameters = new OpaqueDataDictionary();
//        //    if (context.Parameters.ContainsKey("Defines"))
//        //    {
//        //        if (context.Parameters.ContainsKey("Defines"))
//        //        {
//        //            if (context.Parameters["Defines"].ToString() != string.Empty)
//        //            {
//        //                processorParameters.Add("Defines", context.Parameters["Defines"]);
//        //            }
//        //        }
//        //    }
//        //    if (context.Parameters.ContainsKey("DebugMode"))
//        //    {
//        //        processorParameters.Add("DebugMode", context.Parameters["DebugMode"]);
//        //    }
//        //    return context.BuildAsset<EffectContent, CompiledEffectContent>(effect, typeof(FXProcessor).Name, processorParameters, "EffectImporter", effect.Name);
//        //}

//        //private void Search4Map(string PrePath, string Name, MapType mapType, ref ExternalReference<TextureContent> texture)
//        //{
//        //    #region Set PostPath

//        //    string PostPath = string.Empty;
//        //    if (mapType == MapType.Normal)
//        //    {
//        //        PostPath = "_NORM";
//        //    }
//        //    else if (mapType == MapType.Specular)
//        //    {
//        //        PostPath = "_SPEC";
//        //    }
//        //    else
//        //    {
//        //        PostPath = "_EMISSIVE";
//        //    }

//        //    #endregion

//        //    #region Check for Specific Map (supported format is *.jpg | *.png | *.dds |*.tag)

//        //    string FinalPath = Path.GetFullPath(PrePath + "\\" + Name + PostPath + ".jpg");
//        //    if (File.Exists(FinalPath))
//        //    {
//        //        texture = new ExternalReference<TextureContent>(FinalPath);
//        //        return;
//        //    }
//        //    FinalPath = Path.GetFullPath(PrePath + "\\" + Name + PostPath + ".png");
//        //    if (File.Exists(FinalPath))
//        //    {
//        //        texture = new ExternalReference<TextureContent>(FinalPath);
//        //        return;
//        //    }
//        //    FinalPath = Path.GetFullPath(PrePath + "\\" + Name + PostPath + ".dds");
//        //    if (File.Exists(FinalPath))
//        //    {
//        //        texture = new ExternalReference<TextureContent>(FinalPath);
//        //        return;
//        //    }
//        //    FinalPath = Path.GetFullPath(PrePath + "Default" + PostPath + ".tga");
//        //    if (File.Exists(FinalPath))
//        //    {
//        //        texture = new ExternalReference<TextureContent>(FinalPath);
//        //        return;
//        //    }

//        //    #endregion

//        //    #region Check for Default Normal (supported format is *.jpg | *.png | *.dds | *.tag)

//        //    FinalPath = Path.GetFullPath(PrePath + "Default" + PostPath + ".jpg");
//        //    if (File.Exists(FinalPath))
//        //    {
//        //        texture = new ExternalReference<TextureContent>(FinalPath);
//        //        return;
//        //    }
//        //    FinalPath = Path.GetFullPath(PrePath + "Default" + PostPath + ".png");
//        //    if (File.Exists(FinalPath))
//        //    {
//        //        texture = new ExternalReference<TextureContent>(FinalPath);
//        //        return;
//        //    }
//        //    FinalPath = Path.GetFullPath(PrePath + "Default" + PostPath + ".dds");
//        //    if (File.Exists(FinalPath))
//        //    {
//        //        texture = new ExternalReference<TextureContent>(FinalPath);
//        //        return;
//        //    }
//        //    FinalPath = Path.GetFullPath(PrePath + "Default" + PostPath + ".tga");
//        //    if (File.Exists(FinalPath))
//        //    {
//        //        texture = new ExternalReference<TextureContent>(FinalPath);
//        //        return;
//        //    }

//        //    #endregion
//        //}
//    }
//}
