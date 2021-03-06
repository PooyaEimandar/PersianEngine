/*
 * Copyright (c) BaziPardaz.Co Ltd. All rights reserved.
 * 
 * File Name        : CameraManager.cs
 * File Description : The manager of camera
 * Generated by     : Pooya Eimandar
 * Last modified by : Pooya Eimandar on 5/19/2013
 * Comment          : 
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PersianCore.Cameras;
using System;

namespace PersianCore
{
    public class CameraManager
    {
        #region Fields & Properties

        //List<CameraCutScene> cutScenes;
        //public List<string> CutSceneNames
        //{
        //    get
        //    {
        //        var names = new List<string>();
        //        foreach (var cs in cutScenes)
        //        {
        //            names.Add(cs.Name);
        //        }
        //        return names;
        //    }
        //}

        /// <summary>
        /// Only used for free camera
        /// </summary>
        public bool UseCustomeViewProjection
        {
            get
            {
                switch (activeCamera)
                {
                    default:
                    case ActiveCamera.Free:
                        return this._freeCamera.UseCustomeViewProjection;
                }
            }
            set
            {
                switch (activeCamera)
                {
                    default:
                    case ActiveCamera.Free:
                        this._freeCamera.UseCustomeViewProjection = value;
                        break;
                }
            }
        }

        /// <summary>
        /// Always set data either in editor mode or engine mode
        /// </summary>
        public Guid GuidBind
        {
            get
            {
                return this._chaseCamera.BindingGuid;
                //switch (activeCamera)
                //{
                //    default:
                //    case ActiveCamera.Free:
                //        return this._freeCamera.BindingGuid;
                //    case ActiveCamera.Chase:
                //        return this._chaseCamera.BindingGuid;
                //}
            }
            set
            {
                this._chaseCamera.BindingGuid = value;
                //switch (activeCamera)
                //{
                //    default:
                //    case ActiveCamera.Free:
                //        this._freeCamera.BindingGuid = value;
                //        break;
                //    case ActiveCamera.Chase:
                //        this._chaseCamera.BindingGuid = value;
                //        break;
                //}
            }
        }

        ChaseCamera _chaseCamera;
        public ChaseCamera chaseCamera
        {
            get
            {
                return this._chaseCamera;
            }
        }

        FreeCamera _freeCamera;
        public FreeCamera freeCamera
        {
            get
            {
                return this._freeCamera;
            }
        }

        ActiveCamera activeCamera;
        public enum ActiveCamera { Chase, Free };

        public Meshes.Mesh BindTo
        {
            get
            {
                switch (activeCamera)
                {
                    default:
                    case ActiveCamera.Free:
                        return this._freeCamera.BindTo;
                    case ActiveCamera.Chase:
                        return this._chaseCamera.BindTo;
                }
            }
            set
            {
                switch (activeCamera)
                {
                    default:
                    case ActiveCamera.Free:
                        this._freeCamera.BindTo = value;
                        break;
                    case ActiveCamera.Chase:
                        this._chaseCamera.BindTo = value;
                        break;
                }
            }
        }

        public Vector3 Up
        {
            get
            {
                switch (activeCamera)
                {
                    default:
                    case ActiveCamera.Free:
                        return this._freeCamera.Up;
                    case ActiveCamera.Chase:
                        return this._chaseCamera.Up;
                }
            }
        }

        public Vector3 Right
        {
            get
            {
                switch (activeCamera)
                {
                    default:
                    case ActiveCamera.Free:
                        return this._freeCamera.Right;
                    case ActiveCamera.Chase:
                        return this._chaseCamera.Right;
                }
            }
        }

        public Vector3 Forward
        {
            get
            {
                switch (activeCamera)
                {
                    default:
                    case ActiveCamera.Free:
                        return this._freeCamera.Forward;
                    case ActiveCamera.Chase:
                        return this._chaseCamera.Forward;
                }
            }
        }

        public Matrix World
        {
            get
            {
                switch (activeCamera)
                {
                    default:
                    case ActiveCamera.Free:
                        return this._freeCamera.World;
                    case ActiveCamera.Chase:
                        return this._chaseCamera.World;
                }
            }
        }

        public Matrix View
        {
            get
            {
                if (!Persian.RunningEngine)
                {
                    return this._freeCamera.View;
                }
                else
                {
                    switch (activeCamera)
                    {
                        default:
                        case ActiveCamera.Free:
                            return this._freeCamera.View;
                        case ActiveCamera.Chase:
                            return this._chaseCamera.View;
                    }
                }
            }
        }

        public Matrix Projection
        {
            get
            {
                if (!Persian.RunningEngine)
                {
                    return this._freeCamera.Projection;
                }
                else
                {
                    switch (activeCamera)
                    {
                        default:
                        case ActiveCamera.Free:
                            return this._freeCamera.Projection;
                        case ActiveCamera.Chase:
                            return this._chaseCamera.Projection;
                    }
                }
            }
        }

        public Matrix ViewProjection
        {
            get
            {
                if (!Persian.RunningEngine)
                {
                    return this._freeCamera.ViewProjection;
                }
                else
                {
                    switch (activeCamera)
                    {
                        default:
                        case ActiveCamera.Free:
                            return this._freeCamera.ViewProjection;
                        case ActiveCamera.Chase:
                            return this._chaseCamera.ViewProjection;
                    }
                }
            }
        }

        public Vector3 Position
        {
            set
            {
                if (!Persian.RunningEngine)
                {
                    this._freeCamera.Position = value;
                }
                else
                {
                    switch (activeCamera)
                    {
                        default:
                        case ActiveCamera.Free:
                            this._freeCamera.Position = value;
                            break;
                        case ActiveCamera.Chase:
                            this._chaseCamera.Position = value;
                            break;
                    }
                }
            }
            get
            {
                if (!Persian.RunningEngine)
                {
                    return this._freeCamera.Position;
                }
                else
                {
                    switch (activeCamera)
                    {
                        default:
                        case ActiveCamera.Free:
                            return this._freeCamera.Position;
                        case ActiveCamera.Chase:
                            return this._chaseCamera.Position;
                    }
                }
            }
        }

        public Vector3 Target
        {
            set
            {
                if (!Persian.RunningEngine)
                {
                    this._freeCamera.Target = value;
                }
                else
                {
                    switch (activeCamera)
                    {
                        default:
                        case ActiveCamera.Free:
                            this._freeCamera.Target = value;
                            break;
                        case ActiveCamera.Chase:
                            this._chaseCamera.Target = value;
                            break;
                    }
                }
            }
            get
            {
                if (!Persian.RunningEngine)
                {
                    return this._freeCamera.Target;
                }
                else
                {
                    switch (activeCamera)
                    {
                        default:
                        case ActiveCamera.Free:
                            return this._freeCamera.Target;
                        case ActiveCamera.Chase:
                            return this._chaseCamera.Target;
                    }
                }
            }
        }

        public Vector2 Angle
        {
            get
            {
                return this._freeCamera.Angles;
            }
            set
            {
                this._freeCamera.Angles = value;
            }
        }

        public BoundingFrustum Frustum
        {
            get
            {
                if (!Persian.RunningEngine)
                {
                    return this._freeCamera.Frustum;
                }
                else
                {
                    switch (activeCamera)
                    {
                        default:
                        case ActiveCamera.Free:
                            return this._freeCamera.Frustum;
                        case ActiveCamera.Chase:
                            return this._chaseCamera.Frustum;
                    }
                }
            }
        }

        public float TanFieldOfView
        {
            get
            {
                if (!Persian.RunningEngine)
                {
                    return this._freeCamera.TanFieldOfView;
                }
                else
                {
                    switch (activeCamera)
                    {
                        default:
                        case ActiveCamera.Free:
                            return this._freeCamera.TanFieldOfView;
                        case ActiveCamera.Chase:
                            return this._chaseCamera.TanFieldOfView;
                    }
                }
            }
        }

        public float AspectRatio
        {
            get
            {
                if (!Persian.RunningEngine)
                {
                    return this._freeCamera.AspectRatio;
                }
                else
                {
                    switch (activeCamera)
                    {
                        default:
                        case ActiveCamera.Free:
                            return this._freeCamera.AspectRatio;
                        case ActiveCamera.Chase:
                            return this._chaseCamera.AspectRatio;
                    }
                }
            }
            set
            {
                if (!Persian.RunningEngine)
                {
                    this._freeCamera.AspectRatio = value;
                }
                else
                {
                    switch (activeCamera)
                    {
                        default:
                        case ActiveCamera.Free:
                            this._freeCamera.AspectRatio = value;
                            break;
                        case ActiveCamera.Chase:
                            this._chaseCamera.AspectRatio = value; ;
                            break;
                    }
                }
            }
        }

        public float NearClip
        {
            get
            {
                if (!Persian.RunningEngine)
                {
                    return this._freeCamera.NearClip;
                }
                else
                {
                    switch (activeCamera)
                    {
                        default:
                        case ActiveCamera.Free:
                            return this._freeCamera.NearClip;
                        case ActiveCamera.Chase:
                            return this._chaseCamera.NearClip;
                    }
                }
            }
            set
            {
                if (!Persian.RunningEngine)
                {
                    this._freeCamera.NearClip = value;
                }
                else
                {
                    switch (activeCamera)
                    {
                        default:
                        case ActiveCamera.Free:
                            this._freeCamera.NearClip = value;
                            break;
                        case ActiveCamera.Chase:
                            this._chaseCamera.NearClip = value;
                            break;
                    }
                }
            }
        }

        public float FarClip
        {
            get
            {
                if (!Persian.RunningEngine)
                {
                    return this._freeCamera.FarClip;
                }
                else
                {
                    switch (activeCamera)
                    {
                        default:
                        case ActiveCamera.Free:
                            return this._freeCamera.FarClip;
                        case ActiveCamera.Chase:
                            return this._chaseCamera.FarClip;
                    }
                }
            }
            set
            {
                if (!Persian.RunningEngine)
                {
                    this._freeCamera.FarClip = value;
                }
                else
                {
                    switch (activeCamera)
                    {
                        default:
                        case ActiveCamera.Free:
                            this._freeCamera.FarClip = value;
                            break;
                        case ActiveCamera.Chase:
                            this._chaseCamera.FarClip = value;
                            break;
                    }
                }
            }
        }

        public bool NeedUpdate
        {
            get
            {
                switch (activeCamera)
                {
                    default:
                    case ActiveCamera.Free:
                        return this._freeCamera.NeedUpdate;
                    case ActiveCamera.Chase:
                        return this._chaseCamera.NeedUpdate;
                }
            }
            set
            {
                switch (activeCamera)
                {
                    default:
                    case ActiveCamera.Free:
                        this._freeCamera.NeedUpdate = value;
                        break;
                    case ActiveCamera.Chase:
                        this._chaseCamera.NeedUpdate = value;
                        break;
                }
            }
        }

        /// <summary>
        /// in Editor we just use FreeCamera
        /// </summary>
        /// <returns></returns>
        public ActiveCamera GetCurrentCamera()
        {
            return this.activeCamera;
        }

        #endregion

        #region Constructor

        public CameraManager(Viewport pViewport, ActiveCamera activeCamera)
        {
            this.activeCamera = activeCamera;
            this._chaseCamera = new ChaseCamera(pViewport);
            this._freeCamera = new FreeCamera(pViewport);
        }

        #endregion

        #region Method

        public void CustomUpdate(Vector3 position, Vector3 target, Vector3 up, 
            float fov, float AspecRatio, float NClip, float FClip)
        {
            switch (activeCamera)
            {
                case ActiveCamera.Free:
                    this._freeCamera.View = Matrix.CreateLookAt(position, target, up);
                    this._freeCamera.Projection = Matrix.CreatePerspectiveFieldOfView(fov, AspecRatio, NClip, FClip);
                    this._freeCamera.Frustum.Matrix = this._freeCamera.ViewProjection;
                    break;
                case ActiveCamera.Chase:
                    this._chaseCamera.View = Matrix.CreateLookAt(position, target, up);
                    this._chaseCamera.Projection = Matrix.CreatePerspectiveFieldOfView(fov, AspecRatio, NClip, FClip);
                    this._chaseCamera.Frustum.Matrix = this._chaseCamera.ViewProjection;
                    break;
            }
        }

        internal void SetProjection(Matrix Projection)
        {
            switch(activeCamera)
            {
                case ActiveCamera.Free:
                    this._freeCamera.Projection = Projection;
                    break;
                case ActiveCamera.Chase:
                    this._chaseCamera.Projection = Projection;
                    break;
            }
        }

        internal void SetView(Matrix View)
        {
            switch (activeCamera)
            {
                case ActiveCamera.Free:
                    this._freeCamera.View = View;
                    break;
                case ActiveCamera.Chase:
                    this._chaseCamera.View = View;
                    break;
            }
        }

        public void AddToHistory(sbyte i)
        {
            if (!Persian.RunningEngine)
            {
                this.freeCamera.CHistoryManager.AddToHistory(i, this.Position, this.Target);
            }
        }

        public void FocusOnHistory(sbyte i)
        {
            if (!Persian.RunningEngine)
            {
                CameraHistory? cameraHistory = this.freeCamera.CHistoryManager.GetFromHistory(i);
                if (cameraHistory != null)
                {
                    this.Position = cameraHistory.Value.Position;
                    this.Target = cameraHistory.Value.Target;
                }
            }
        }

        public void SetActiveCamera(ActiveCamera value)
        {
            this.activeCamera = value;
        }

        public ActiveCamera GetActiveCamera()
        {
            return this.activeCamera;
        }

        public void SetMouseTranslationSpeed(float value)
        {
            Vector3 vector = this.freeCamera.Setting;
            vector.X = value;
            this.freeCamera.Setting = vector;
        }

        public void SetMouseRotationSpeed(float value)
        {
            Vector3 vector = this.freeCamera.Setting;
            vector.Y = value;
            this.freeCamera.Setting = vector;
        }

        public void SetMouseScrollWheel(float value)
        {
            var vector = this.freeCamera.Setting;
            vector.Z = value;
            this.freeCamera.Setting = vector;
        }

        #endregion

        #region Update

        public void BeginUpdate4Editor()
        {
            this._freeCamera.NeedUpdate = false;
        }

        public void Update4Editor(GameTime gameTime)
        {
            this._freeCamera.Update(gameTime);
        }

        public void BeginUpdate4Engine()
        {
            this._chaseCamera.NeedUpdate = false;
        }

        public void Update4Engine(GameTime gameTime)
        {
            Persian.Camera.SetMouseScrollWheel((float)(InputManager.ScrollMouse));
            switch (activeCamera)
            {
                case ActiveCamera.Chase:
                    this._chaseCamera.Update(gameTime);
                    break;
                case ActiveCamera.Free:
                    //if (InputManager.IsKeyHolded(Microsoft.Xna.Framework.Input.Keys.S) || Persian.Camera.freeCamera.ForceUpdate)
                    //{
                    this._freeCamera.Update(gameTime);
                    //}
                    break;
            }
        }

        public void Reset()
        {
            if (!Persian.RunningEngine)
            {
                this._freeCamera.Reset();
            }
            else
            {
                if (activeCamera == ActiveCamera.Chase)
                {
                    this._chaseCamera.Reset();
                }
                else
                {
                    this._freeCamera.Reset();
                }
            }
        }

        #endregion

        #region Dispose

        public void Dispose()
        {
            SystemMemory.SafeDispose(this._chaseCamera);
            SystemMemory.SafeDispose(this._freeCamera);
        }

        #endregion
    }
}
