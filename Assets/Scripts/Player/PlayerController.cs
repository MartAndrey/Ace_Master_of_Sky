using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : MonoBehaviour
{
    public float RotSpeed { get { return rotSpeed; } set { rotSpeed = value; } }
    public float Acceleration { get { return acceleration; } set { acceleration = value; } }
    public float Damage { get { return damage; } }

    [SerializeField] float speed;
    [SerializeField] float maxSpeed;
    [SerializeField] float rotSpeed;
    [SerializeField] float rateFire;
    [SerializeField] float acceleration;
    [SerializeField] float damage;

    Rigidbody2D rb;

    bool gunLoaded;
    float movementX;
    float movementY;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gunLoaded = true;
    }

    void FixedUpdate()
    {
        rb.rotation -= movementX * rotSpeed;
        speed = Mathf.Clamp(speed + movementY / acceleration, 1.5f, maxSpeed);

        rb.velocity = transform.up * speed;
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void OnFire()
    {
        if (gunLoaded)
        {
            gunLoaded = false;

            GameObject bullet = BulletObjectPooler.Instance.GetPoolObject();
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;

            StartCoroutine(ReloadGun());
        }
    }

    IEnumerator ReloadGun()
    {
        yield return new WaitForSeconds(1 / rateFire);
        gunLoaded = true;
    }
}