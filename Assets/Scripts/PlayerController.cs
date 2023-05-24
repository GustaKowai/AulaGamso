using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float movSpeed = 500f;

    public GameObject projPrefab;
    public Transform shootingPoint;
    public float projSpeed = 20f;

    public float shootCooldown = .2f;
    public float shootCounter;

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
        
    }

    void Atirar(){
        GameObject projectile = Instantiate(projPrefab, shootingPoint.position, shootingPoint.rotation);
        projectile.GetComponent<Rigidbody2D>().AddForce(shootingPoint.up * projSpeed, ForceMode2D.Impulse);
    }
}
