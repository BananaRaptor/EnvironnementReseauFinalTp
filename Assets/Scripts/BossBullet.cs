using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class BossBullet : NetworkBehaviour
{
    private Rigidbody2D _rigidBody2D;
    private float nextActionTime = 0.0f;
    private bool left;

    public Vector3 direction;
    [SerializeField]
    public float period = 0.1f;
    [SerializeField]
    public float speed;
    private Vector2 _moveVelocity;
    
    

    void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            
            //Destroy(this);
        }
        else
        {
            Physics2D.IgnoreCollision(collision, GetComponent<Collider2D>());   
        }
    }
    
    // Update is called once per frame
    [Server]
    void Update()
    {
        var velocity = direction * Time.deltaTime * speed;

        _rigidBody2D.MovePosition(transform.position + velocity );
        
    }
}