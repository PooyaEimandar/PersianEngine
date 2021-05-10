﻿/*
 * Copyright (c) BaziPardaz.Co Ltd. All rights reserved.
 * 
 * File Name        : ChaseCamera.cs
 * File Description : The chase camera
 * Generated by     : Pooya Eimandar
 * Last modified by : Pooya Eimandar on 5/19/2013
 * Comment          : 
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PersianCore.Cameras
{
    public class ChaseCamera : BaseCamera
    {
        #region Constants

        const float RUp = 10.0f, RDown = -4.0f;
        const float LEFTRIGHT = 1.5f, UPDOWN = 4.5f, ZINDEX = 4.0f;

        #endregion

        #region Fields & Properties

        Vector3 _velocity;

        float yaw, pitch;
        [DoNotSave]
        public float Yaw
        {
            get
            {
                return this.yaw;
            }
            set
            {
                this.yaw = value;
            }
        }
        [DoNotSave]
        public float Pitch
        {
            get
            {
                return this.pitch;
            }
            set
            {
                pitch = value;
                PMathHelper.Restrict(pitch, RDown, RUp);
            }
        }

        float _stiffness;
        float _damping;
        float _mass;

        Vector3 desiredPosition;
        Vector3 lookAtOffset;
        Vector3 UpVec;
        Vector3 RightVec;
        Vector3 HeadingVec;

        Vector3 desiredPositionOffset;
        public float LeftRight
        {
            get
            {
                return this.desiredPositionOffset.X;
            }
            set
            {
                this.desiredPositionOffset.X = value;
            }
        }

        public float UpDown
        {
            get
            {
                return this.desiredPositionOffset.Y;
            }
            set
            {
                this.desiredPositionOffset.Y = value;
            }
        }

        public float ZIndex
        {
            get
            {
                return this.desiredPositionOffset.Z;
            }
            set
            {
                this.desiredPositionOffset.Z = value;
            }
        }

        #endregion

        #region Constructor

        public ChaseCamera(Viewport viewPort)
            : base(viewPort)
        {
            this._stiffness = 4000.0f;
            this._damping = 800.0f;
            this._mass = 50.0f;
            this.position = Vector3.Zero;
            this.target = Vector3.Zero;
            this.lookAtOffset = new Vector3(1f, 3.5f, -1f);
            this.desiredPositionOffset = new Vector3(LEFTRIGHT, UPDOWN, ZINDEX);
            UpdateProjection();
            SetLookAt(this.position, target, Vector3.Up);
        }

        #endregion

        #region Update

        public void Update(GameTime gameTime)
        {
            this.needUpdate = false;
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            yaw = BindTo.Rotation.Y;
            Matrix ypr_Matrix = Matrix.Identity * Matrix.CreateFromYawPitchRoll(yaw, pitch, 0);

            // Updates the camera position relative to the model Matrix
            desiredPosition = BindTo.Position + Vector3.TransformNormal(desiredPositionOffset, ypr_Matrix);
            Vector3 stretch = this.position - desiredPosition;
            Vector3 force = -_stiffness * stretch - _damping * _velocity;
            Vector3 acceleration = force / _mass;
            _velocity += acceleration * elapsed;
            this.position += _velocity * elapsed;

            this.target = this.position + ypr_Matrix.Forward;
            this.world = ypr_Matrix * Matrix.CreateTranslation(this.position);
            base.UpdateView();
        }

        public void SetLookAt(Vector3 cameraPos, Vector3 CameraTarget, Vector3 UpVector)
        {
            this.position = cameraPos;
            this.target = CameraTarget;
            this.HeadingVec = CameraTarget - cameraPos;
            this.HeadingVec.Normalize();
            this.UpVec = UpVector;
            this.RightVec = Vector3.Cross(this.HeadingVec, this.UpVec);
            base.UpdateView();
        }

        internal void Reset()
        {
            this.desiredPositionOffset = new Vector3(LEFTRIGHT, UPDOWN, ZINDEX);
            base.BaseReset();
        }

        #endregion
    }
}