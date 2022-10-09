using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float rotSpeed;
    [SerializeField] float maxSpeed;

    Rigidbody2D rb;

    float movementX;
    float movementY;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void FixedUpdate()
    {
        rb.rotation -= movementX * rotSpeed;
        speed = Mathf.Clamp(speed + movementY, 1.5f , maxSpeed);

        rb.velocity = transform.up * speed;
    }
}