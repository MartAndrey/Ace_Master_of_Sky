using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyPlaneController : EnemyBaseController
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DisableGameObject();
            other.GetComponent<PlayerLife>().ChangeHealth(damage);
        }
    }
}