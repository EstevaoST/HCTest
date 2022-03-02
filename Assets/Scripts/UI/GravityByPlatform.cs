using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;

public class GravityByPlatform : MonoBehaviour
{
    public MoveController target;
    public float desktopValue = 0;
    public float mobileValue = 1;
    // Start is called before the first frame update
    void Awake()
    {
        switch (Application.platform)
        {
            case RuntimePlatform.OSXEditor:                
            case RuntimePlatform.OSXPlayer:                
            case RuntimePlatform.WindowsPlayer:                            
            case RuntimePlatform.WindowsEditor:
            case RuntimePlatform.LinuxPlayer:
            case RuntimePlatform.LinuxEditor:
            case RuntimePlatform.WebGLPlayer:
                SetValue(desktopValue);
                break;
            case RuntimePlatform.IPhonePlayer:                            
            case RuntimePlatform.Android:
                SetValue(mobileValue);
                break;
        }
    }

    private void SetValue(float value)
    {
        if (target != null)
        {
            target.gravity = value;
        }
        else
            Debug.LogError("Target not set");
    }
}
