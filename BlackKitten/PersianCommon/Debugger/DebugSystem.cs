/*
 * Copyright (C) Microsoft Corporation. All rights reserved.
 * 
 * File Name        : DebugSystem.cs
 * File Description : Microsoft XNA Community Game Platform
 * Generated by     : Pooya Eimandar
 * Last modified by : Pooya Eimandar on 7/30/2013
 * Comment          : 
 *                      GameDebugTools.DebugSystem.Initialize(this, "MyFont");
 *                      GameDebugTools.DebugSystem.Instance.TimeRuler.StartFrame()
 *                      GameDebugTools.DebugSystem.Instance.TimeRuler.BeginMark("BlockCode", Color.Blue);
 *                      {
 *                          //Your code goes here//
 *                      }
 *                      GameDebugTools.DebugSystem.Instance.TimeRuler.EndMark("BlockCode");
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Debugger
{
    public class DebugSystem
    {
        /// <summary>
        /// Gets white texture.
        /// </summary>
        static Texture2D WhiteTexture { get; set; }

        /// <summary>
        /// Gets SpriteFont for debug.
        /// </summary>
        static SpriteFont DebugFont { get; set; }
        
        static DebugSystem debugSystemInstance;
        public static DebugSystem Instance
        {
            get 
            { 
                return debugSystemInstance; 
            }
        }
      
        public DebugCommandUI DebugCommandUI { get; private set; }
        public UsageReporter UsageReporter { get; private set; }
        public TimeRuler TimeRuler { get; private set; }
        
        /// <summary>
        /// Initializes the DebugSystem and adds all components to the game's Components collection.
        /// </summary>
        /// <param name="game">The game using the DebugSystem.</param>
        /// <param name="debugFont">The font to use by the DebugSystem.</param>
        /// <returns>The DebugSystem for the game to use.</returns>
        public static DebugSystem Initialize(GraphicsDevice GraphicsDevice, SpriteFont debugFont)
        {
            // if the singleton exists, return that; we don't want two systems being created for a game
            if (debugSystemInstance != null)
            {
                return debugSystemInstance;
            }
            
            DebugFont = debugFont;

            // Create white texture.
            WhiteTexture = new Texture2D(GraphicsDevice, 1, 1);
            Color[] whitePixels = new Color[] { Color.White };
            WhiteTexture.SetData<Color>(whitePixels);

            // Create the system
            debugSystemInstance = new DebugSystem();
            
            debugSystemInstance.DebugCommandUI = new DebugCommandUI();
            debugSystemInstance.UsageReporter = new UsageReporter();
            debugSystemInstance.TimeRuler = new TimeRuler(GraphicsDevice);
            
            return debugSystemInstance;
        }

        // Private constructor; games should use Initialize
        private DebugSystem() 
        { 
        }

        public void Update(GameTime gameTime)
        {
            debugSystemInstance.UsageReporter.Update(gameTime);
            debugSystemInstance.DebugCommandUI.Update(gameTime);
        }

        public void BeginDraw()
        {
            debugSystemInstance.UsageReporter.BeginDraw();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            debugSystemInstance.DebugCommandUI.Draw(spriteBatch, DebugFont, WhiteTexture);            
            debugSystemInstance.TimeRuler.Draw(spriteBatch, DebugFont, WhiteTexture);
        }

        public void DrawStatus(SpriteBatch spriteBatch)
        {
            debugSystemInstance.UsageReporter.DrawStatus(spriteBatch, DebugFont, WhiteTexture);
        }

        public void EndDraw()
        {
            debugSystemInstance.UsageReporter.EndDraw();
        }
    }
}
