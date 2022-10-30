using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSpawn : MonoBehaviour
{
    [SerializeField] CameraFollow mainCamera;
    [SerializeField] GameObject spawnPoint;
    [SerializeField] GameObject[] listShips;

    float speed;

    void OnEnable()
    {
        GameManager.OnResetPosition += ResetPosition;
    }

    void Start()
    {
        speed = mainCamera.SpeedX;
        StartCoroutine(SpawnShipRutiner());
    }

    void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;
    }

    void ResetPosition()
    {
        float offsetShipSpawn = transform.position.x - GameManager.Instance.CameraCurrentPosition.x;
        transform.position = new Vector2(offsetShipSpawn, transform.position.y);
    }

    void SpawnShip()
    {
        int random = Random.Range(0, listShips.Length);
        Instantiate(listShips[random], spawnPoint.transform.position, Quaternion.identity);
    }

    IEnumerator SpawnShipRutiner()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            SpawnShip();

        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ship"))
        {
            Destroy(other.gameObject);
        }
    }
}
