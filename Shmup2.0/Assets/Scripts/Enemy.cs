using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]float hitPoints;
    GameObject powerUp;
    int powerUpChance;
    [SerializeField]int score;
    bool hydro = false;
    bool pyro = false;
    bool cryo = false;
    bool reverseVaporize = false;
    bool Vaporize = false;
    bool reverseMelt = false;
    bool Melt = false;
    bool freeze = false;
    bool Damage150 = false;
    bool Damage200 = false;
    bool freezePosition = false;
   [SerializeField] Bullet bullet;
    [SerializeField]float doubleDamageTime;
    [SerializeField] float doubleTimeAmount;
    void Start()
    {
        powerUpChance = Random.Range(1, 5);
    }

    public void damageTaken(float damage)
    {

        if (Damage200 && Melt)
        {
            hitPoints -= damage * 2;
            pyro = false;
            cryo = false;
        }
        else if (Damage150 && reverseMelt)
        {
            hitPoints -= damage / 100 * 150;
        }
        else if (Damage200 && Vaporize)
        {
            hitPoints -= damage * 2;
            pyro = false;
            hydro = false;
        }
        else if (Damage150 && reverseVaporize)
        {
            hitPoints -= damage / 100 * 150;
            pyro = false;
            hydro = true;
        }
        else if (freeze && cryo || freeze && hydro)
        {
            hitPoints -= damage;
            hydro = false;

        }
        else
            hitPoints -= damage;


    }
    public void Update()
    {
        Reactions();
        doubleDamageTime -= Time.deltaTime;
    }

    public void Reactions()
    {
        if(hydro && !pyro)
            reverseVaporize = true;
        if (reverseVaporize && pyro)
            Damage150 = true;
        if (pyro && !hydro)
           Vaporize = true;
        if (Vaporize && hydro)
            Damage150 = true;
        if (pyro && !Melt)
           reverseMelt = true;
        if (reverseMelt && cryo)
            Damage150 = true;
        if (cryo && !pyro)
            Melt = true;
        if(Melt && pyro)
            Damage200 = true;
        if(hydro && !cryo || cryo && !hydro)
            freeze = true;
        if (freeze && cryo || freeze && hydro)
        {
            
        }



    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("Bullet2"))
        {
            if (collision.gameObject.CompareTag("HydroBullet"))
                hydro = true;
            else if(collision.gameObject.CompareTag("CryoBullet"))
                cryo = true;
            else if(collision.gameObject.CompareTag("PyroBullet"))
                pyro = true;

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
