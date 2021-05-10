﻿/*
 * Copyright (c) BaziPardaz.Co Ltd. All rights reserved.
 * 
 * File Name        : IAnimatedCamera.cs
 * File Description : The interface for information of animated camera
 * Generated by     : Seyed Mahdi Hosseini
 * Last modified by : Seyed Mahdi Hosseini on 9/14/2013
 * Comment          : 
 */

using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace PersianCore.Cameras
{
    public interface IAnimatedCameraData
    {
        List<Vector3> Positions { get; set; }
        List<Vector2> Angles { get; set; }
    }
}
