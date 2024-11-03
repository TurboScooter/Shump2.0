using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalizeShielf : MonoBehaviour
{

    [SerializeField]float shieldHitPoints;
    float damageAmount;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            shieldHitPoints -= collision.GetComponent<Enemy>().Damage;
            if (shieldHitPoints <= 0)
            {
                Destroy(gameObject);
            }
        }


    }
}
