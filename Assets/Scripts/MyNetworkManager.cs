using System;
using UnityEngine;
using Mirror;

public class MyNetworkManager : NetworkManager 
{
    [SerializeField]
    public GameObject spawner;

    public override void OnStartServer()
    {
        base.OnStartServer();
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        
        base.OnClientConnect(conn);
            
        if (GameObject.FindGameObjectsWithTag("Player").Length == 2 && GameObject.FindGameObjectsWithTag("Spawner").Length == 0 )
        {
            Invoke("CreateSpawner", 2);
        }
        
    }
    

    [Server]
    void CreateSpawner()
    {
        var test = Instantiate(spawner);
        NetworkServer.Spawn(test);
    }



}