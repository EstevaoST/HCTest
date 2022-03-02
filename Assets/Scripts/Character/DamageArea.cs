using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    // Variables
    public int damage = 1;
    public Vector2 knockback = Vector2.zero;
    public float duration = 0.1f;
    private Collider2D[] colliders = null;

    // Properties
    public CharacterLayer CharacterLayer
    {
        get
        {
            if (Enum.IsDefined(typeof(CharacterLayer), gameObject.layer))
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

    // Unity Events
    private void Awake()
    {
        colliders = GetComponentsInChildren<Collider2D>();
    }
    private void OnEnable()
    {
        foreach(Collider2D c in colliders) 
        {
            c.enabled = true;
        }
    }
    private void OnDisable()
    {
        foreach (Collider2D c in colliders)
        {
            c.enabled = false;
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Hit(collision.GetComponent<Hittable>());
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        Hit(collision.collider.GetComponent<Hittable>());
    }
    public void OnTriggerEnter(Collider other)
    {
        Hit(other.GetComponent<Hittable>());
    }
    public void OnCollisionEnter(Collision collision)
    {
        Hit(collision.collider.GetComponent<Hittable>());
    }

    private void FixedUpdate()
    {
        if (duration > 0)
        {
            duration -= Time.fixedDeltaTime;
            if (duration <= 0)
                Destroy(gameObject);
        }
    }

    // Damage Area events
    private void Hit(Hittable h)
    {
        if (h?.Alive ?? false)
        {
            h?.Damaged(damage);
            h?.Knockbacked(knockback);
        }
    }
}
