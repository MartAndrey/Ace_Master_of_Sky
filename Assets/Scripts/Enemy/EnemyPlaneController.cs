using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyPlaneController : MonoBehaviour
{
    CameraFollow mainCamera;

    [SerializeField] GameObject explosionPrefab;
    [SerializeField] int damage;
    [SerializeField] int life;
    [SerializeField] int points;

    void OnEnable()
    {
        GameManager.OnResetPosition += ResetPosition;
    }

    void OnDisable()
    {
        GameManager.OnResetPosition -= ResetPosition;
    }

    void ResetPosition()
    {
        float offsetEnemy = transform.position.x - GameManager.Instance.CameraCurrentPosition.x;
        transform.position = new Vector2(offsetEnemy, transform.position.y);
    }

    public void TakeDamage()
    {
        life--;

        if (life <= 0)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
            GameManager.Instance.Score += points;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TakeDamage();
        }
    }
}