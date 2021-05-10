﻿/*
 * Copyright (c) BaziPardaz.Co Ltd. All rights reserved.
 * 
 * File Name        : ColorAnimation.cs
 * File Description : The animation for vector
 * Generated by     : Pooya Eimandar
 * Last modified by : Pooya Eimandar on 7/27/2013
 * Comment          : 
 */
using System;
using Microsoft.Xna.Framework;

namespace PersianCore.Graphics.UI.Animation
{
    public class VectorAnimation
    {
        #region Fields & Properties

        float elapsedTimeMS;
        public Vector4 current;
        public Vector4 from;
        public Vector4 to;
        public TimeSpan duration;
        public AnimationState animationState { get; set; }
        public event EventHandler OnFinished;

        #endregion

        #region Constructor/Destructor

        public VectorAnimation()
            : this(Vector4.Zero, Vector4.Zero, TimeSpan.Zero)
        {
        }

        public VectorAnimation(Vector4 from, Vector4 to, TimeSpan duration)
        {
            this.from = from;
            this.to = to;
            this.duration = duration;
            Reset();
        }

        #endregion

        #region Methods

        public void Reset()
        {
            this.current = from;
            this.elapsedTimeMS = 0;
            this.animationState = AnimationState.NotStarted;
        }

        #endregion

        #region Update

        public Vector4 Update()
        {
            if (this.animationState == AnimationState.Finished) return to;

            this.elapsedTimeMS += (float)Persian.gameTime.ElapsedGameTime.TotalMilliseconds;
            float amount = MathHelper.Clamp(this.elapsedTimeMS / (float)this.duration.TotalMilliseconds, 0, 1);

            this.current.W = MathHelper.Lerp(from.W, to.W, amount);
            this.current.X = MathHelper.Lerp(from.X, to.X, amount);
            this.current.Y = MathHelper.Lerp(from.Y, to.Y, amount);
            this.current.Z = MathHelper.Lerp(from.Z, to.Z, amount);

            if (this.elapsedTimeMS > this.duration.TotalMilliseconds)
            {
                this.animationState = AnimationState.Finished;
                if (OnFinished != null)
                {
                    OnFinished(this, new EventArgs());
                }
            }
            else
            {
                this.animationState = AnimationState.Running;
            }

            return this.current;
        }

        #endregion
    }
}
