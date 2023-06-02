using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public float enemySpeed = 4f;
    GameObject player;
    bool isAlive = true;
    SpriteRenderer enemy_SpriteRenderer;

    public Color orange = new Color(1.0f, 0.64f, 0.0f);

    private int enemyLives = 3;
    // Start is called before the first frame update
    void Start()
    { 
        player = GameObject.FindWithTag("Player");
        enemy_SpriteRenderer = GetComponent<SpriteRenderer>();
        enemy_SpriteRenderer.color = Color.grey;

    }

    // Update is called once per frame
    void Update()
    {
        if (player != null && isAlive){
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemySpeed*Time.deltaTime);
        }
        if(enemySpeed < 30f){
            enemySpeed += 0.1f;
        }
        
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "projectile"){
           enemyLives = enemyLives-1;
           enemySpeed -=2.0f;
           if(enemyLives == 2) {
            enemy_SpriteRenderer.color = Color.yellow;
           }
           if(enemyLives == 1){
            enemy_SpriteRenderer.color = orange;
           }
           if(enemyLives<=0){
            isAlive = false;
            enemy_SpriteRenderer.color = Color.red;
            Destroy(gameObject,2f);
           } 
        }
        if(other.gameObject.tag == "Player"){
            enemySpeed = -enemySpeed;
        }
    }
}
