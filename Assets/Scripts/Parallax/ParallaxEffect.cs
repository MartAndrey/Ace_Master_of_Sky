using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] float speedParallax;

    Transform cameraTransform;
    Vector3 previousCameraPosition;
    float spriteWidth, startPosition, moveAmount, deltaX;

    Vector3 resertPreviousCameraPosition;

    void OnEnable()
    {
        GameManager.OnResetPosition += ResetPosition;
    }

    void OnDisable()
    {
        GameManager.OnResetPosition -= ResetPosition;
    }

    void Start()
    {
        cameraTransform = Camera.main.transform;
        previousCameraPosition = cameraTransform.position;
        spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
        startPosition = transform.position.x;

        resertPreviousCameraPosition = previousCameraPosition;
    }

    void LateUpdate()
    {
        deltaX = (cameraTransform.position.x - previousCameraPosition.x) * speedParallax;
        moveAmount = cameraTransform.position.x * (1 - speedParallax);

        if (deltaX > 0.1f || deltaX < -0.1)
        {
            deltaX = 0;
        }

        transform.Translate(new Vector3(deltaX, 0, 0));
        previousCameraPosition = cameraTransform.position;

        if (moveAmount > startPosition + spriteWidth)
        {
            transform.Translate(new Vector3(spriteWidth, 0, 0));
            startPosition += spriteWidth;
        }
    }

    public void ResetPosition()
    {
        previousCameraPosition.x = resertPreviousCameraPosition.x;
        StartCoroutine(FixStartPosition());
    }

    IEnumerator FixStartPosition()
    {
        yield return new WaitForSeconds(1);
        startPosition = GameManager.Instance.OffsetBackground;
    }
}