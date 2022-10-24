using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float OffsetBackground { get { return offsetBackground; } }

    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject background;
    [SerializeField] GameObject player;

    float startPositionCamera, offsetPlayer, offsetBackground;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        startPositionCamera = mainCamera.transform.position.x;
    }

    void Update()
    {
        if (mainCamera.transform.position.x >= 48)
        {
            SetStartPosition();

            ParallaxEffect parallaxEffect = FindObjectOfType<ParallaxEffect>();
            parallaxEffect.ResetPosition();
        }
    }

    void SetStartPosition()
    {
        offsetPlayer = player.transform.position.x - mainCamera.transform.position.x;
        offsetBackground = background.transform.position.x - mainCamera.transform.position.x;

        mainCamera.transform.position = new Vector2(startPositionCamera, mainCamera.transform.position.y);
        background.transform.position = new Vector2(offsetBackground, background.transform.position.y);
        player.transform.position = new Vector2(offsetPlayer, player.transform.position.y);
    }
}