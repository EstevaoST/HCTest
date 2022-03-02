using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawWallMovement : MonoBehaviour
{
    public Vector2 startingMovement;
    public Vector2 targetMovement;
    public float acceleration;
    public Vector2 onHitMovement;

    private Rigidbody2D rg;


    void Awake()
    {
        rg = GetComponent<Rigidbody2D>();
        rg.velocity = transform.InverseTransformDirection(startingMovement);
    }

    void FixedUpdate()
    {
        rg.velocity = Vector2.MoveTowards(rg.velocity, transform.InverseTransformDirection(targetMovement), acceleration * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        rg.velocity = transform.InverseTransformDirection(onHitMovement);
    }
}
