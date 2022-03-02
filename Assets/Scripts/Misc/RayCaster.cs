using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCaster : MonoBehaviour
{
    public LayerMask layer;
    public float lenght = 1;
    protected float lastUpdate = -1;
    protected bool hitted = false;
    protected RaycastHit2D hit = new RaycastHit2D();

    public RaycastHit2D? Hit
    {
        get
        {
            Raycast();
            if (hitted)
                return hit;
            else
                return null;
        }
    }

    protected virtual void Raycast()
    {
        if (lastUpdate < Time.time)
        {
            hit = Physics2D.Raycast(transform.position, transform.Forward2D(), lenght, layer.value);
            hitted = hit.collider != null;
            lastUpdate = Time.time;
        }
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, transform.Forward2D() * lenght);
    }
}
