using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class EnemyMove : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float speed;

    Rigidbody2D rb;
    Vector2 vectorDirection;
    float angleOffset = 90;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        vectorDirection = (target.position - transform.position).normalized;
    }

    void FixedUpdate()
    {
        float angle = Mathf.Atan2(vectorDirection.y, vectorDirection.x) * Mathf.Rad2Deg;

        rb.velocity = vectorDirection * speed;
        rb.rotation = angle - angleOffset;
    }
}