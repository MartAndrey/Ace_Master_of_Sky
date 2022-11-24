using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] GameObject explosionPrefab;

    [SerializeField] float health;

    public void ChangeHealth(float amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);

        GameManager.Instance.StateGameOver();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Limit"))
        {
            Death();
        }
    }
}