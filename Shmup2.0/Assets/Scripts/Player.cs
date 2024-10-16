using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject bullet2;
    float cooldown;
    [SerializeField] float cooldownAmount;
    int hitPoints = 3;
    bool bulletColor = true;
    [SerializeField]float speed;
    Rigidbody2D rb;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        ;
        float horizontal = Input.GetAxis("Horizontal");
      rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);


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
            Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();
            rbBullet.velocity = new Vector2(0,1) ;
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
        if (!collision.gameObject.CompareTag("Bullet") || !collision.gameObject.CompareTag("Bullet2"))
        hitPoints--;
        if (hitPoints <= 0)
            Destroy(gameObject);
    }
}
