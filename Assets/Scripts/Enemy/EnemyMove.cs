using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class EnemyMove : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float speed;
 
    Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.velocity = (target.position - transform.position).normalized * speed;
    }
}