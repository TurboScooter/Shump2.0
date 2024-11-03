using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject electroBullet;
    [SerializeField] GameObject cryoBullet;
    [SerializeField] GameObject pyroBullet;
    [SerializeField] GameObject hydroBullet;
    [SerializeField] GameObject anemoBullet;
    [SerializeField] GameObject geoBullet;
    [SerializeField] GameObject dendroBullet;
    int currentElement = 1;
    float cooldown;
    [SerializeField] float cooldownAmount;
    [SerializeField] int hitPoints = 3;
    bool bulletColor = true;
    [SerializeField]float speed;
    Rigidbody2D rb;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        
      float horizontal = Input.GetAxis("Horizontal");
      rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);


    }
    void Update()
    {

        cooldown -= Time.deltaTime;
        Input.GetAxisRaw("Horizontal");
        Shoot();
        ElementChange();
    }

    public void Shoot()
    {
        if (Input.GetKey(KeyCode.Space) && cooldown <= 0 && currentElement == 1)
        {
            Instantiate(cryoBullet, transform.position, transform.rotation);
            cooldown = cooldownAmount;
        }//cryo
        else if (Input.GetKey(KeyCode.Space) && cooldown <= 0 && currentElement == 2)
        {
            Instantiate(hydroBullet, transform.position, transform.rotation);
            cooldown = cooldownAmount;
        }//hydro
        else if (Input.GetKey(KeyCode.Space) && cooldown <= 0 && currentElement == 3)
        {
            Instantiate(dendroBullet, transform.position, transform.rotation);
            cooldown = cooldownAmount;
        }//dendro
        else if (Input.GetKey(KeyCode.Space) && cooldown <= 0 && currentElement == 4)
        {
            Instantiate(electroBullet, transform.position, transform.rotation);
            cooldown = cooldownAmount;
        }//electro
        else if (Input.GetKey(KeyCode.Space) && cooldown <= 0 && currentElement == 5)
        {
            Instantiate(geoBullet, transform.position, transform.rotation);
            cooldown = cooldownAmount;
        }//geo
        else if (Input.GetKey(KeyCode.Space) && cooldown <= 0 && currentElement == 6)
        {
            Instantiate(anemoBullet, transform.position, transform.rotation);
            cooldown = cooldownAmount;
        }//anemo
        else if (Input.GetKey(KeyCode.Space) && cooldown <= 0 && currentElement == 7)
        {
            Instantiate(pyroBullet, transform.position, transform.rotation);
            cooldown = cooldownAmount;
        }//pyro
    }

    public void ElementChange()
    {
        if(Input.GetKeyDown(KeyCode.Z)) 
        {
            currentElement += 1;
        }
        if(Input.GetKeyDown(KeyCode.X))
        { 
            currentElement -= 1; 
        }
        if(currentElement > 7)
        {
            currentElement = 1;
        }
        if(currentElement < 1)
        {
            currentElement = 7;
        }
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentElement = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentElement = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentElement = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            currentElement = 4;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            currentElement = 5;
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            currentElement = 6;
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            currentElement = 7;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ()
        hitPoints--;
        if (hitPoints <= 0)
            Destroy(gameObject);
    }
}
