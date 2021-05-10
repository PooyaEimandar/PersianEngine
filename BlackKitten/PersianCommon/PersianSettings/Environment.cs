﻿/*
 * Copyright (c) BaziPardaz.Co Ltd. All rights reserved.
 * 
 * File Name        : SkySet.cs
 * File Description : The setting of static mesh 
 * Generated by     : Pooya Eimandar
 * Last modified by : Pooya Eimandar on 8/27/2013
 * Comment          : 
 */

using Microsoft.Xna.Framework;

namespace PersianSettings
{
    public class EnvironmentSet
    {
        public string ID;
        public Vector3[] StartPositions;
        public float Scale;
        public Vector3 Speed;
        public Vector2 Height;
        public Vector3 Distrotion;
        public string FireTexAsset;
        public string DisTexAsset;
        public string OpacityTexAsset;
    }

    public class SkySet
    {
        public string TextureName = null;
        public string CloudsName = null;
        public Vector3 SunVector = new Vector3();
        public bool UseSameTexture;
        public float TexCoordScale;
        public bool AllowRotate;
    }
}