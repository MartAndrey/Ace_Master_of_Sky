using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] float speed;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        StartCoroutine(TimeLife());

        rb.velocity = transform.up * speed;
    }

    IEnumerator TimeLife()
    {
        yield return new WaitForSeconds(5);
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyBaseController>().ChangeHealth(player.Damage);
            gameObject.SetActive(false);
        }
    }
}