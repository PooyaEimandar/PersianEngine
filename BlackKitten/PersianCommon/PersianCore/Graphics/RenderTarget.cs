/*
 * Copyright (c) BaziPardaz.Co Ltd. All rights reserved.
 * 
 * File Name        : RenderCapture.cs
 * File Description : The render capture
 * Generated by     : Pooya Eimandar
 * Last modified by : Pooya Eimandar on 9/28/2013
 * Comment          : 
 */

using Microsoft.Xna.Framework.Graphics;

namespace PersianCore.Graphics
{
    /// <summary>
    /// Safe render target
    /// </summary>
    public class RenderTarget : Disposable
    {
        #region Fields & Properties

        RenderTarget2D rt;
        public RenderTarget2D RenderTarget2D
        {
            get
            {
                return this.rt;
            }
        }

        public Texture2D Texture2D
        {
            get
            {
                return this.rt as Texture2D;
            }
        }

        #endregion

        #region Constructor/Destructor

        public RenderTarget()
        {
            var pp = Persian.GDevice.PresentationParameters;
            this.rt = new RenderTarget2D(
                Persian.GDevice, 
                pp.BackBufferWidth, 
                pp.BackBufferHeight);
        }

        public RenderTarget(int width, int height, bool mipMap, SurfaceFormat surfaceFormat,
            DepthFormat depthFormat, int multiSampleCount, RenderTargetUsage renderTargetUsage)
        {
            this.rt = new RenderTarget2D(
                Persian.GDevice,
                width,
                height,
                mipMap,
                surfaceFormat,
                depthFormat,
                multiSampleCount,
                renderTargetUsage);
        }

        #endregion

        #region Methods

        public void Begin()
        {
            if (this.rt == null) Logger.WriteError("Null refrenced render target");
            Persian.GDevice.SetRenderTarget(this.rt);
        }

        public void End()
        {
            Persian.GDevice.SetRenderTarget(null);
        }

        protected override void Dispose(bool disposing)
        {
            if (!disposing || isDisposed) return;
            SystemMemory.SafeDispose(this.rt);
            base.Dispose(disposing);
        }

        #endregion
    }
}
