using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToSceneOnDeath : GoToScene
{
    public GameCharacter target;    

    // Unity Events
    private void Awake()
    {
        target.OnDeath += this.CallGoToScene;
    }
}
