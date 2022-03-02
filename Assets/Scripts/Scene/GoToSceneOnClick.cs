using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToSceneOnClick : MonoBehaviour
{
    public GameData.GameScenes sceneToGo;
    public LoadSceneMode mode = LoadSceneMode.Single;

    public void Click()
    {

        SceneManager.LoadScene((int)sceneToGo, mode);
    }
}
