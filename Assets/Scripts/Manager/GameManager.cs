using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject background;
    [SerializeField] GameObject player;

    float startPositionCamera;
    float startPositionBackground;
    float startPlayer;

    void Start()
    {
        GetStartPosition();
    }

    void Update()
    {
        if (mainCamera.transform.position.x >= 1920)
        {
            SetStartPosition();

            ParallaxEffect parallaxEffect = FindObjectOfType<ParallaxEffect>();
            parallaxEffect.ResetPosition();
        }
    }

    void GetStartPosition()
    {
        startPositionCamera = mainCamera.transform.position.x;
        startPositionBackground = background.transform.position.x;
        startPlayer = player.transform.position.x;
    }

    void SetStartPosition()
    {
        mainCamera.transform.position = new Vector2(startPositionCamera, mainCamera.transform.position.y);
        background.transform.position = new Vector2(startPositionBackground, background.transform.position.y);
        player.transform.position = new Vector2(startPlayer, player.transform.position.y);
    }
}
