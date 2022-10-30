using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipController : MonoBehaviour
{
    void OnEnable()
    {
        GameManager.OnResetPosition += ResetPosition;
    }

    void OnDisable()
    {
        GameManager.OnResetPosition -= ResetPosition;
    }

    void ResetPosition()
    {
        float offsetEnemy = transform.position.x - GameManager.Instance.CameraCurrentPosition.x;
        transform.position = new Vector2(offsetEnemy, transform.position.y);
    }
}
