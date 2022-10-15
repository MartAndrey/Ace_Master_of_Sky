using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    [SerializeField] GameObject explosionPrefab;

    void OnEnable()
    {
        GameManager.OnUpdateScore += Deactivate;
    }

    void OnDisable()
    {
        GameManager.OnUpdateScore.Invoke();
        GameManager.OnUpdateScore -= Deactivate;
    }

    void Deactivate()
    {
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Deactivate();
        }

        if (other.CompareTag("Bullet"))
        {
            Deactivate();
        }
    }
}
