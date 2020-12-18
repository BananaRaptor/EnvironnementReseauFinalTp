using System.Collections;
using System.Collections.Generic;
using Mirror;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Boss : NetworkBehaviour
{
    [SerializeField]
    private float nextActionTime = 0.0f;
    private Rigidbody2D _rigidBody2D;
    private int phase = 1;
    [SerializeField]
    public GameObject CircleBullet;
    [SerializeField]
    public GameObject bulletPrefab;

    private bool voiceLineTime = false;

    [SerializeField] public int speed;
    public int health = 3;
    
    // Start is called before the first frame update
    [Server]
    void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        SoundManagerScript.playSound("ssethHere");
        InvokeRepeating("ShootBoss", nextActionTime, 2f);
    }

    [Server]
    void ShootBoss()
    {
        if (phase % 2 != 0)
        {
            shootCircle();
            Invoke("shootCircle",0.5f);
            if (voiceLineTime)
            {
                SoundManagerScript.playSound("ssethCircle");
            }
        }
        else
        {
            shootBasicBullet();
            Invoke("shootBasicBullet",0.5f);
            if (voiceLineTime)
            {
                SoundManagerScript.playSound("ssethLine");
            }
        }

        voiceLineTime = Random.Range(0, 2) == 1;
        phase++;
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

    private void shootBasicBullet()
    {
        for (float i = -4 ; i < 3; i += 0.4f ){
            CreateBullet((float) (i + 0.5), transform.position.y - 1 ,transform.position.z );
            SoundManagerScript.playSound("ennemyShoot");
        }
    }

    [Server]
    private void CreateBullet(float x, float y , float z)
    {
        var newPosition = new Vector3(x,y,z);
        var bullet = Instantiate(bulletPrefab, newPosition, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = Vector2.down * speed;
        NetworkServer.Spawn(bullet);
        Destroy(bullet, 40f);
    }
    

    [Server]
    private void shootCircle()
    {
        for (int i = 0; i < 140; i++){
            createCircleBullet();
        }
    }
    
    Vector3 RandomCircle ( Vector3 center ,   float radius ,float ang ){
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.z = center.z;
        return pos;
    }

    [Server]
    void createCircleBullet()
    {
        Vector3 center = transform.position;
        float ang = Random.value * 360;
        Vector3 pos = RandomCircle(center, 1f,ang);
        Quaternion rot = Quaternion.FromToRotation(Vector3.left, center-pos);
        var bullet = Instantiate(CircleBullet, pos, Quaternion.identity);
        //bullet.GetComponent<Rigidbody2D>().velocity = Vector2.down * 2;
        var x = 1f * Mathf.Sin(ang * Mathf.Deg2Rad);
        var y = 1f * Mathf.Cos(ang * Mathf.Deg2Rad);
        bullet.GetComponent<BossBullet>().direction = new Vector3(x ,y, pos.y);
        NetworkServer.Spawn(bullet);
        Destroy(bullet, 10f);
    }


    [Server]
    // Update is called once per frame
    void Update()
    {
        
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
