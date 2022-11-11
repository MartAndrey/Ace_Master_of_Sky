using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager Instance;

    [SerializeField] Canvas screenGame;
    [SerializeField] Canvas screenPause;
    [SerializeField] Canvas screenGameOver;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void CheckPauseStatus()
    {
        if (!screenPause.enabled)
        {
            ShowPause(0);
            HideGame(0);

            PauseMenuController.Instance.FadeIn();
        }
        else
        {
            HidePause();
            ShowGame();

            PauseMenuController.Instance.FadeOut();
        }
    }

    public void ShowGame(float time = 1)
    {
        ScreenChangeTransition.Instance.ChangeScreen(screenGame, true, time);
    }

    public void HideGame(float time = 1)
    {
        ScreenChangeTransition.Instance.ChangeScreen(screenGame, false, time);
    }

    public void ShowPause(float time = 1)
    {
        ScreenChangeTransition.Instance.ChangeScreen(screenPause, true, time);
        GameManager.Instance.InputSystemUI = true;
    }

    public void HidePause(float time = 1)
    {
        ScreenChangeTransition.Instance.ChangeScreen(screenPause, false, time);
        GameManager.Instance.InputSystemUI = false;
    }

    public void ShowGameOver(float time = 1)
    {
        GameManager.Instance.InputSystemUI = true;
        ScreenChangeTransition.Instance.ChangeScreen(screenGameOver, true, time);
    }

    public void HideGameOver(float time = 1)
    {
        ScreenChangeTransition.Instance.ChangeScreen(screenGameOver, false, time);
        GameManager.Instance.InputSystemUI = false;
    }
}