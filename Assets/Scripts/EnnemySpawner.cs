using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnnemySpawner : NetworkBehaviour
{
    [SerializeField]
    private GameObject ennemy;
    [SerializeField]
    private GameObject Boss;
    private float randx;
    private float randy;
    private Vector2 whereToSpawn;
    [SerializeField]
    public int numberOfPlayer;
    [SerializeField]
    public float spawnRate;

    private int waveNumber = 3;

    [SerializeField] private int testWave;

    public float nextSpawn = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    
    [Server]
    // Update is called once per frame
    void Update()
    {
        if (isServer)
        {
            if (GameObject.FindGameObjectsWithTag("Ennemy").Length == 0 &&
                GameObject.FindGameObjectsWithTag("Player").Length == numberOfPlayer && waveNumber != 10)
            {
                if (testWave != 10){
                    GenerateWave(testWave);
                }
                else{
                    GenerateWave(waveNumber);
                }
                waveNumber++;
            }

            if (GameObject.FindGameObjectsWithTag("Player").Length == 0)
            {
                foreach (var ennemy in GameObject.FindGameObjectsWithTag("Ennemy"))
                {
                    Destroy(ennemy);
                }

                waveNumber = 0;
            }
        }
    } 
    
    [Server]
    private void GenerateWave(int wave)
    {
        if (isServer)
        {
            switch (wave)
            {
                case 0:
                    NetworkServer.Spawn(Instantiate(ennemy, new Vector2(-2.4f, 4.5f), Quaternion.identity));
                    NetworkServer.Spawn(Instantiate(ennemy, new Vector2(-2.1f, 4.5f), Quaternion.identity));
                    NetworkServer.Spawn(Instantiate(ennemy, new Vector2(-1.8f, 4.5f), Quaternion.identity));
                    NetworkServer.Spawn(Instantiate(ennemy, new Vector2(-1.5f, 4.5f), Quaternion.identity));
                    NetworkServer.Spawn(Instantiate(ennemy, new Vector2(-1.2f, 4.5f), Quaternion.identity));
                    break;
                case 1:
                    NetworkServer.Spawn(Instantiate(ennemy, new Vector2(-2.1f, 4.5f), Quaternion.identity));
                    NetworkServer.Spawn(Instantiate(ennemy, new Vector2(-1.8f, 4.5f), Quaternion.identity));
                    NetworkServer.Spawn(Instantiate(ennemy, new Vector2(-1.5f, 4.5f), Quaternion.identity));
                    NetworkServer.Spawn(Instantiate(ennemy, new Vector2(-1.2f, 4.5f), Quaternion.identity));
                    NetworkServer.Spawn(Instantiate(ennemy, new Vector2(-0.9f, 4.5f), Quaternion.identity));
                    NetworkServer.Spawn(Instantiate(ennemy, new Vector2(-0.6f, 4.5f), Quaternion.identity));
                    NetworkServer.Spawn(Instantiate(ennemy, new Vector2(-0.3f, 4.5f), Quaternion.identity));
                    NetworkServer.Spawn(Instantiate(ennemy, new Vector2(-0f, 4.5f), Quaternion.identity));
                    NetworkServer.Spawn(Instantiate(ennemy, new Vector2(2.1f, 4.5f), Quaternion.identity));
                    NetworkServer.Spawn(Instantiate(ennemy, new Vector2(1.8f, 4.5f), Quaternion.identity));
                    NetworkServer.Spawn(Instantiate(ennemy, new Vector2(1.5f, 4.5f), Quaternion.identity));
                    NetworkServer.Spawn(Instantiate(ennemy, new Vector2(1.2f, 4.5f), Quaternion.identity));
                    NetworkServer.Spawn(Instantiate(ennemy, new Vector2(0.9f, 4.5f), Quaternion.identity));
                    NetworkServer.Spawn(Instantiate(ennemy, new Vector2(0.6f, 4.5f), Quaternion.identity));
                    NetworkServer.Spawn(Instantiate(ennemy, new Vector2(0.3f, 4.5f), Quaternion.identity));
                    break;
                case 2:
                   NetworkServer.Spawn(Instantiate(ennemy, new Vector2(-2.1f, 4.5f), Quaternion.identity));
                   NetworkServer.Spawn(Instantiate(ennemy, new Vector2(-1.8f, 4.5f), Quaternion.identity));
                   NetworkServer.Spawn(Instantiate(ennemy, new Vector2(-1.5f, 4.5f), Quaternion.identity));
                   NetworkServer.Spawn(Instantiate(ennemy, new Vector2(-1.2f, 4.5f), Quaternion.identity));
                   NetworkServer.Spawn(Instantiate(ennemy, new Vector2(-0.9f, 4.5f), Quaternion.identity));
                   NetworkServer.Spawn(Instantiate(ennemy, new Vector2(-0.6f, 4.5f), Quaternion.identity));
                   NetworkServer.Spawn(Instantiate(ennemy, new Vector2(-0.3f, 4.5f), Quaternion.identity));
                   NetworkServer.Spawn(Instantiate(ennemy, new Vector2(-0f, 4.5f), Quaternion.identity));
                   NetworkServer.Spawn(Instantiate(ennemy, new Vector2(2.1f, 4.5f), Quaternion.identity));
                   NetworkServer.Spawn(Instantiate(ennemy, new Vector2(1.8f, 4.5f), Quaternion.identity));
                   NetworkServer.Spawn(Instantiate(ennemy, new Vector2(1.5f, 4.5f), Quaternion.identity));
                   NetworkServer.Spawn(Instantiate(ennemy, new Vector2(1.2f, 4.5f), Quaternion.identity));
                   NetworkServer.Spawn(Instantiate(ennemy, new Vector2(0.9f, 4.5f), Quaternion.identity));
                   NetworkServer.Spawn(Instantiate(ennemy, new Vector2(0.6f, 4.5f), Quaternion.identity));
                   NetworkServer.Spawn(Instantiate(ennemy, new Vector2(0.3f, 4.5f), Quaternion.identity));

                   NetworkServer.Spawn(Instantiate(ennemy, new Vector2(-2.1f, 4f), Quaternion.identity));
                   NetworkServer.Spawn(Instantiate(ennemy, new Vector2(-1.8f, 4f), Quaternion.identity));
                   NetworkServer.Spawn(Instantiate(ennemy, new Vector2(-1.5f, 4f), Quaternion.identity));
                   NetworkServer.Spawn(Instantiate(ennemy, new Vector2(-1.2f, 4f), Quaternion.identity));
                   NetworkServer.Spawn(Instantiate(ennemy, new Vector2(-0.9f, 4f), Quaternion.identity));
                   NetworkServer.Spawn(Instantiate(ennemy, new Vector2(-0.6f, 4f), Quaternion.identity));
                   NetworkServer.Spawn(Instantiate(ennemy, new Vector2(-0.3f, 4f), Quaternion.identity));
                   NetworkServer.Spawn(Instantiate(ennemy, new Vector2(-0f, 4f), Quaternion.identity));
                   NetworkServer.Spawn(Instantiate(ennemy, new Vector2(2.1f, 4f), Quaternion.identity));
                   NetworkServer.Spawn(Instantiate(ennemy, new Vector2(1.8f, 4f), Quaternion.identity));
                   NetworkServer.Spawn(Instantiate(ennemy, new Vector2(1.5f, 4f), Quaternion.identity));
                   NetworkServer.Spawn(Instantiate(ennemy, new Vector2(1.2f, 4f), Quaternion.identity));
                   NetworkServer.Spawn(Instantiate(ennemy, new Vector2(0.9f, 4f), Quaternion.identity));
                   NetworkServer.Spawn(Instantiate(ennemy, new Vector2(0.6f, 4f), Quaternion.identity));
                   NetworkServer.Spawn(Instantiate(ennemy, new Vector2(0.3f, 4f), Quaternion.identity));
                    break;
                case 3:
                    NetworkServer.Spawn(Instantiate(Boss, new Vector2(0f, 3.5f), Quaternion.identity));
                    break;

            }
        }
    }
}
