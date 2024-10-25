using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HyperBloomCode : MonoBehaviour
{

    [SerializeField] float hyperbloomRadius;
    [SerializeField] float hyperBloomDamage;
    [SerializeField] float Speed;
   [SerializeField] float moveSpeed;
    [SerializeField]Vector2 point;
    [SerializeField] Vector2 moveTo;
    bool enemyChecked;
    [SerializeField] float distance;
    [SerializeField] float nearestDistance;
    [SerializeField] Vector3 nearestObject;
    [SerializeField] GameObject nearestGameObject;
    bool nearestFound = false;
    LayerMask enemy;
    void Start()
    {
        enemy = LayerMask.GetMask("Enemy");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damage();
    }
    private void Update()
    {
        moveSpeed = Speed * Time.deltaTime;
        LocateClosest();
        if (nearestGameObject != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, nearestGameObject.transform.position, moveSpeed);
        }
    }
    void Damage()
    {
        Vector2 point = new Vector2(transform.position.y, transform.position.x);
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(point, hyperbloomRadius);
        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("Enemy"))
            {
                Debug.Log(hitCollider.gameObject);

                hitCollider.gameObject.GetComponent<Enemy>().damageTaken(hyperBloomDamage);
            }
            Destroy(gameObject);
        }
    }

    void LocateClosest()
    {
        Debug.Log("locate");
         point = new Vector2(transform.position.x, transform.position.y);
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(point, hyperbloomRadius, enemy);
        foreach (Collider2D hitCollider in hitColliders)
        {
           distance = Vector2.Distance(transform.position, hitCollider.transform.position);

            if(distance < nearestDistance)
            {
                Debug.Log(hitCollider);
                int i=0;
                i++;
                nearestObject = hitColliders[i].transform.position;
                if (!nearestFound)
                {
                    Debug.Log("1");
                    nearestGameObject = hitColliders[i].gameObject;
                    nearestFound = true;
                }
                nearestDistance = distance;
                enemyChecked = true;
            }
        }
       
    }
}
