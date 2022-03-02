using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAI : MonoBehaviour
{
    public GameCharacter target;
    public RayCaster vision;
    public float attackDistance = 2;
    public float attackDelay = 1;
    private float attackTimer;

    // Update is called once per frame
    void FixedUpdate()
    {
        // if target is in vision
        if(vision.Hit.HasValue)
        {
            // if we are near to the target
            if ((vision.Hit?.distance ?? Mathf.Infinity) < 1)
            {
                // countdown to attack
                attackTimer -= Time.fixedDeltaTime;
                if (attackTimer <= 0)
                {
                    target.InputMelee();
                    attackTimer = attackDelay;
                }
            }
            // else if we are far, approach
            else
            {
                attackTimer = attackDelay;
                target.input = target.transform.Forward2D();
            }                
        }

        
    }
}
