﻿/*
 * Copyright (c) BaziPardaz.Co Ltd. All rights reserved.
 * 
 * File Name        : DisplayResolutions.cs
 * File Description : The display resolutions
 * Generated by     : Pooya Eimandar
 * Last modified by : Pooya Eimandar on 10/1/2013
 * Comment          : 
 */

using System.Collections.Generic;
using System.Runtime.InteropServices;

public class DisplayResolutions
{
    const int ENUM_CURRENT_SETTINGS = -1;
    const int ENUM_REGISTRY_SETTINGS = -2;

    [DllImport("user32.dll")]
    public static extern bool EnumDisplaySettings(string deviceName, int modeNum, ref DisplayResolutionInfo devMode);

    [StructLayout(LayoutKind.Sequential)]
    public struct DisplayResolutionInfo
    {
        const int CCHDEVICENAME = 0x20;
        const int CCHFORMNAME = 0x20;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
        public string dmDeviceName;
        public short dmSpecVersion;
        public short dmDriverVersion;
        public short dmSize;
        public short dmDriverExtra;
        public int dmFields;
        public int dmPositionX;
        public int dmPositionY;
        public System.Windows.Forms.ScreenOrientation dmDisplayOrientation;
        public int dmDisplayFixedOutput;
        public short dmColor;
        public short dmDuplex;
        public short dmYResolution;
        public short dmTTOption;
        public short dmCollate;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
        public string dmFormName;
        public short dmLogPixels;
        public int dmBitsPerPel;
        public int dmPelsWidth;
        public int dmPelsHeight;
        public int dmDisplayFlags;
        public int dmDisplayFrequency;
        public int dmICMMethod;
        public int dmICMIntent;
        public int dmMediaType;
        public int dmDitherType;
        public int dmReserved1;
        public int dmReserved2;
        public int dmPanningWidth;
        public int dmPanningHeight;
    }

    public static List<DisplayResolutionInfo> Resolutions = new List<DisplayResolutionInfo>();

    public static void GetAvailableList()
    {
        var vDevMode = new DisplayResolutionInfo();
        int i = 0;
        while (EnumDisplaySettings(null, i, ref vDevMode))
        {
            Resolutions.Add(vDevMode);
            i++;
            //var str = string.Format("Width:{0} Height:{1} Color:{2} Frequency:{3}",
            //                        vDevMode.dmPelsWidth,
            //                        vDevMode.dmPelsHeight,
            //                        1 << vDevMode.dmBitsPerPel,
            //                        vDevMode.dmDisplayFrequency
            //                    );
        }
    }
}