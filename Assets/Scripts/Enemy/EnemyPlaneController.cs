using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyPlaneController : EnemyBaseController
{
    [SerializeField] Pathfinding.AIPath aIPath;

    void Update()
    {
        if (aIPath.maxSpeed != GameManager.Instance.SpeedEnemy || aIPath.rotationSpeed != GameManager.Instance.RotationSpeedEnemy)
        {
            aIPath.maxSpeed = GameManager.Instance.SpeedEnemy;
            aIPath.rotationSpeed = GameManager.Instance.RotationSpeedEnemy;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DisableGameObject();
            other.GetComponent<PlayerLife>().ChangeHealth(damage);
        }
    }
}