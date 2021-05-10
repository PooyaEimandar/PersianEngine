﻿/*
 * Copyright (c) BaziPardaz.Co Ltd. All rights reserved.
 * 
 * File Name        : SkinnedSphere.cs
 * File Description : Based on SkinnedMeshRuntime library of Microsoft XNA Community
 * Generated by     : Pooya Eimandar
 * Last modified by : Pooya Eimandar on 8/19/2013
 * Comment          : 
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace SkinnedMeshRuntime
{
    public class SkinnedSphere
    {
        public string BoneName;
        public float Radius;

        [ContentSerializer(Optional = true)]
        public Vector3 Center = new Vector3();
    }
}