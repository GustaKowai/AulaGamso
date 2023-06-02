using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{

    public Transform[] spawnPoints;

    public GameObject enemy;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemies", 2f,8f );
    }

    void SpawnEnemies(){
        int randIndex = Random.Range(0,spawnPoints.Length);
        Instantiate(enemy, spawnPoints[randIndex].position, Quaternion.identity);
    }    
    
}
