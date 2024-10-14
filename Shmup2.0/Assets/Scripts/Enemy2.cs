using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Enemy
{
    public GameObject Bullet;

    void Start()
    {
        
    }



    private void shoot()
    {
        Instantiate(Bullet);
    }
}
