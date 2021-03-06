/*
 * Copyright (c) BaziPardaz.Co Ltd. All rights reserved.
 * 
 * File Name        : FXAA.cs
 * File Description : The class implement fast approximate anti aliasing
 * Generated by     : Pooya Eimandar
 * Last modified by : Pooya Eimandar on 8/21/2013
 * Comment          : 
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PersianCore.Graphics.AntiAliasing
{
    internal class FXAA : Node
    {
        #region Fields & Properties

        /// <summary>
        /// Near the back
        /// </summary>
        const float MainImageDepth = 0.9f;
        const float ThingsInFrontDepth = 0.5f;

        RenderTarget renderTarget;

        public bool Enable { get; set; }

        /// <summary>
        /// This effects sub-pixel AA quality and inversely sharpness. 
        /// Where N ranges between,
        ///     N = 0.50 (default)
        ///     N = 0.33 (sharper)
        /// </summary>
        public float N;

        /// <summary>
        /// Choose the amount of sub-pixel aliasing removal.
        /// This can effect sharpness.
        ///   1.00 - upper limit (softer)
        ///   0.75 - default amount of filtering
        ///   0.50 - lower limit (sharper, less sub-pixel aliasing removal)
        ///   0.25 - almost off
        ///   0.00 - completely off
        /// </summary>
        public float subPixelAliasingRemoval;

        /// <summary>
        /// The minimum amount of local contrast required to apply algorithm.
        ///   0.333 - too little (faster)
        ///   0.250 - low quality
        ///   0.166 - default
        ///   0.125 - high quality 
        ///   0.063 - overkill (slower)
        /// </summary>
        public float edgeTheshold;

        /// <summary>
        /// Trims the algorithm from processing darks.
        ///   0.0833 - upper limit (default, the start of visible unfiltered edges)
        ///   0.0625 - high quality (faster)
        ///   0.0312 - visible limit (slower)
        /// Special notes when using FXAA_GREEN_AS_LUMA,
        ///   Likely want to set this to zero.
        ///   As colors that are mostly not-green
        ///   will appear very dark in the green channel!
        ///   Tune by looking at mostly non-green content,
        ///   then start at zero and increase until aliasing is a problem.
        /// </summary>
        public float edgeThesholdMin;

        /// <summary>
        /// This does not effect PS3, as this needs to be compiled in.
        ///   Use FXAA_CONSOLE__PS3_EDGE_SHARPNESS for PS3.
        ///   Due to the PS3 being ALU bound,
        ///   there are only three safe values here: 2 and 4 and 8.
        ///   These options use the shaders ability to a free *|/ by 2|4|8.
        /// For all other platforms can be a non-power of two.
        ///   8.0 is sharper (default!!!)
        ///   4.0 is softer
        ///   2.0 is really soft (good only for vector graphics inputs)
        /// </summary>
        public float consoleEdgeSharpness;

        /// <summary>
        /// This does not effect PS3, as this needs to be compiled in.
        ///   Use FXAA_CONSOLE__PS3_EDGE_THRESHOLD for PS3.
        ///   Due to the PS3 being ALU bound,
        ///   there are only two safe values here: 1/4 and 1/8.
        ///   These options use the shaders ability to a free *|/ by 2|4|8.
        /// The console setting has a different mapping than the quality setting.
        /// Other platforms can use other values.
        ///   0.125 leaves less aliasing, but is softer (default!!!)
        ///   0.25 leaves more aliasing, and is sharper
        /// </summary>
        public float consoleEdgeThreshold;

        /// <summary>
        /// Trims the algorithm from processing darks.
        /// The console setting has a different mapping than the quality setting.
        /// This only applies when FXAA_EARLY_EXIT is 1.
        /// This does not apply to PS3, 
        /// PS3 was simplified to avoid more shader instructions.
        ///   0.06 - faster but more aliasing in darks
        ///   0.05 - default
        ///   0.04 - slower and less aliasing in darks
        /// Special notes when using FXAA_GREEN_AS_LUMA,
        ///   Likely want to set this to zero.
        ///   As colors that are mostly not-green
        ///   will appear very dark in the green channel!
        ///   Tune by looking at mostly non-green content,
        ///   then start at zero and increase until aliasing is a problem.
        /// </summary>
        public float consoleEdgeThresholdMin;
        Effect effect;
        EffectParameter WVPParam { get; set; }
        EffectParameter InverseViewportSizeParam { get; set; }
        EffectParameter ConsoleSharpnessParam { get; set; }
        EffectParameter ConsoleOpt1Param { get; set; }
        EffectParameter ConsoleOpt2Param { get; set; }
        EffectParameter SubPixelAliasingRemovalParam { get; set; }
        EffectParameter EdgeThresholdParam { get; set; }
        EffectParameter EdgeThresholdMinParam { get; set; }
        EffectParameter ConsoleEdgeSharpnessParam { get; set; }
        EffectParameter ConsoleEdgeThresholdParam { get; set; }
        EffectParameter ConsoleEdgeThresholdMinParam { get; set; }

        #endregion

        #region Constructor

        public FXAA()
        {
            this.Enable = true;
            this.N = 0.40f;
            this.subPixelAliasingRemoval = 0.75f;
            this.edgeTheshold = 0.166f;
            this.edgeThesholdMin = 0f;
            this.consoleEdgeSharpness = 8.0f;
            this.consoleEdgeThreshold = 0.125f;
            this.consoleEdgeThresholdMin = 0f;
        }

        #endregion

        #region Load

        public void Load(ContentManager Content)
        {
            this.renderTarget = new RenderTarget();

            string _LightPath = @"Shaders\";
            this.effect = Content.Load<Effect>(string.Format(@"{0}{1}AntiAliasing\FXAA", CoreShared.PrePathContent, _LightPath));

            var effectParameters = this.effect.Parameters;
            this.WVPParam = effectParameters["WVP"];
            this.InverseViewportSizeParam = effectParameters["InverseViewportSize"];
            this.ConsoleSharpnessParam = effectParameters["ConsoleSharpness"];
            this.ConsoleOpt1Param = effectParameters["ConsoleOpt1"];
            this.ConsoleOpt2Param = effectParameters["ConsoleOpt2"];
            this.SubPixelAliasingRemovalParam = effectParameters["SubPixelAliasingRemoval"];
            this.EdgeThresholdParam = effectParameters["EdgeThreshold"];
            this.EdgeThresholdMinParam = effectParameters["EdgeThresholdMin"];
            this.ConsoleEdgeSharpnessParam = effectParameters["ConsoleEdgeSharpness"];
            this.ConsoleEdgeThresholdParam = effectParameters["ConsoleEdgeThreshold"];
            this.ConsoleEdgeThresholdMinParam = effectParameters["ConsoleEdgeThresholdMin"];


            this.SubPixelAliasingRemovalParam.SetValue(subPixelAliasingRemoval);
            this.EdgeThresholdParam.SetValue(edgeTheshold);
            this.EdgeThresholdMinParam.SetValue(edgeThesholdMin);
            this.ConsoleEdgeSharpnessParam.SetValue(consoleEdgeSharpness);
            this.ConsoleEdgeThresholdParam.SetValue(consoleEdgeThreshold);
            this.ConsoleEdgeThresholdMinParam.SetValue(consoleEdgeThresholdMin);
        }

        #endregion

        #region Draw

        public void Draw(SpriteBatch spriteBatch, params Texture2D[] targets)
        {
            var viewport = Persian.GDevice.Viewport;
            var projection = Matrix.CreateOrthographicOffCenter(0, viewport.Width, viewport.Height, 0, 0, 1);
            var halfPixelOffset = Matrix.CreateTranslation(-0.5f, -0.5f, 0);

            this.renderTarget.Begin();
            {
                spriteBatch.Begin(SpriteSortMode.Deferred,
                    BlendState.AlphaBlend,
                    SamplerState.PointClamp,
                    DepthStencilState.Default,
                    RasterizerState.CullCounterClockwise);
                {
                    for (int i = 0; i < targets.Length; ++i)
                    {
                        spriteBatch.Draw(targets[i], viewport.Bounds, Color.White);
                    }
                    spriteBatch.End();
                }

                this.WVPParam.SetValue(halfPixelOffset * projection);
                this.InverseViewportSizeParam.SetValue(new Vector2(1f / viewport.Width, 1f / viewport.Height));
                this.ConsoleSharpnessParam.SetValue(
                    new Vector4(-N / viewport.Width, -N / viewport.Height, N / viewport.Width, N / viewport.Height));

                this.ConsoleOpt1Param.SetValue(new Vector4(
                    -2.0f / viewport.Width,
                    -2.0f / viewport.Height,
                    2.0f / viewport.Width,
                    2.0f / viewport.Height));

                this.ConsoleOpt2Param.SetValue(new Vector4(
                    8.0f / viewport.Width,
                    8.0f / viewport.Height,
                    -4.0f / viewport.Width,
                    -4.0f / viewport.Height));


                this.effect.CurrentTechnique = this.effect.Techniques[Enable ? "FXAA" : "Standard"];
                this.renderTarget.End();
            }

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Opaque, SamplerState.LinearClamp, 
                null, null, this.effect);
            {
                spriteBatch.Draw(renderTarget.Texture2D, viewport.Bounds, Color.White);
                spriteBatch.End();
            }
        }

        #endregion

        #region Dispose

        protected override void Dispose(bool disposing)
        {
            if (!disposing || isDisposed) return;
            SystemMemory.SafeDispose(this.ConsoleEdgeSharpnessParam);
            SystemMemory.SafeDispose(this.ConsoleEdgeThresholdMinParam);
            SystemMemory.SafeDispose(this.ConsoleEdgeThresholdParam);
            SystemMemory.SafeDispose(this.ConsoleOpt1Param);
            SystemMemory.SafeDispose(this.ConsoleOpt2Param);
            SystemMemory.SafeDispose(this.ConsoleSharpnessParam);
            SystemMemory.SafeDispose(this.EdgeThresholdMinParam);
            SystemMemory.SafeDispose(this.EdgeThresholdParam);
            SystemMemory.SafeDispose(this.InverseViewportSizeParam);
            SystemMemory.SafeDispose(this.SubPixelAliasingRemovalParam);
            SystemMemory.SafeDispose(this.WVPParam);
            SystemMemory.SafeDispose(this.Enable);
            base.Dispose();
        }

        #endregion
    }
}