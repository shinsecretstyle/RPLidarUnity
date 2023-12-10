using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
public struct LidarData
{
    public byte syncBit;
    public float theta;
    public float distant;
    public uint quality;
};

