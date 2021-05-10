﻿/*
 * Copyright (c) BaziPardaz.Co Ltd. All rights reserved.
 * 
 * File Name        : SystemMemory.cs
 * File Description : Safe dispose memory
 * Generated by     : Pooya Eimandar
 * Last modified by : Pooya Eimandar on 5/15/2013
 * Comment          : 
 */

using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;

public class SystemMemory
{
    public static int DisposedResource = 0;
    public static uint FreePointers = 0;

    public static void SafeDispose(object sender)
    {
        if (sender == null) return;
        try
        {
            if (sender is Texture2D)
            {
                var s = sender as Texture2D;
                if (s.Tag != null)
                {
                    //is shared texture

                    var tag = (AssetTag)(s.Tag);
                    tag._ref--;
                    s.Tag = tag;
                    //All references count equal zero
                    if (tag._ref == 0)
                    {
                        AssetsManager.RemoveTexture(tag._path);
                        s.Dispose();
                    }
                }
                else
                {
                    //is not shared texture or it is a render target
                    s.Dispose();
                }
            }
            else if (sender is GraphicsResource)
            {
                (sender as GraphicsResource).Dispose();
                DisposedResource++;
            }
#if SILVERLIGHT
            else if (sender is SilverlightEffect || sender is RenderTargetBinding)
            {
                sender = null;
                FreePointers++;
            }
#else
            else if (sender is EffectParameter || sender is RenderTargetBinding)
            {
                sender = null;
                FreePointers++;
            }
#endif
            else if (sender is Node)
            {
                (sender as Node).Dispose();
                DisposedResource++;
            }
            else if (sender.GetType().IsArray)
            {
                #region If is Array

                var senders = (Array)sender;
                for (int i = 0; i < senders.Length; i++)
                {
                    SafeDispose(senders.GetValue(i));
                }

                #endregion
            }
            else if (sender is ICollection)
            {
                #region If is Collection

                foreach (object obj in sender as ICollection)
                {
                    SafeDispose(obj);
                }

                #endregion
            }
            else if (sender.GetType().IsGenericType)
            {
                if (sender.GetType().GetGenericTypeDefinition().Name == "Dictionary`2")
                {
                    #region If is Dictionary

                    var IDic = (IDictionary)sender;
                    foreach (object k in IDic.Keys)
                    {
                        SafeDispose(IDic[k]);
                    }

                    #endregion
                }
                else if (sender.GetType().GetGenericTypeDefinition().Name == "List`1")
                {
                    #region If is List

                    var iList = (IList)sender;
                    foreach (object obj in iList)
                    {
                        SafeDispose(obj);
                    }

                    #endregion
                }
            }
            else
            {
                InvokeDisposing(sender);
            }
        }
        catch (Exception ex)
        {
            string message = string.Format("{0} with following inner exception : {1}",
                ex.Message, ex.InnerException == null || String.IsNullOrEmpty(ex.InnerException.ToString()) ? "NULL" : ex.InnerException.ToString());
            Logger.WriteWarning(String.Format("Can not dispose {0} beacuse of : ", sender, message));
        }
    }
    
    private static void InvokeDisposing(object sender)
    {
        sender.GetType().GetMethod("Dispose").Invoke(sender, new object[] { });
    }
}