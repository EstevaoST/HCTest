using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiRayCaster : RayCaster
{
    public float width = 1;

    public Vector2[] positions => new Vector2[] {
        transform.position - transform.up * width * 0.5f,
        transform.position,
        transform.position + transform.up * width * 0.5f
    };

    protected override void Raycast()
    {
        if (lastUpdate < Time.time)
        {
            // Reseta os dados
            hitted = false;            
            hit.distance = Mathf.Infinity;

            // Prepara objetos para os testes
            int hitCount = 0;
            Vector2[] positions = this.positions;
            RaycastHit2D auxHit = new RaycastHit2D();

            // Aplica raycast pra cada posição
            foreach (Vector2 pos in positions)
            {
                auxHit = Physics2D.Raycast(pos, transform.Forward2D(), lenght, layer.value);
                if (auxHit.collider != null && auxHit.distance < hit.distance)
                {
                    hit = auxHit;
                    hitCount++;
                }
            }

            // Aplica os dados
            hitted = hitCount > 0;                        
            lastUpdate = Time.time;
        }
    }

    protected override void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        foreach (Vector3 pos in positions)
        {
            Gizmos.DrawRay(pos, transform.Forward2D() * lenght);
        }        
    }
}
