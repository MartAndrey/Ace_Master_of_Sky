using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipController : EnemyBaseController
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float distanceToShoot;
    [SerializeField] float speedBullet;
    [SerializeField, Range(1f, 10)] float rateFire;
    PlayerController player;
    bool shootLoaded;
    float angle;

    Vector2 direction;

    void Start()
    {
        shootLoaded = true;
        player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        direction = player.transform.position - transform.position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (direction.magnitude < distanceToShoot && shootLoaded)
        {
            ShootBullet();
        }
    }

    void ShootBullet()
    {
        shootLoaded = false;
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = direction.normalized * speedBullet;
        bulletRb.rotation = angle;

        StartCoroutine(ShootBulletRutiner());
    }

    IEnumerator ShootBulletRutiner()
    {
        yield return new WaitForSeconds(1 / rateFire);
        shootLoaded = true;
    }

    protected override void DisableGameObject()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
        GameManager.Instance.Score += points;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            DisableGameObject();
            other.collider.GetComponent<PlayerLife>().ChangeHealth(damage);
        }
    }
}