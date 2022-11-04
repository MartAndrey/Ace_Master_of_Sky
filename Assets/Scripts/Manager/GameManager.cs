using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { StateMenu, StateGame, StatePause, StateGameOver }

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static Action OnResetPosition;

    [SerializeField] GameState currentGameState = GameState.StateGame;

    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject background;
    [SerializeField] GameObject player;

    [Range(0.1f, 10f), SerializeField] float spawnRate;
    public Vector3 CameraCurrentPosition { get; set; }
    public float SpawnRate { get { return spawnRate; } }
    public float OffsetBackground { get { return offsetBackground; } }
    public int Score { get; set; }
    public int Level { get; set; }

    float startPositionCamera, offsetPlayer, offsetBackground;

    void OnEnable()
    {
        OnResetPosition += SetStartPosition;
    }

    void OnDisable()
    {
        OnResetPosition -= SetStartPosition;
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        startPositionCamera = mainCamera.transform.position.x;

        StartCoroutine(GetEnemy());
    }

    void Update()
    {
        CameraCurrentPosition = mainCamera.transform.position;

        if (mainCamera.transform.position.x >= 48)
        {
            OnResetPosition?.Invoke();
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

    public void StateMenu()
    {
        SetGameState(GameState.StateMenu);
    }

    public void StateGame()
    {
        SetGameState(GameState.StateGame);
    }

    public void StatePause()
    {
        SetGameState(GameState.StatePause);
    }

    public void StateGameOver()
    {
        SetGameState(GameState.StateGameOver);
    }

    void SetGameState(GameState newGameState)
    {
        if (newGameState == GameState.StateMenu)
        {

        }
        else if (newGameState == GameState.StateGame)
        {

        }
        else if (newGameState == GameState.StatePause)
        {

        }
        else if (newGameState == GameState.StateGameOver)
        {
            ScreenManager.Instance.ShowGameOver();
        }

        currentGameState = newGameState;
    }

    IEnumerator GetEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(1 / spawnRate);
            SpawnEnemies.Instance.ActiveEnemy();
        }
    }
}