﻿/*
 * Copyright (c) BaziPardaz.Co Ltd. All rights reserved.
 * 
 * File Name        : Flare.cs
 * File Description : Flare
 * Generated by     : Pooya Eimandar
 * Last modified by : Pooya Eimandar on 5/19/2013
 * Comment          : 
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PersianCore.Graphics.Environment
{
    public class Flare
    {
        #region Fields

        public float Position;
        public float Scale;
        public Vector3 Color;
        public string TexturePath;
        public Texture2D Texture;

        #endregion

        #region Constructor

        public Flare(float Position, float Scale, Vector3 Color, string TexturePath)
        {
            this.Position = Position;
            this.Scale = Scale;
            this.Color = Color;
            this.TexturePath = TexturePath;
        }

        #endregion
    }
}
