﻿/*
 * Copyright (c) BaziPardaz.Co Ltd. All rights reserved.
 * 
 * File Name        : PaAnimationBone.cs
 * File Description : PA format for animation bones
 * Generated by     : Seyed Mahdi Hosseini
 * Last modified by : Seyed Mahdi Hosseini on 8/27/2013
 * Comment          : 
 */

using System.Collections.Generic;

namespace PersianCore.Framework.PA
{
    public struct PaAnimationBone
    {
        public List<PaAnimationClip> AC { get; set; }
        public List<PaBone> B { get; set; }
    }
}
