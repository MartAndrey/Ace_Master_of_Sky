using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShip : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 3);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        EnemyShipController ship = FindObjectOfType<EnemyShipController>();

        if (other.CompareTag("Player"))
        {
            if (ship == null)
                return;
                
            other.GetComponent<PlayerLife>().ChangeHealth(ship.Damage);
            Destroy(gameObject);
        }
    }
}