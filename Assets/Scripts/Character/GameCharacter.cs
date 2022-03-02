using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCharacter : Hittable
{
    // Input variables
    public Vector2 input;

    // Physics stats variables
    public float speed, acceleration = Mathf.Infinity, airAcceleration = 10;
    [Range(0, 1)]
    public float friction = 0.9f;
    public float jumpForce = 5;

    public float hurtTime;
    public bool invulnerableWhileHurt = false;
    private float hurtTimer; 

    // Character state variables
    public Vector2 movement, physics;
    public RayCaster groundedRay;
    public bool grounded = true;

    // References
    private Rigidbody2D rg;
    private Animator anim;

    // Events
    public System.Action OnKill = null;

    // Constants
    private const float SNAP_TO_GROUND_FORCE = 5;
    private const float GRAVITY_MULTIPLIER = 4;

    // Unity Events
    void Awake()
    {
        // Get references
        rg = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
    }
    protected override void Start()
    {
        // Initialize values
        base.Start();
        hurtTimer = 0;
    }
    void FixedUpdate()
    {
        // Update hurt control
        if(hurtTimer > 0)
        {
            hurtTimer -= Time.fixedDeltaTime;
        }

        // Update physics related control
        bool lastGrounded = grounded;
        grounded = physics.y <= 0 && groundedRay.Hit.HasValue;
        if (grounded)
        {
            physics.y = -SNAP_TO_GROUND_FORCE * (groundedRay.Hit?.distance ?? 0);
        }
        else
        {
            physics.y = Mathf.MoveTowards(physics.y, Physics.gravity.y * GRAVITY_MULTIPLIER, Mathf.Abs(Physics.gravity.y * Time.fixedDeltaTime) * GRAVITY_MULTIPLIER);
        }
        physics.x = Mathf.MoveTowards(physics.x, 0, GRAVITY_MULTIPLIER * Time.fixedDeltaTime);

        // Update input related control
        if (Alive && input.x != 0)
        {
            movement.x = Mathf.MoveTowards(movement.x, input.x * speed, grounded ? acceleration : airAcceleration);
        }
        else
        {
            movement.x = movement.x * friction;
        }     

        // Apply control on Rigidbody
        // Applying directly on velocity is not a good practice, but allows us to know exactly what force is from movement or physics
        rg.velocity = movement + physics;
        if(Alive && grounded && movement.x != 0 && Mathf.Sign(transform.Forward2D().x) != Mathf.Sign(movement.x))
        {
            // If we are not looking the way we are moving, flip around
            transform.Rotate(0, 180, 0);
        }

        // Apply on Mecanim
        anim.SetFloat(CharacterMecanimParameters.MoveHorz.Hash(), Mathf.Abs(movement.x));
        anim.SetFloat(CharacterMecanimParameters.MoveVert.Hash(), movement.y / GRAVITY_MULTIPLIER);
        anim.SetBool(CharacterMecanimParameters.Grounded.Hash(), grounded);
        if (lastGrounded && !grounded)
        {
            anim.SetTrigger(CharacterMecanimParameters.Air.Hash());
        }
        anim.SetBool(CharacterMecanimParameters.Hurt.Hash(), hurtTimer > 0);
    }

    // Hittable Events    
    public override void Damaged(int damage)
    {
        // Ignore damage if is invulnerable
        if (invulnerableWhileHurt && hurtTimer > 0)
            return;
        
        base.Damaged(damage);
        if (Alive)
        {
            hurtTimer = hurtTime;
        }        
    }
    public override void Died()
    {
        // Apply on Mecanim
        anim.SetBool(CharacterMecanimParameters.Alive.Hash(), this.Alive);
        gameObject.SetLayerRecursively((int)CharacterLayer.Corpse);

        base.Died();
    }
    public override void Knockbacked(Vector2 knockback)
    {
        if (Alive)
        {
            this.physics = knockback;
        }
    }

    // Inputs methods
    public void InputMove(float move)
    {
        input.x = move;
    }
    public void InputMelee()
    {
        if (Alive)
        {
            anim.SetTrigger(CharacterMecanimParameters.Meleed.Hash());
        }
    }
    public void InputShoot()
    {
        // Verify if we can perform the action
        if (Alive)
        {
            anim.SetTrigger(CharacterMecanimParameters.Shooted.Hash());
        }
    }
    public void InputJump()
    {
        // Verify if we can perform the action
        if (Alive && grounded && physics.y <= 0)
        {
            physics.y = jumpForce;
        }
    }

    public void SpawnDamageArea(DamageArea area)
    {
        DamageArea spawn = Instantiate(area.gameObject).GetComponent<DamageArea>();

        spawn.CharacterLayer = this.CharacterLayer.GetEnemyDamageLayer();               
        spawn.transform.position = transform.position + spawn.transform.position;
        spawn.transform.RotateAround(transform.position, transform.Forward2D(), transform.rotation.eulerAngles.x);
        spawn.transform.RotateAround(transform.position, transform.up, transform.rotation.eulerAngles.y);

        spawn.enabled = true;
        spawn.OnHit += DamageDealt;
    }

    private void DamageDealt(bool killed)
    {
        if (killed)
            OnKill?.Invoke();
    }
}
