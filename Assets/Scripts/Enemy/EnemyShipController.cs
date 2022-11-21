using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipController : EnemyBaseController
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float distanceToShoot;
    [SerializeField] float speedBullet;
    [SerializeField, Range(1f, 10)] float rateFire;
    [SerializeField] GameObject[] shootPositionLeft;
    [SerializeField] GameObject[] shootPositionRight;

    PlayerController player;
    bool shootLoaded;
    float angle;
    float sqrDistanceToShoot;

    Vector2 direction;

    void Start()
    {
        shootLoaded = true;
        player = FindObjectOfType<PlayerController>();

        sqrDistanceToShoot = distanceToShoot * distanceToShoot;
    }

    void Update()
    {
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        CheckAvailableToShoot();
    }

    void FixedUpdate()
    {
        if (!player.gameObject.activeInHierarchy)
            return;

        direction = player.transform.position - transform.position;
    }

    void CheckAvailableToShoot()
    {

        if (IsCloseToPlayer() && shootLoaded && angle >= 90)
        {
            StartCoroutine(ShootBulletRutiner(shootPositionLeft));
        }
        else if (IsCloseToPlayer() && shootLoaded && angle <= 90)
        {
            StartCoroutine(ShootBulletRutiner(shootPositionRight));
        }
    }

    void ShootBullet(Vector2 position)
    {
        shootLoaded = false;

        GameObject bullet = Instantiate(bulletPrefab, position, Quaternion.identity);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = (direction + Vector2.right * player.Speed).normalized * speedBullet;
        bulletRb.rotation = angle;
    }

    IEnumerator ShootBulletRutiner(GameObject[] spawnPositionToShoot)
    {
        for (int i = 0; i < spawnPositionToShoot.Length; i++)
        {
            if (!IsCloseToPlayer())
                break;

            Vector2 currentPosition = spawnPositionToShoot[i].transform.position;
            ShootBullet(currentPosition);
            yield return new WaitForSeconds(1 / rateFire);
            shootLoaded = true;
        }
    }

    bool IsCloseToPlayer() => direction.sqrMagnitude < sqrDistanceToShoot;

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