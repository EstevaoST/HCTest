using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotation : MonoBehaviour
{
    public float rotateSpeed = 15;

    void FixedUpdate()
    {
        transform.Rotate(Vector3.forward, rotateSpeed * Time.fixedDeltaTime);
    }
}
