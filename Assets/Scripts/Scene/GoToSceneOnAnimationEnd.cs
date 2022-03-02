using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToSceneOnAnimationEnd : GoToScene
{
    public new Animation animation;    
    // Update is called once per frame
    void Update()
    {
        if(animation != null && !animation.isPlaying)
        {
            CallGoToScene();
        }
    }
}
