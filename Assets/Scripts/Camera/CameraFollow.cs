using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] float speedX;
    [SerializeField] float speedY;

    float positionY;
    float maxHight = 20;
    float minHight = 0;
    float time = 20;

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
        Debug.Log(positionY);
    }
}
