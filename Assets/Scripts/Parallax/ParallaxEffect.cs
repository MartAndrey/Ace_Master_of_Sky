using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] float speedParallax;

    Transform cameraTransform;
    Vector3 previousCameraPosition;
    float spriteWidth, startPosition;

    void Start()
    {
        cameraTransform = Camera.main.transform;
        previousCameraPosition = cameraTransform.position;
        spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
        startPosition = transform.position.x;
    }
    
    void LateUpdate()
    {
        float deltaX = (cameraTransform.position.x - previousCameraPosition.x) * speedParallax;
        float moveAmount = cameraTransform.position.x * (1 - speedParallax);

        transform.Translate(new Vector3(deltaX, 0, 0));
        previousCameraPosition = cameraTransform.position;

        if (moveAmount > startPosition + spriteWidth)
        {
            transform.Translate(new Vector3(spriteWidth, 0, 0));
            startPosition += spriteWidth;
        }
    }
}
