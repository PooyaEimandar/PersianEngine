/*
 * Copyright (c) BaziPardaz.Co Ltd. All rights reserved.
 * 
 * File Name        : ENUMS.cs
 * File Description : Enums
 * Generated by     : Pooya Eimandar
 * Last modified by : Pooya Eimandar on 8/27/2013
 * Comment          : 
 */

using System;
using System.Collections.Generic;
using PersianCore;

public static class ENUMS
{
    /// <summary>
    /// Every Enums that are need for saving and loading must be store here
    /// </summary>
    static readonly Dictionary<string, Type> enums = new Dictionary<string, Type>()
    {
        {typeof(PersianCore.Graphics.Lights.LightType).Name , typeof(PersianCore.Graphics.Lights.LightType)},
        {typeof(CameraManager.ActiveCamera).Name , typeof(CameraManager.ActiveCamera)},
        //{typeof(WeaponType).Name , typeof(WeaponType)},
        //{typeof(WeaponState).Name , typeof(WeaponState)},
        {typeof(PersianCore.Physic.PhysicTypes).Name , typeof(PersianCore.Physic.PhysicTypes)},   
    };

    public static Enum GetEnum(string Name)
    {
        Type enumType = null;
        enums.TryGetValue(Name, out enumType);
        if (enumType.IsEnum)
        {
            return (Enum)Activator.CreateInstance(enumType);
        }
        return null;
    }

    public static T StringToEnum<T>(string name)
    {
        if (Enum.IsDefined(typeof(T), name))
        {
            return (T)Enum.Parse(typeof(T), name);
        }
        return default(T);
    }

    public static Enum StringToEnum(Type enumType, string name)
    {
        if (Enum.IsDefined(enumType, name))
        {
            return (Enum)Enum.Parse(enumType, name);
        }
        return null;
    }
}

public enum Compass : byte
{
    /// <summary>
    /// (...)
    /// </summary>
    From_NotEQ_Till_NotEQ,
    /// <summary>
    /// (...]
    /// </summary>
    From_NotEQ_Till_EQ,
    /// <summary>
    /// [...)
    /// </summary>
    From_EQ_Till_NotEQ,
    /// <summary>
    /// [...]
    /// </summary>
    From_EQ_Till_EQ,
    /// <summary>
    /// Less=[...
    /// </summary>
    LessEqualFrom,
    /// <summary>
    /// Less[
    /// </summary>
    LessFrom,
    /// <summary>
    /// Greate=]
    /// </summary>
    GreatEqualfrom,
    /// <summary>
    /// Greate]
    /// </summary>
    GreatFrom,
}