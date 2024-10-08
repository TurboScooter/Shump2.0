using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] float cooldown;
    [SerializeField] float cooldownAmount;
    int health = 3;
    
    void Start()
    {
        
    }

    void Update()
    {
        cooldown -= Time.deltaTime;
        Input.GetAxisRaw("Horizontal");
        Shoot();    
    }

    public void Shoot()
    {
        if(Input.GetKey(KeyCode.Space) && cooldown <= 0)
        {
            Instantiate(bullet, gameObject.transform.position, gameObject.transform.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.up ;
            cooldown = cooldownAmount;
        }
    }

    public void special()
    {
        if(Input.GetKeyDown(KeyCode.Z)) 
        {
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!gameObject.CompareTag("Bullet"))
        health--;
        if (health <= 0)
            Destroy(gameObject);
    }
}
