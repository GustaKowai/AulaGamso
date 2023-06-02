using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float movSpeed = 500f;

    public GameObject projPrefab;
    public Transform shootingPoint;
    public float projSpeed = 2f;

    public float shootCooldown = .2f;
    public float shootCounter;

    public float dashCooldown = 1.0f;

    public float dashCounter;

    public int lives = 3;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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

        if(movSpeed > 500f){
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
            movSpeed = 500f;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Enemy"){
            lives = lives-1;
            if(lives <= 0){
                Destroy(gameObject);
            }
        }
    }
}

    