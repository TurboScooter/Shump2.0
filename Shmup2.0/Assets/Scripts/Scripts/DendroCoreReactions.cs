using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class DendroCoreReactions : MonoBehaviour
{
    bool pyro = false;
    bool dendro = true;
    bool electro = false;
    [SerializeField]float burgeonRadius;
    [SerializeField] float burgeonDamage;
    [SerializeField] float selfDestructDamage;
    [SerializeField] float selfDestructTime;
    [SerializeField] float selfDestructRadius;
    [SerializeField] GameObject sprawlingShot;
    void Start()
    {
        
    }

    void Update()
    {
        ApplyBurgeon();
        ApplyHyperBloom();
        selfDestructTime -= Time.deltaTime;
        if(selfDestructTime < 0)
        {
            Vector2 point = new Vector2(transform.position.y, transform.position.x);
            if (!pyro && !electro)
            {
                Collider2D[] hitColliders = Physics2D.OverlapCircleAll(point, selfDestructRadius);
                foreach (Collider2D hitCollider in hitColliders)
                {
                    if (hitCollider.gameObject.CompareTag("Enemy"))
                    {
                        Debug.Log(hitCollider.gameObject);

                        hitCollider.gameObject.GetComponent<Enemy>().damageTaken(selfDestructDamage);
                    }
                }
                Destroy(gameObject);
            }
        }
    }
    void ApplyHyperBloom()
    {
        if (electro)
        {
            Instantiate(sprawlingShot, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
    void ApplyBurgeon()
    {
        
        
        Vector2 point = new Vector2(transform.position.y, transform.position.x);
        if (pyro)
        {
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(point, burgeonRadius);
            foreach (Collider2D hitCollider in hitColliders)
            {
                if (hitCollider.gameObject.CompareTag("Enemy"))
                {
                    Debug.Log(hitCollider.gameObject);

                    hitCollider.gameObject.GetComponent<Enemy>().damageTaken(burgeonDamage);
                    Destroy(gameObject);
                }
            }

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PyroBullet"))
            pyro = true;
        else if (collision.gameObject.CompareTag("ElectroBullet"))
            electro = true;
    }
}
