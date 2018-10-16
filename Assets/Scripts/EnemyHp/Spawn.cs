using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

    public Transform[] SpawnPoint;
    public float SpawnTimer;
    public float Respawn;
    public GameObject Zombie;
    public Hpbar playerhealth;
   

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("Spawnner", SpawnTimer, Respawn);
    }

    
    void Spawnner()
    {
        if(playerhealth.CurrentHp<=0)
        {
            return;
        }
        int spawnindex = Random.Range(0, SpawnPoint.Length);// allocates the gas to each spawn points randomly in the array
        
        Instantiate(Zombie, SpawnPoint[spawnindex].position, SpawnPoint[spawnindex].rotation);
        
    }

}
