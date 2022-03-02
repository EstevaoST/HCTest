using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class GoToScene : MonoBehaviour
{
    public GameData.GameScenes sceneToGo;
    public LoadSceneMode mode = LoadSceneMode.Single;
    public float delay = 0;
    
    public void CallGoToScene()
    {
        if(delay <= 0)
            SceneManager.LoadScene((int)sceneToGo, mode);
        else
            StartCoroutine(DelayedGoToScene());
    }

    protected IEnumerator DelayedGoToScene()
    {
        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene((int)sceneToGo, mode);
    }

}
