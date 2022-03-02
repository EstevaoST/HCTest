using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    public Vector2 relativeMovement;

    private Rigidbody2D rg;

    void Awake()
    {
        rg = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        rg.velocity = transform.InverseTransformDirection(relativeMovement);
    }
}
