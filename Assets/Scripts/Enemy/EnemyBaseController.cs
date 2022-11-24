using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseController : MonoBehaviour
{
    public float Damage { get { return damage; } }

    [SerializeField] protected GameObject explosionPrefab;
    [SerializeField] protected float damage;
    [SerializeField] protected float health;
    [SerializeField] protected float startHealth;
    [SerializeField] protected int points;

    void OnEnable()
    {
        GameManager.OnResetPosition += ResetPosition;
        health = startHealth;
    }

    void OnDisable()
    {
        GameManager.OnResetPosition -= ResetPosition;
    }

    protected void ResetPosition()
    {
        float offsetEnemy = transform.position.x - GameManager.Instance.CameraCurrentPosition.x;
        transform.position = new Vector2(offsetEnemy, transform.position.y);
    }

    public void ChangeHealth(float amount)
    {
        health -= amount;

        if (health <= 0)
        {
            DisableGameObject();
        }
    }

    protected virtual void DisableGameObject()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
        GameManager.Instance.Score += points;
    }
}