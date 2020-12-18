using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class ShootBullet : NetworkBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float speed;
    void Start()
    {
        
    }

    
    void FixedUpdate()
    {
        if (isLocalPlayer && Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    
    [Command]
    private void Shoot()
    {
        var newPosition = new Vector3(transform.position.x, transform.position.y +1, transform.position.z);
        var bullet = NetworkManager.Instantiate(bulletPrefab, newPosition, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
        NetworkServer.Spawn(bullet);
        Destroy(bullet, 1f);
        SoundManagerScript.playSound("playerShoot");
    }
}