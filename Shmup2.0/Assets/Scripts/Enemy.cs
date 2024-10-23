using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]float hitPoints;
    GameObject powerUp;
    int powerUpChance;
    [SerializeField]int score;
    [SerializeField] bool hydro = false;
    bool pyro = false;
    [SerializeField] bool cryo = false;
    bool dendro = false;
    bool geo = false;
    bool electro = false;
    bool anemo = false;
    bool reverseVaporize = false;
    bool Vaporize = false;
    bool reverseMelt = false;
    bool Melt = false;
    [SerializeField] bool freeze = false;
    bool Damage150 = false;
    bool Damage200 = false;
    bool swirl = false;
    bool crystalize = false;
    bool shatter = false;
    bool overloaded = false;
    bool electroCharged = false;
    bool superConduct = false;
    bool bloom = false;
    bool hyperBloom = false;
    bool burgeon = false;
    bool burning = false;
    bool quicken = false;
    bool spread = false;
    bool aggrevate = false;

    [SerializeField] float ElementalTimers;
    float cryoTime;
    float hydroTime;
    float pyroTime;
    float geoTime;
    float electroTime;
    float anemoTime;
    float dendroTime;
    [SerializeField]float reactionTimer;
    float freezeTime;
    float burgeonTime;
    [SerializeField]bool freezePosition = false;
    [SerializeField]GameObject dendroCore;
    [SerializeField] GameObject CrystalizeShard;
    Rigidbody2D rb;
    RigidbodyConstraints2D rbConstraints;
    [SerializeField] float baseDamage;
    [SerializeField] float overloadedRadius;
    [SerializeField] float overloadedDamage;
    [SerializeField]float levelMultiplier;

    void Start()
    {
        powerUpChance = Random.Range(1, 5);
        rb = GetComponent<Rigidbody2D>();
        rbConstraints = rb.constraints;
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
            pyro = false;
            cryo = false;
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
        else
            hitPoints -= damage;


    }
    public void Update()
    {
        Reactions();
        Timers();
        Applyfreeze();
        ApplyOverload();
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
        if (electro && !cryo || cryo && !electro)
            superConduct = true;
        if(pyro && !electro || electro && !pyro)
            overloaded = true;
        if(geo && !pyro || geo && !cryo || geo && !hydro || geo && !electro)
            crystalize = true;
        if(pyro && !geo|| hydro && !geo || cryo && !geo || electro && !geo)
            crystalize = true;
        if (dendro && !hydro || hydro && !dendro)
            bloom = true;
        if(dendro && !pyro || pyro && !dendro)
            burgeon = true;
        if(dendro && !electro || electro && !dendro)
            quicken = true;

        if(electroTime <= 0)
            electro = false; 
        if (cryoTime <= 0)
            cryo = false;
        if (pyroTime <= 0)
            pyro = false;
        if (hydroTime <= 0)
            hydro = false;
        if (geoTime <= 0)
            geo = false;
        if (dendroTime <= 0)
            dendro = false;
        if (anemoTime <= 0)
            anemo = false;

        if(!pyro && !hydro)
        {
            reverseVaporize = false;
            Vaporize = false;
        }
        if(!cryo && !pyro)
        {
            Melt = false;
            reverseMelt = false;
        }
        if(!hydro && !cryo)
        {
            freeze = false;
        }
        if(!electro && !cryo)
        {
            electroCharged = false;
        }
        if (!electro && !pyro)
        {
            overloaded = false;
        }
        if (!geo && !hydro || !geo && !pyro || !geo && !cryo || !geo && !electro)
        {
            crystalize = false;
        }
        if (!dendro && !hydro)
        {
            bloom = false;
        }
        if (!dendro && !pyro)
        {
            burgeon = false;
        }
    }

    void Applyfreeze()
    {
        
        if(hydro && freeze && cryo)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            hydro = false;
            freezeTime = reactionTimer;
            StartCoroutine("RemoveFreeze");

        }
    }
    IEnumerator RemoveFreeze()
    {

        yield return new WaitForSeconds(freezeTime);
        freeze = false;
        cryo = false;
    }
    void ApplyOverload()
    {
        overloadedDamage = 2 * levelMultiplier;

        Vector2 point = new Vector2(transform.position.y, transform.position.x);
        if(electro && overloaded && pyro)
        {
                
                Collider2D[] hitColliders = Physics2D.OverlapCircleAll(point, overloadedRadius);
                foreach (Collider2D hitCollider in hitColliders)
                {
                    if (hitCollider.gameObject.CompareTag("Enemy"))
                    {
                        Debug.Log(hitCollider.gameObject);
                        
                        hitCollider.gameObject.GetComponent<Enemy>().damageTaken(overloadedDamage);
                    }
                }
              
                electro = false;
                pyro = false;
                overloaded = false;
            }
    }

    void ApplySuperConduct()
    {

    }

    void ApplyElectroCharged()
    {

    }


    private void OnTriggerEnter2D(Collider2D collision)    
    {
        if (collision.gameObject.CompareTag("HydroBullet"))
        {
            hydro = true;
            hydroTime = ElementalTimers;
        }
        else if (collision.gameObject.CompareTag("CryoBullet"))
        {
            cryo = true;
            cryoTime = ElementalTimers;
        }
        else if (collision.gameObject.CompareTag("PyroBullet"))
        {
            pyro = true;
            pyroTime = ElementalTimers;
        }
        else if (collision.gameObject.CompareTag("DendroBullet"))
        {
            dendro = true;
            dendroTime = ElementalTimers;
        }
        else if (collision.gameObject.CompareTag("ElectroBullet"))
        {
            electro = true;
            electroTime = ElementalTimers;
        }
        else if (collision.gameObject.CompareTag("GeoBullet"))
        {
            geo = true;
            geoTime = ElementalTimers;
        }
        else if (collision.gameObject.CompareTag("AnemoBullet"))
        {
            anemo = true;
            anemoTime = ElementalTimers;
        }

        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("AnemoBullet") || collision.gameObject.CompareTag("GeoBullet") || collision.gameObject.CompareTag("ElectroBullet") || collision.gameObject.CompareTag("DendroBullet") || collision.gameObject.CompareTag("PyroBullet") || collision.gameObject.CompareTag("CryoBullet") || collision.gameObject.CompareTag("HydroBullet"))
        {
            damageTaken(baseDamage);
            score += 10;
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
    
    void Timers()
    {
        freezeTime -= Time.deltaTime;
        cryoTime -= Time.deltaTime;
        pyroTime -= Time.deltaTime;
        electroTime -= Time.deltaTime;
        dendroTime -= Time.deltaTime;
        geoTime -= Time.deltaTime;
        hydroTime -= Time.deltaTime;
        anemoTime -= Time.deltaTime;
    }

}
