using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float SpeedX { get { return speedX; } }

    [SerializeField] float speedX;
    [SerializeField] float speedY;

    float positionY;
    float maxHight = 16;
    float minHight = 0;
    float time = 20;

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
        StartCoroutine(ChangeHight());
    }

    void Update()
    {
        transform.position += transform.right * speedX * Time.deltaTime;

        if (transform.position.y != positionY)
        {
            if (transform.position.y > positionY)
            {
                transform.position -= transform.up * positionY / speedY * Time.deltaTime;
            }
            else if (transform.position.y < positionY)
            {
                transform.position += transform.up * positionY / speedY * Time.deltaTime;
            }
        }
    }

    IEnumerator ChangeHight()
    {
        yield return new WaitForSeconds(time);
        StartCoroutine(ChangeHight());
        positionY = Random.Range(minHight, maxHight);
    }

    void StatsUp()
    {
        speedX += LevelUp.Instance.LevelUpStats(speedX);
        speedY += LevelUp.Instance.LevelUpStats(speedY);
    }
}