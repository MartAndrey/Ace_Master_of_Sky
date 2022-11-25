using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public enum GameState { StateMenu, StateGame, StatePause, StateGameOver }

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static Action OnResetPosition;
    public static Action OnUpdateSound;
    public static Action OnStatsUp;

    [SerializeField] GameState currentGameState = GameState.StateGame;
    public GameState CurrentGameState { get { return currentGameState; } }

    public float SpawnRate { get { return spawnRate; } }
    public float OffsetBackground { get { return offsetBackground; } }
    public InputSystemUIInputModule InputSystemUI { get { return inputSystemUI; } set { inputSystemUI = value; } }
    public Vector3 CameraCurrentPosition { get; set; }
    public int Score { get; set; }
    public int Level { get; set; }

    public float RotationSpeedEnemy { get { return rotationSpeedEnemy; } }
    public float SpeedEnemy { get { return speedEnemy; } }

    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject background;
    [SerializeField] GameObject player;
    [Range(0.1f, 10f), SerializeField] float spawnRate;
    [SerializeField] InputSystemUIInputModule inputSystemUI;
    [SerializeField] float requisiteScoreUp;

    Keyboard keyboard;

    float startPositionCamera, offsetPlayer, offsetBackground;
    float speedEnemy = 2, rotationSpeedEnemy = 200;

    void OnEnable()
    {
        OnResetPosition += SetStartPosition;
        OnStatsUp += StatsUp;
    }

    void OnDisable()
    {
        OnResetPosition -= SetStartPosition;
        OnStatsUp -= StatsUp;
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

        Level = 1;
    }

    void Update()
    {
        CheckGameStatus();

        if ((keyboard.escapeKey.wasPressedThisFrame || keyboard.pKey.wasPressedThisFrame) && !PauseMenuController.Instance.IsTransition)
        {
            if (currentGameState == GameState.StateGame || currentGameState == GameState.StatePause)
            {
                StartCoroutine(PauseMenuController.Instance.CheckTransition());
                StatePause();
            }
        }

        CameraCurrentPosition = mainCamera.transform.position;

        if (mainCamera.transform.position.x >= 48)
        {
            OnResetPosition?.Invoke();
        }

        if (Score >= requisiteScoreUp)
            ToLevelUp();
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
            ScreenManager.Instance.ShowMainMenu();
        }
        else if (newGameState == GameState.StateGame)
        {
            SceneManager.LoadScene("GameScene");
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
            ScreenManager.Instance.ShowGameOver(0);
            GameOverMenuController.Instance.StartFade();
            OnUpdateSound?.Invoke();
            PauseMenuController.Instance.DisableScrollbars();

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

    void ToLevelUp()
    {
        Level += 1;
        requisiteScoreUp *= 2;

        OnStatsUp?.Invoke();
    }

    void StatsUp()
    {
        spawnRate += LevelUp.Instance.LevelUpStats(spawnRate);
        speedEnemy += LevelUp.Instance.LevelUpStats(speedEnemy);
        rotationSpeedEnemy += LevelUp.Instance.LevelUpStats(rotationSpeedEnemy);
    }
}