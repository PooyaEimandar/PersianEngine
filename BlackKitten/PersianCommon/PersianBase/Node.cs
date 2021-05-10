﻿/*
 * Copyright (c) BaziPardaz.Co Ltd. All rights reserved.
 * 
 * File Name        : Node.cs
 * File Description : The base object
 * Generated by     : Pooya Eimandar
 * Last modified by : Pooya Eimandar on 8/19/2013
 * Comment          : 
 */

public abstract class Node : 

#if SILVERLIGHT
    Disposable
#else
    FilterablePropertyBase
#endif

{
    #region Fields & Properties

    protected readonly object locker;
    protected object CTag;

    #endregion

    #region Constructor

    public Node()
    {
        this.locker = new object();
        this.isDisposed = false;
    }

    ~Node()
    {
    }

    #endregion

    #region Events

#if !SILVERLIGHT
    /// <summary>
    /// Rised when graphics device changes
    /// </summary>
    /// <param name="e"></param>
    public virtual void OnPreparingDevice(Microsoft.Xna.Framework.PreparingDeviceSettingsEventArgs e)
    {
    }

#endif

    #endregion
}