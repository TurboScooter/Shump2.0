using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject bullet2;
    float cooldown;
    [SerializeField] float cooldownAmount;
    int hitPoints = 3;
    bool bulletColor = true;
    
    void Start()
    {
        
    }

    void Update()
    {
        cooldown -= Time.deltaTime;
        Input.GetAxisRaw("Horizontal");
        Shoot();
        BulletChange();
    }

    public void Shoot()
    {
        if(Input.GetKey(KeyCode.Space) && cooldown <= 0 && bulletColor == true)
        {
            Instantiate(bullet, gameObject.transform.position, gameObject.transform.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.up ;
            cooldown = cooldownAmount;
        }
        else if(Input.GetKey(KeyCode.Space) && cooldown <= 0 && bulletColor == false)
        {
            Instantiate(bullet2, gameObject.transform.position, gameObject.transform.rotation);
            Rigidbody2D rb2 = bullet.GetComponent<Rigidbody2D>();
            rb2.velocity = Vector2.up;
            cooldown = cooldownAmount;
        }
    }

    public void BulletChange()
    {
        if(Input.GetKeyDown(KeyCode.Z)) 
        {
            if (bulletColor == true)
                bulletColor = false;
            else
                bulletColor = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!gameObject.CompareTag("Bullet"))
        hitPoints--;
        if (hitPoints <= 0)
            Destroy(gameObject);
    }
}
