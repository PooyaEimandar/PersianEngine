/*
 * Copyright (c) BaziPardaz.Co Ltd. All rights reserved.
 * 
 * File Name        : CoreFrameWork.cs
 * File Description : The core of the engine
 * Generated by     : Pooya Eimandar
 * Last modified by : Pooya Eimandar on 7/29/2013
 * Comment          : 
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PersianCore.Graphics;
using PersianCore.Graphics.Lights;
using PersianCore.Graphics.PostProcessing;
using PersianCore.Physic;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersianCore
{
    public class CoreFrameWork : Node
    {
        #region Fields & Properties

        enum LoadingState { NOP, StartLoading, Loading, Loaded }
        LoadingState loadingState;
        List<Action<GraphicsDevice>> RenderInvokes;
        SpriteBatch spriteBatch;
        SpriteFont Font;
        PhysicManager physicManager;

        PostProcessor post;
        public PostProcessor Post
        {
            get
            {
                return this.post;
            }
        }

        public Texture2D ResultBuffer;

        RenderManager renderManager;

        public RenderManager RenderManager
        {
            get
            {
                return this.renderManager;
            }
        }

        ObjectsManager objectsManager;
        public ObjectsManager ObjectsManager
        {
            get
            {
                return this.objectsManager;
            }
        }

        #endregion

        #region Constructor/Destructor

        public CoreFrameWork(bool SlavedByEngine, Action<GraphicsDevice> RenderEditor)
        {
            Persian.RunningEngine = SlavedByEngine;
            InitializeFields(RenderEditor);
        }

        #endregion

        #region Initialize
        
        private void InitializeFields(Action<GraphicsDevice> RenderEditor)
        {
            //var uri = string.Format("https://graph.facebook.com/oauth/authorize?client_id={0}&redirect_uri=http://www.facebook.com/connect/login_success.html&scope={1}&type=user_agent&display=popup", "512328042192813", "publish_stream,read_stream,publish_actions");
            this.loadingState = LoadingState.NOP;
            this.spriteBatch = new SpriteBatch(Persian.GDevice);
            this.renderManager = new RenderManager();
            this.objectsManager = new ObjectsManager();
            this.physicManager = new PhysicManager();

            this.RenderInvokes = new List<Action<GraphicsDevice>>();
            if (RenderEditor != null)
            {
                this.RenderInvokes.Add(RenderEditor);
            }
            this.RenderInvokes.Add(new Action<GraphicsDevice>(this.DrawOpaques));

            if (!Persian.RunningEngine)
            {
                this.renderManager.EnableDebug = true;
            }
        }

        #endregion

        #region Load

        public void Load(ContentManager Content,
            string EngineAssemblyName = "PersianEngine",
            string GameScreensNameSpaces = "GameScreens.")
        {
            CoreShared.LoadShared();
            Persian.Load();

            this.objectsManager.Initialize();
            //Load Fonts
            this.Font = Fonts.GetFont("Times14");
            // Load render
            this.renderManager.Load(Content);
            //Load post process
            this.post = new PostProcessor();
            this.post.Load(Content);
            ScreenManager.Load(this, EngineAssemblyName, GameScreensNameSpaces);
            //Add default light
            var light = new Light(
                LightType.Directional,
                Vector3.Zero,
                new Vector3(1.0f, 10.0f, 10.0f),
                Color.LightYellow,
                1.0f);
            this.AddLight(light);

            //TODO : Export animated facial skin
            #region Export animated facial skin

            //var mesh = new Meshes.Mesh(@"N\MasterChefFace3.xnb", Vector3.Zero, Vector3.Zero, Vector3.One);
            //mesh.Load(null);
            //this.objectsManager.AddMesh(mesh);

            //mesh.AddToMixer(new MixerInfo()
            //{
            //    Name = "DemoLev1",
            //    AnimationTracks = new List<string>() { "DemoLev1_Body", "DemoLev1_Facial" },
            //    AnimationTracksInverese = new List<bool>() { false, false },
            //    DeltaBetweenFrames = new List<long>() { 0, 4428 },
            //    MixerType = 0,
            //    BoneInherits = new Dictionary<int, int>() 
            //    { 
            //        { 43, 1 }, { 37, 1 }, { 44, 1 }, 
            //        { 38, 1 }, { 48, 1 }, { 47, 1 }, 
            //        { 46, 1 }, { 42, 1 }, { 40, 1 },
            //        { 39, 1 }, { 45, 1 }, { 41, 1 },
            //        { 49, 1 }, 
            //    },
            //});
            //mesh.AnimData.UseMixer = true;


            //mesh = new Meshes.Mesh(@"Models\Characters\Chef.xnb", Vector3.Zero, Vector3.Zero, Vector3.One)
            //{
            //    DiffuseMaps = new Dictionary<string, List<string>>() 
            //    {
            //        { "Model_polymsh_obj" , new List<string>() { "Chef.dds" } }
            //    },
            //};
            //mesh.Load(null);
            //mesh.LoadAnimationClips(@"CutScenes\Level1\Chef\Body", @"CutScenes\Level1\Chef\Face");
            //this.objectsManager.AddMesh(mesh);

            //mesh.AddToMixer(new MixerInfo()
            //{
            //    Name = "DemoLev1",
            //    AnimationTracks = new List<string>() { "DemoLev1_Body", "DemoLev1_Facial" },
            //    AnimationTracksInverese = new List<bool>() { false, false },
            //    DeltaBetweenFrames = new List<long>() { 0, 3120 },
            //    MixerType = 0,
            //    BoneInherits = new Dictionary<int, int>() 
            //    { 
            //        { 43, 1 }, { 37, 1 }, { 44, 1 }, 
            //        { 38, 1 }, { 48, 1 }, { 47, 1 }, 
            //        { 46, 1 }, { 42, 1 }, { 40, 1 },
            //        { 39, 1 }, { 45, 1 }, { 41, 1 },
            //        { 49, 1 }, 
            //    },
            //});
            //mesh.AnimData.UseMixer = true;

            this.objectsManager.StartLoading();

            #endregion
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get the light
        /// </summary>
        /// <param name="LightName"></param>
        /// <returns></returns>
        public Light GetLight(string LightName)
        {
            Light light = null;
            foreach (var item in renderManager.Lights)
            {
                if (item.Name == LightName)
                {
                    light = item;
                    break;
                }
            }
            return light;
        }

        public void AddLight(Light light)
        {
            this.renderManager.AddLight(light);
            if (light.LightType == Graphics.Lights.LightType.Directional)
            {
                LightManager.MainDirLight = light;
            }
        }

        public void RemoveAll()
        {
            this.objectsManager.RemoveAll();
            this.renderManager.RemoveAll();
            Persian.Camera.Reset();
            PhysicManager.physicWorld.Clear();
            Persian.CutScenes.Clear();
        }

        public void SelectAllMeshes(bool isInverse)
        {
            if (!Persian.RunningEngine)
            {
                foreach (var mesh in this.objectsManager.Meshes)
                {
                    if (mesh.Visibility || (!mesh.Visibility && mesh.ShowBoundings))
                    {
                        if (!isInverse)
                        {
                            this.objectsManager.SelectMeshe(mesh);
                        }
                        else
                        {
                            if (mesh.SelectionColor == 0)
                            {
                                this.objectsManager.SelectMeshe(mesh);
                            }
                            else
                            {
                                mesh.SelectionColor = 0;
                            }
                        }

                        if (mesh.BonesVisibility)
                        {
                            foreach (var bone in mesh.BonesData)
                            {
                                if (!isInverse && !bone.IsSelected)
                                {
                                    bone.IsSelected = true;
                                }
                                else
                                {
                                    bone.IsSelected = !bone.IsSelected;
                                }
                            }
                        }
                    }
                }
            }
        }

        public void VisibleAll()
        {
            this.objectsManager.VisibleAll();
        }

        #endregion

        #region Events

        public override void OnPreparingDevice(PreparingDeviceSettingsEventArgs e)
        {
            ScreenManager.OnPreparingDevice(e);
            this.post.OnPreparingDevice(e);
            this.renderManager.OnPreparingDevice(e);
            this.objectsManager.OnPreparingDevice(e);
            base.OnPreparingDevice(e);
        }

        #endregion

        #region Update

        public void Update(GameTime gameTime)
        {
            if (this.isDisposed) return;
           
            if (Persian.RuntimeLoadings.Count != 0)
            {
                switch (this.loadingState)
                {
                    case LoadingState.NOP:
                        this.loadingState = LoadingState.StartLoading;
                        break;
                    case LoadingState.Loading:
                        //Show loading on Screen Manager
                        ScreenManager.ShowLoadingScreen();
                        //Runtime loadings are waiting, start then on parallel task
                        #region Start them on task
                        var task = Task.Factory.StartNew(() =>
                        {
                            while (Persian.RuntimeLoadings.Count != 0)
                            {
                                var loadingFunc = Persian.RuntimeLoadings.Dequeue();
                                var HR = loadingFunc.Invoke(null);
                                if (HR != null)
                                {
                                    Logger.WriteLine(HR);
                                }
                                if (Persian.RuntimeLoadings.Count == 0)
                                {
                                    this.loadingState = LoadingState.Loaded;
                                }
                            }
                        });
                        #endregion
                        break;
                }
            }
            else
            {
                #region Do the same

                if (Persian.RunningEngine || ScreenManager.isShowingLoading)
                {
                    #region Update ScreenManager

                    if (ScreenManager.updateState != PersianSettings.UpdateState.Core)
                    {
                        ScreenManager.Update();
                        if (ScreenManager.updateState == PersianSettings.UpdateState.Screen)
                        {
                            return;
                        }
                    }

                    #endregion
                }

                this.objectsManager.Update();

                //Update CutScene
                if (Persian.ActiveCutScene != -1 && Persian.CutScenes.Count != 0)
                {
                    var Cut = Persian.CutScenes[Persian.ActiveCutScene];
                    if (Cut.Active)
                    {
                        Cut.Update();
                    }
                }

                if (Persian.RunningEngine)
                {
                    UpdateEngine();
                }

                // DisplayResolutions.GetAvailableList();
                KinectLibrary.KinectGetsure.GetState();

                #endregion
            }
        }

        private void UpdateEngine()
        {
            this.physicManager.Update();
        }

        #endregion

        #region Draw

        private void DrawOpaques(GraphicsDevice GDevice)
        {
            this.physicManager.Draw();
        }

        public void Draw()
        {
            if (this.isDisposed) return;

            if (ScreenManager.updateState == PersianSettings.UpdateState.Screen)
            {
                ScreenManager.Draw();

                if (this.loadingState == LoadingState.Loaded)
                {
                    this.loadingState = LoadingState.NOP;
                    //Caching GPU
                    RenderManager.CachingGPU = true;
                    {
                        Render();
                        RenderManager.CachingGPU = false;
                    }
                    //Hide loading screen
                    ScreenManager.HideLoadingScreen();
                }
                return;
            }

            Render();
            
            if (this.loadingState == LoadingState.StartLoading)
            {
                //Make sure one frame has been captured
                this.loadingState = LoadingState.Loading;
            }
            else
            {
                this.renderManager.ShowDebug(spriteBatch);
            }

            if (ScreenManager.updateState != PersianSettings.UpdateState.Core)
            {
                ScreenManager.Draw();
            }
        }

        private void Render()
        {
            this.ResultBuffer = null;
            this.renderManager.Draw(
                this.spriteBatch,
                this.objectsManager,
                this.post,
                RenderInvokes,
                ref this.ResultBuffer);
        }

        #endregion

        #region Dispose

        protected override void Dispose(bool disposing)
        {
            if (!disposing || isDisposed) return;

            this.renderManager.Dispose();
            this.physicManager.Dispose();
            this.objectsManager.Dispose();
            SystemMemory.SafeDispose(this.spriteBatch);
            SystemMemory.SafeDispose(this.ResultBuffer);

            base.Dispose(disposing);
        }

        #endregion
    }
}