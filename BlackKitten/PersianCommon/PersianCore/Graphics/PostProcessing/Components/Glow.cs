/*
 * Copyright (c) BaziPardaz.Co Ltd. All rights reserved.
 * 
 * File Name        : Bloom.cs
 * File Description : The bloom post process
 * Generated by     : Pooya Eimandar
 * Last modified by : Pooya Eimandar on 12/15/2013
 * Comment          : 
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PersianCore.Graphics.PostProcessing
{
    public class Glow : Disposable
    {
        #region Fields & Properties

        GaussianBlur blur;
        public float VerticalBlurAmount
        {
            get
            {
                return this.blur.VAmount;
            }
            set
            {
                if (this.blur != null)
                {
                    this.blur.VAmount = value;
                }
            }
        }
        public float HorizentalBlurAmount
        {
            get
            {
                return this.blur.HAmount;
            }
            set
            {
                if (this.blur != null)
                {
                    this.blur.HAmount = value;
                }
            }
        }
        RenderTarget resultTarget;
        SpriteBatch spriteBatch; 

        #endregion

        #region Constructor

        public Glow()
        {
            this.blur = new GaussianBlur()
            {
                VAmount = 2.0f,
                HAmount = 2.0f,
            };
        }

        #endregion

        #region Load

        public void Load(ContentManager Content)
        {
            // Create a texture for rendering the main scene, prior to applying bloom.
            this.resultTarget = new RenderTarget();
            this.spriteBatch = new SpriteBatch(Persian.GDevice);
            this.blur.Load(Content);
        }

        #endregion

        #region Draw

        /// <summary>
        /// This is where it all happens. Grabs a scene that has already been rendered,
        /// and uses postprocess magic to add a glowing bloom effect over the top of it.
        /// </summary>
        public Texture2D RenderToFX(Lights.PrepassTechnique.LBuffers lBuffers, Texture2D Input, SamplerState samplerState)
        {
            var bounds = Persian.GDevice.PresentationParameters.Bounds;

            //Pass 0 : first make blur pass on glow target
            var bluredResult = this.blur.RenderToFX(lBuffers.glowBuffer);
            this.resultTarget.Begin();
            {
                Persian.GDevice.Clear(Color.Black);

                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, samplerState, DepthStencilState.Default, RasterizerState.CullCounterClockwise);
                spriteBatch.Draw(bluredResult, bounds, Color.White);
                spriteBatch.Draw(Input, bounds, Color.White);
                spriteBatch.End();

                Persian.GDevice.BlendState = BlendState.Opaque;

                this.resultTarget.End();
            }

            return this.resultTarget.Texture2D;
        }

        #endregion

        #region Dispose

        /// <summary>
        /// Unload your graphics content.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (!disposing || isDisposed) return;

            SystemMemory.SafeDispose(this.resultTarget);
            SystemMemory.SafeDispose(this.spriteBatch);
            SystemMemory.SafeDispose(this.blur);
            
            base.Dispose(disposing);
        }

        #endregion
    }
}
