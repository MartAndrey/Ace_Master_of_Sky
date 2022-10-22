using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float rotSpeed;
    [SerializeField] float maxSpeed ;
    [SerializeField] float rateFire; 
    [SerializeField] float acceleration; 

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
            GameObject go = ObjectPooler.Instance.GetPoolObject("Bullet");
            go.transform.position = transform.position ;
            go.transform.rotation = transform.rotation;
            go.SetActive(true);

            StartCoroutine(ReloadGun());
        }
    }

    IEnumerator ReloadGun()
    {
        yield return new WaitForSeconds(1 / rateFire);
        gunLoaded = true;
    }
}