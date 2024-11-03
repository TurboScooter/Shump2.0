using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystalizecode : MonoBehaviour
{

    [SerializeField] GameObject Shield;
    [SerializeField] GameObject Player;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {          
            Instantiate(Shield, Player.transform);
          Destroy(gameObject);
        }
    }
}
