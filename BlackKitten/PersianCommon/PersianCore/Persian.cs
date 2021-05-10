﻿/*
 * Copyright (c) BaziPardaz.Co Ltd. All rights reserved.
 * 
 * File Name        : Persian.cs
 * File Description : This class responsible to store shared data
 * Generated by     : Pooya Eimandar
 * Last modified by : Pooya Eimandar on 10/06/2013
 * Comment          : 
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PersianCore;
using PersianCore.Framework.Animation;
using PersianCore.Graphics;
using System;
using System.Collections.Generic;
using System.IO;

public static class Persian
{
    #region Fields & Properties

    public static Queue<Func<object, string>> RuntimeLoadings = new Queue<Func<object, string>>();

    public static bool ShutDown { get; set; }

    public static string EVersion = string.Empty;
    
    #region CutScenes

    public static List<CutScene> CutScenes = new List<CutScene>();
    public static int ActiveCutScene = -1;
    public static List<string> CutSceneNames
    {
        get
        {
            var cutSceneNames = new List<string>();
            foreach (var cs in CutScenes)
            {
                cutSceneNames.Add(cs.Name);
            }
            return cutSceneNames;
        }
    }
    public static List<string> CameraCutSceneNames
    {
        get
        {
            var cameraCutSceneNames = new List<string>();
            foreach (var cs in CutScenes)
            {
                cameraCutSceneNames.Add(cs.AnimatedCameraName);
            }
            return cameraCutSceneNames;
        }
    }

    #endregion

    /// <summary>
    /// Indicates that we are running game from engine or editor
    /// </summary>
    public static bool RunningEngine;

    /// <summary>
    /// Pointer to Game
    /// </summary>
    static Game game { get; set; }

    /// <summary>
    /// Camera
    /// </summary>
    public static CameraManager Camera;

    /// <summary>
    /// Quad render
    /// </summary>
    public static Quad Quad = new Quad();

    /// <summary>
    /// Global game time
    /// </summary>
    public static GameTime gameTime;

    /// <summary>
    /// Graphics device manager
    /// </summary>
    public static GraphicsDeviceManager GDeviceManager { get; private set; }

    /// <summary>
    /// Graphics device
    /// </summary>
    public static GraphicsDevice GDevice
    {
        get
        {
            return GDeviceManager.GraphicsDevice;
        }
    }

    public static Vector2 HalfScreenSize { get; set; }

    public static ContentManager EngineContent { get; set; }
    public static ContentManager EditorContent { get; set; }

    public static DisplayMode DisplayMode
    {
        get
        {
            return GDevice.DisplayMode;
        }
    }

    public static string CurrentDir { get; set; }
    public static string ContentDir { get; set; }
    public static string EngineContentDir { get; set; }
    public static string EditorBrowserDir { get; set; }

    static SurfaceFormat surfaceFormat;
    static DepthFormat depthFormat;
    static int multiSample;

    #endregion

    #region Methods

    public static string GetUniqueName(string StartWith = "")
    {
        return StartWith + string.Format("{0}{1}{2}{3}{4}{5}",
               DateTime.Now.Month,
               DateTime.Now.Day,
               DateTime.Now.Year,
               DateTime.Now.Hour,
               DateTime.Now.Minute,
               DateTime.Now.Second);
    }

    public static void Initialize(Game Game, bool RunningEngine)
    {
        Persian.RunningEngine = RunningEngine;
        //if game is not equal to null, this class have been called from Engine
        EVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        game = Game;
        if (game != null)
        {
            game.IsFixedTimeStep = true;
            game.IsMouseVisible = false;

            GDeviceManager = new GraphicsDeviceManager(game)
            {
                PreferredBackBufferWidth = 1024,
                PreferredBackBufferHeight = 768,
                PreferMultiSampling = true,
                SynchronizeWithVerticalRetrace = true,
                IsFullScreen = false,
                PreferredBackBufferFormat = SurfaceFormat.Color,
                PreferredDepthStencilFormat = DepthFormat.Depth24Stencil8
            };

            EngineContent = game.Content;
        }
    }

    public static void Load()
    {
        //Load default font
        Fonts.Load();
    }

    public static void GetSharedParams(bool CalledFromEditor, string[] args)
    {
        Persian.CurrentDir = Directory.GetCurrentDirectory();
        if (CalledFromEditor)
        {
            ContentDir = Path.GetFullPath(CurrentDir + @"..\..\..\..\..\..\PersianContent\Content");
            EngineContentDir = Path.GetFullPath(CurrentDir + @"..\..\..\..\..\..\PersianEngine\PersianEngine\bin\x86\Debug\Content");
            EditorBrowserDir = Path.GetFullPath(CurrentDir + @"\Content\EditorBrowser");
        }
        else
        {
            if (args.Length == 0)
            {
                //Run Directly from Engine
                ContentDir = Path.GetFullPath(String.Concat(CurrentDir, @"\Content\"));
                EngineContentDir = ContentDir;
            }
            else
            {
                //Run from Editor
                CurrentDir = Path.GetFullPath(String.Concat(CurrentDir, @"..\..\..\..\..\..\PersianEngine\PersianEngine\bin\x86\Debug\"));
                ContentDir = Path.GetFullPath(String.Concat(CurrentDir, @"\Content\"));
                EngineContentDir = ContentDir;
            }
        }
    }

    public static bool Check(ref SurfaceFormat pSurfaceFormat, ref DepthFormat pDepthFormat, ref int pMultiSample)
    {
        if (!GDevice.Adapter.IsProfileSupported(GraphicsProfile.HiDef))
        {
            throw new Exception("Graphics Card does not support HiDef");
        }

        return GDevice.Adapter.QueryBackBufferFormat(
             GraphicsProfile.HiDef,
             pSurfaceFormat,
             pDepthFormat,
             pMultiSample,
             out surfaceFormat,
             out depthFormat,
             out multiSample);
    }

    public static void ApplyChanges(int Width = 1024, int Height = 768, bool FullScreen = false,
        bool PreferMultiSampling = true, bool VSync = true)
    {
        GDeviceManager.PreferredBackBufferFormat = surfaceFormat;
        GDeviceManager.PreferredDepthStencilFormat = depthFormat;
        GDeviceManager.PreferredBackBufferWidth = Width;
        GDeviceManager.PreferredBackBufferHeight = Height;
        GDeviceManager.PreferMultiSampling = PreferMultiSampling;
        GDeviceManager.IsFullScreen = FullScreen;
        GDeviceManager.SynchronizeWithVerticalRetrace = VSync;
        game.IsFixedTimeStep = VSync;

        GDeviceManager.ApplyChanges();
    }

    #endregion

    #region Dispose

    static void Dispose()
    {
        SystemMemory.SafeDispose(GDevice);
        RuntimeLoadings.Clear();
        EngineContent.Unload();
    }

    #endregion
}
