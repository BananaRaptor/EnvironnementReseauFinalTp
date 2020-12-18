using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class EnnemyBullet : NetworkBehaviour
{
    private Rigidbody2D _rigidBody2D;
    private float nextActionTime = 0.0f;
    private bool left;
    [SerializeField][SyncVar]
    public float period = 0.1f;
    [SerializeField][SyncVar]
    public float speed;
    private Vector3 _moveVelocity;

    void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }
    
    [Server]
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
    
    [Server]
    [ClientRpc]
    // Update is called once per frame
    void Update()
    {
        if (isServer)
        {
            if (Time.time > nextActionTime)
            {
                nextActionTime += period;
                if (left)
                {
                    _moveVelocity = Vector3.zero;
                    _moveVelocity.x = -1;
                    _moveVelocity.y = -1;
                    _moveVelocity.Normalize();

                    var velocity = _moveVelocity * Time.deltaTime * speed;
                    var positionToEvaluate = transform.position + velocity;

                    _rigidBody2D.MovePosition(transform.position + velocity);


                }
                else
                {
                    _moveVelocity = Vector3.zero;
                    _moveVelocity.x = 1;
                    _moveVelocity.y = -1;
                    _moveVelocity.Normalize();

                    var velocity = _moveVelocity * Time.deltaTime * speed;
                    var positionToEvaluate = transform.position + velocity;

                    _rigidBody2D.MovePosition(transform.position + velocity);
                }


            }
            else
            {
                nextActionTime = 0;
                left = !left;
            }
        }

    }
}
