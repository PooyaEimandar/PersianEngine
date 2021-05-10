/*
 * Copyright (c) BaziPardaz.Co Ltd. All rights reserved.
 * 
 * File Name        : Screens.cs
 * File Description : The screens of game
 * Generated by     : Pooya Eimandar
 * Last modified by : Pooya Eimandar on 10/2/2013
 * Comment          : 
 */

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace PersianSettings
{
    public enum UpdateState { Core, Screen, Both }

    /// <summary>
    /// Settings class describes all the tweakable options used
    /// to control the appearance of a screens.
    /// </summary>
    public class ScreenInfo
    {
        /// <summary>
        /// The name of screen 
        /// </summary>
        public string Name = "NoNameScreen";
        
        /// <summary>
        /// The name of next screen 
        /// </summary>
        public string NextScreenName = string.Empty;

        /// <summary>
        /// The name of last screen 
        /// </summary>
        //public string LastScreenName = string.Empty;

        /// <summary>
        /// Start time for screen 
        /// </summary>
        public TimeSpan WaitBefore = TimeSpan.FromSeconds(1.0f);

        /// <summary>
        /// Duration of Screen 
        /// </summary>
        public TimeSpan Duration = TimeSpan.Zero;

        /// <summary>
        /// Start this screen with fade effect
        /// </summary>
        public bool StartWithFading = false;

        /// <summary>
        /// Set state of screen 
        /// </summary>
        [ContentSerializerIgnore]
        public UpdateState UpdateState = UpdateState.Screen;
        [ContentSerializer(ElementName = "UpdateState")]
        private string StateSerializationHelper
        {
            get
            {
                return this.UpdateState.ToString();
            }

            set
            {
                switch (value)
                {
                    case "Core": this.UpdateState = UpdateState.Core; break;
                    case "Screen": this.UpdateState = UpdateState.Screen; break;
                    case "Both": this.UpdateState = UpdateState.Both; break;

                    default:
                        throw new ArgumentException("Unknown State " + value);
                }
            }
        }
        
        /// <summary>
        /// Alpha blending settings. 
        /// </summary>
        [ContentSerializerIgnore]
        public BlendState BlendState = BlendState.Additive;
        [ContentSerializer(ElementName = "BlendState")]
        private string BlendStateSerializationHelper
        {
            get { return this.BlendState.Name.Replace("BlendState.", string.Empty); }

            set
            {
                switch (value)
                {
                    case "AlphaBlend": this.BlendState = BlendState.AlphaBlend; break;
                    case "Additive": this.BlendState = BlendState.Additive; break;
                    case "NonPremultiplied": this.BlendState = BlendState.NonPremultiplied; break;

                    default:
                        throw new ArgumentException("Unknown blend state " + value);
                }
            }
        }
    }
}
