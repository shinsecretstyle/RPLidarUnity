using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{





    public void startMotor()
    {
        if (RplidarBinding.StartMotor())
        {
            Debug.Log("start motor succeed");
        }
        else Debug.Log("start motor failed");
    }
    public void stopMotor() 
    {
        if (RplidarBinding.EndMotor())
        {
            Debug.Log("end motor succeed");
        }
        else Debug.Log("end motor failed");
    }
    public void startScan()
    {
        if (RplidarBinding.StartScan())
        {
            Debug.Log("start scan succeed");
        }
        else Debug.Log("start scan failed");
    }
    public void stopScan()
    {
        if (RplidarBinding.EndScan())
        {
            Debug.Log("end scan succeed");
        }
        else Debug.Log("end scan failed");
    }
    public void startUpdateData()
    {
        RplidarMap.updatingData = true;
    }

    public void stopUpdateData()
    {
        RplidarMap.updatingData = false;
    }
}

