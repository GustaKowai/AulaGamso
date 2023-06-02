using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float movSpeed = 300f;

    public GameObject projPrefab;
    public Transform shootingPoint;
    public float projSpeed = 2f;

    public float shootCooldown = .2f;
    public float shootCounter;

    public float dashCooldown = 1.0f;

    public float dashCounter;

    public int lives = 3;
    SpriteRenderer player_SpriteRenderer;
    public Color alive = new Color(0f,1f,0f);
    public Color scratch = new Color (0.1f,0.9f,0.1f);
    public Color wound = new Color(0.3f, 0.7f, 0.3f);
    public Color fatal = new Color(0.5f,0.5f,0.5f);

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player_SpriteRenderer = GetComponent<SpriteRenderer>();
        player_SpriteRenderer.color = alive;

    }

    

    void Update()
    {
        // MOVIMENTAÇÃO // 
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        // if(Input.anyKeyDown){
        //     Debug.Log(input);
        // }
        rb.velocity = input * movSpeed * Time.deltaTime;

        // ROTACIONAR NA DIREÇÃO DO MOUSE //
        Vector2 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookAt = mouseScreenPosition - rb.position;
        float angle = Mathf.Atan2(lookAt.y, lookAt.x) * Mathf.Rad2Deg;
        rb.rotation = angle;

        // ATIRAR //
        if(Input.GetButtonDown("Fire1") && shootCounter <= 0){
            Atirar();
            shootCounter = shootCooldown;
        }

        if(shootCounter > 0){
            shootCounter -= Time.deltaTime;
        }
        
        // DASH //
        if(Input.GetButtonDown("Fire2") && dashCounter <= 0){
            
            dashCounter = dashCooldown;
            Dash();
        }

        if(movSpeed > 300f){
            Dash();
        }
        if(dashCounter>0){
            dashCounter -=Time.deltaTime;
        }

    }



    void Atirar(){
        GameObject projectile = Instantiate(projPrefab, shootingPoint.position, shootingPoint.rotation);
        projectile.GetComponent<Rigidbody2D>().AddForce(shootingPoint.up * projSpeed, ForceMode2D.Impulse);
    }

    void Dash(){
        if(dashCounter > 0.4){
            movSpeed = 1500f;
        }
        if(dashCounter < 0.4){
            movSpeed = 300f;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Enemy"){
            lives = lives-1;
            if(lives == 2) {
                player_SpriteRenderer.color = scratch;
           }
           if(lives == 1){
                player_SpriteRenderer.color = wound;
           }
            if(lives <= 0){
                player_SpriteRenderer.color = fatal;
                movSpeed = 0f;
                Destroy(gameObject,1f);
            }
        }
    }
}

    