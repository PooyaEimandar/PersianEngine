﻿/*
 * Copyright (c) BaziPardaz.Co Ltd. All rights reserved.
 * 
 * File Name        : DoNotSave.cs
 * File Description : DoNotSave attribute use for saving scene
 * Generated by     : Pooya Eimandar
 * Last modified by : Pooya Eimandar on 8/27/2013
 * Comment          : 
 */

using System;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public sealed class DoNotSave : Attribute
{
    #region Constructor

    public DoNotSave()
    {
    }

    #endregion
}