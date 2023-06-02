using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    private int enemyLives = 3;
    // Start is called before the first frame update
    void Start()
    { 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "projectile"){
           enemyLives = enemyLives-1;
           if(enemyLives<=0){
            Destroy(gameObject);
           } 
        }
    }
}
