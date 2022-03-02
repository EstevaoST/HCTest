    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToSceneOnCollision : GoToScene
{
    private void OnCollisionEnter(Collision collision)
    {
        CallGoToScene();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        CallGoToScene();
    }
    private void OnTriggerEnter(Collider other)
    {
        CallGoToScene();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CallGoToScene();
    }
}
