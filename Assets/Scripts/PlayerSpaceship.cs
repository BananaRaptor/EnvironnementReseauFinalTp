using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class PlayerSpaceship : NetworkBehaviour
{
    private Rigidbody2D _rigidBody2D;

    private Vector3 _moveVelocity;
    
    [SyncVar] public int health = 3;

    [SyncVar]public bool isDamageable = true;
    
    [SyncVar]public int life;
    
    [SyncVar]float timer = 0.0f;

    [SerializeField] 
    private int invulnerabilityPeriod = 3;
    
    [SerializeField]
    private float speed;
    
    [SerializeField]
    public GameObject spawner;

    [SerializeField] private float spriteBlinkingTotalTimer;
    [SerializeField] private double spriteBlinkingTotalDuration;
    private float spriteBlinkingTimer;
    private double spriteBlinkingMiniDuration;
    private bool startBlinking;

    public override void OnStartLocalPlayer (){
        //Camera.main.transform.SetParent(transform);
        //Camera.main.transform.localPosition = new Vector3(0,3,0);
        //Camera.main.transform.localEulerAngles = new Vector3(10f, 0, 0);
        
        
        
        _rigidBody2D = GetComponent<Rigidbody2D>();

    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(collision, GetComponent<Collider2D>());
        }
        else
        {
            Destroy(collision.gameObject);
            if (isDamageable)
            {
                health -= 1;
                isDamageable = false;
                SoundManagerScript.playSound("PlayerDamage");
            }
        }
    }



    private void SpriteBlinkingEffect()
    {
        spriteBlinkingTotalTimer += Time.deltaTime;
        if (spriteBlinkingTotalTimer >= spriteBlinkingTotalDuration)
        {
            startBlinking = false;
            spriteBlinkingTotalTimer = 0.0f;
            this.gameObject.GetComponent<SpriteRenderer>().enabled = true; // according to 
            //your sprite
            return;
        }

        spriteBlinkingTimer += Time.deltaTime;
        if (spriteBlinkingTimer >= spriteBlinkingMiniDuration)
        {
            spriteBlinkingTimer = 0.0f;
            if (this.gameObject.GetComponent<SpriteRenderer>().enabled == true)
            {
                this.gameObject.GetComponent<SpriteRenderer>().enabled = false; //make changes
            }
            else
            {
                this.gameObject.GetComponent<SpriteRenderer>().enabled = true; //make changes
            }
        }
    }

    private void Update()
    {
        
        if (isLocalPlayer)
        {

            if (!isDamageable)
            {
                SpriteBlinkingEffect();
                timer += Time.deltaTime;
                if (timer % 60 > invulnerabilityPeriod)
                {
                    timer = 0;
                    isDamageable = true;
                }
            }
            _moveVelocity = Vector3.zero;
            _moveVelocity.x = Input.GetAxisRaw("Horizontal");
            _moveVelocity.y = Input.GetAxisRaw("Vertical");

            _moveVelocity.Normalize();
            //Camera.main.transform.
            
            if (health == 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
    
    private void FixedUpdate()
    {
        if (isLocalPlayer)
        {
            if (Input.GetKey(KeyCode.Z))
            {
                _moveVelocity.x /= 2;
                _moveVelocity.y /= 2;
            }
            var velocity = _moveVelocity * Time.deltaTime * speed;
            var positionToEvaluate = transform.position + velocity;
            if (positionToEvaluate.x >  -2.6 && positionToEvaluate.x < 2.6)
            {
                if (positionToEvaluate.y > -4.5 && positionToEvaluate.y < 4.5)
                {
                    _rigidBody2D.MovePosition(transform.position + velocity);
                }
            }
        }
    }


}
