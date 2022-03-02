using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hittable : MonoBehaviour
{
    // Stats variables
    public int maxHealth = 3, health;

    // properties
    public bool Alive => health > 0;
    public bool Dead => health <= 0;

    // Events
    public System.Action OnDeath = null;

    // Properties
    public CharacterLayer CharacterLayer
    {
        get
        {
            if(Enum.IsDefined(typeof(CharacterLayer), gameObject.layer))
            {
                return (CharacterLayer)gameObject.layer;
            }
            return CharacterLayer.Enemy;
        }
        set
        {
            gameObject.layer = (int)value;
        }
    }

    // Methods
    public virtual void Damaged(int damage)
    {
        if (health > 0)
        {
            health -= damage;
            if (health <= 0)
            {
                Died();                
            }
        }
    }
    public virtual void Knockbacked(Vector2 knockback)
    {
        // By default Hittable does nothing here
    }
    public virtual void Died()
    {
        health = 0;
        OnDeath?.Invoke();
    }

    // Unity Events
    protected virtual void Start()
    {
        health = maxHealth;
    }

    
}
