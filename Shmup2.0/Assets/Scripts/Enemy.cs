using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]float hitPoints;
    GameObject powerUp;
    int powerUpChance;
    [SerializeField]int score;
    bool statusEffect1 = false;
    bool statusEffect2 = false;
   [SerializeField] Bullet bullet;
    [SerializeField]float doubleDamageTime;
    [SerializeField] float doubleTimeAmount;
    void Start()
    {
        

        powerUpChance = Random.Range(1, 5);
    }

    public void damageTaken(float damage)
    {
        
        if (doubleDamageTime >= 0)
        {

            hitPoints -= damage * 2;
        } else
            hitPoints -= damage;
    }
    public void Update()
    {
        Reaction();
        doubleDamageTime -= Time.deltaTime;
    }

    public void Reaction()
    {
        if (statusEffect1 && statusEffect2)
        {
            doubleDamageTime = doubleTimeAmount;
            statusEffect1 = false;
            statusEffect2 = false;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("Bullet2"))
        {
            if (collision.gameObject.CompareTag("Bullet"))
                statusEffect1 = true;
            else if(collision.gameObject.CompareTag("Bullet2"))
                statusEffect2 = true;

            score += 10;
            damageTaken(bullet.damage);
            if(hitPoints <= 0)
            {
                score += 100;
                if(powerUpChance == 1)
                {
                    Instantiate(powerUp,gameObject.transform.position, gameObject.transform.rotation);
                }
                Destroy(gameObject);
            }
        }
    }
}
