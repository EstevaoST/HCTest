using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToSceneOnAdFinished : GoToScene
{
    public AdvertisementManager target;
    void Awake()
    {        
        target.OnAdFinished += AdFinished;           
    }

    private void AdFinished()
    {
        CallGoToScene();
    }
}
