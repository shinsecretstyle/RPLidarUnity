using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public static class RplidarBinding {

    [DllImport("DLL")]
    public static extern int OnConnect(string opt_com_path, int opt_com_baudrate);
    //public static extern int OnConnect(string opt_com_path);
    [DllImport("DLL")]
    public static extern bool onDisconnect();

    [DllImport("DLL")]
    public static extern bool StartMotor();
    [DllImport("DLL")]
    public static extern bool EndMotor();

    [DllImport("DLL")]
    public static extern bool StartScan();
    [DllImport("DLL")]
    public static extern bool StartScanExpress(bool forcescan, int usingScanMode_);
    [DllImport("DLL")]
    public static extern bool EndScan();

    [DllImport("DLL")]
    public static extern bool ReleaseDrive();

    [DllImport("DLL")]
    public static extern bool GetLidarDataSize();

    [DllImport("DLL")]
    public static extern int GrabData(IntPtr ptr);


    public static int GetData(ref LidarData[] data)
    {
        GCHandle handler = GCHandle.Alloc(data, GCHandleType.Pinned);
        IntPtr structPtr = handler.AddrOfPinnedObject();
        int count = GrabData(structPtr);
        handler.Free();
        return count;
    }
}
