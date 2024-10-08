using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject Bullet;
    [SerializeField]float hitPoints;
    GameObject powerUp;
    [SerializeField]int powerUpChance;
    int score;
    void Start()
    {
        powerUpChance = Random.Range(1, 5);
    }

    void Update()
    {
        
    }

    public void damageTaken(float damage)
    {
        hitPoints =- damage;
    }
    public void Shoot()
    {
        Instantiate(Bullet);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "Bullet")
        {
            score += 10;
            damageTaken();
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
