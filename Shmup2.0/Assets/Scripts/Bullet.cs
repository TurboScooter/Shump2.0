using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] public int damage;
    private void Start()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!collision.gameObject.CompareTag("Player"))
        Destroy(gameObject);
    }
}
