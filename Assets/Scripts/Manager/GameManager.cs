using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum GameState { StateMenu, StateGame, StatePause, StateGameOver }

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static Action OnResetPosition;

    [SerializeField] GameState currentGameState = GameState.StateGame;

    public Vector3 CameraCurrentPosition { get; set; }
    public float SpawnRate { get { return spawnRate; } }
    public float OffsetBackground { get { return offsetBackground; } }
    public int Score { get; set; }
    public int Level { get; set; }

    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject background;
    [SerializeField] GameObject player;
    [Range(0.1f, 10f), SerializeField] float spawnRate;

    Keyboard keyboard;

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

        keyboard = Keyboard.current;
    }

    void Start()
    {
        startPositionCamera = mainCamera.transform.position.x;

        StartCoroutine(GetEnemy());
    }

    void Update()
    {
        CheckGameStatus();

        if (keyboard.escapeKey.wasPressedThisFrame && !PauseMenuController.Instance.IsTransition)
        {
            StartCoroutine(PauseMenuController.Instance.CheckTransition());
            StatePause();
        }

        CameraCurrentPosition = mainCamera.transform.position;

        if (mainCamera.transform.position.x >= 48)
        {
            OnResetPosition?.Invoke();
        }
    }

    void CheckGameStatus()
    {
        if (currentGameState != GameState.StateGame)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
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
            // TODO: ScreenManager.Instance.ShowMenu();
        }
        else if (newGameState == GameState.StateGame)
        {
            ScreenManager.Instance.ShowGame();
        }
        else if (newGameState == GameState.StatePause)
        {
            ScreenManager.Instance.CheckPauseStatus();

            // Switch to game state if you press the ESC key while paused
            if (currentGameState == GameState.StatePause)
            {
                StartCoroutine(SwitchGameStateRutiner());
            }
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

    public IEnumerator SwitchGameStateRutiner()
    {
        yield return new WaitForSecondsRealtime(1);
        currentGameState = GameState.StateGame;
    }
}