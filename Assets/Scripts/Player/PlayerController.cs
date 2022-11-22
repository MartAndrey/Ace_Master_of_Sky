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
    public float Speed { get { return speed; } }

    [Header("Stats")]
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

    void OnEnable()
    {
        GameManager.OnStatsUp += StatsUp;
    }

    void OnDisable()
    {
        GameManager.OnStatsUp -= StatsUp;
    }

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

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 movementVector = context.ReadValue<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (gunLoaded && context.performed && GameManager.Instance.CurrentGameState == GameState.StateGame)
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

    void StatsUp()
    {
        rateFire += LevelUp.Instance.LevelUpStats(rateFire);
    }
}