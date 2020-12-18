using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using Mirror;
using UnityEngine;

public class BasicEnnemy : NetworkBehaviour
{
    [SyncVar]
    [SerializeField]private float nextActionTime = 0.0f;
    [SyncVar]
    [SerializeField]public float period = 0.1f;
    private bool left;
    private Vector3 _moveVelocity;
    private Rigidbody2D _rigidBody2D;

    [SyncVar]public int health = 3;

    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] public int speed;
    [SerializeField] public int tilt;
    
    [Server]
    // Start is called before the first frame update
    void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        InvokeRepeating("ShootBulletEnnemy", nextActionTime, 1f);
    }

    // Update is called once per frame
    [Server]
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
                    _moveVelocity.x = -5;
                    _moveVelocity.y = 0;
                    _moveVelocity.Normalize();

                    var velocity = _moveVelocity * Time.deltaTime * speed;
                    var positionToEvaluate = transform.position + velocity;

                    _rigidBody2D.MovePosition(transform.position + velocity);


                }
                else
                {
                    _moveVelocity = Vector3.zero;
                    _moveVelocity.x = 5;
                    _moveVelocity.y = 0;
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

            if (health <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    [Server]
    private void ShootBulletEnnemy()
    {
        CreateBullet(transform.position.x, transform.position.y - 1 ,transform.position.z );
        SoundManagerScript.playSound("ennemyShoot");
    }

    [Server]
    private void CreateBullet(float x, float y , float z)
    {
        var newPosition = new Vector3(x,y,z);
        var bullet = Instantiate(bulletPrefab, newPosition, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = Vector2.down * speed;
        NetworkServer.Spawn(bullet);
        Destroy(bullet, 20f);
    }

    [Server]
    void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.CompareTag("Bullet"))
        {
            health -= 1;
        }
        else if (collision.CompareTag("Ennemy") || collision.gameObject.CompareTag("EnnemyBullet") )
        {
            Physics2D.IgnoreCollision(collision, GetComponent<Collider2D>());
        }
    }
    
}
