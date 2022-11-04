using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager Instance;

    [SerializeField] GameObject screenGame;
    [SerializeField] GameObject screenPause;
    [SerializeField] GameObject screenGameOver;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void ShowGame()
    {
        ScreenChangeTransition.Instance.ChangeScreen(screenGame, true);
    }

    public void HideGame()
    {
        ScreenChangeTransition.Instance.ChangeScreen(screenGame, false);
    }

    public void ShowPause()
    {
        ScreenChangeTransition.Instance.ChangeScreen(screenPause, true);
    }

    public void HidePause()
    {
        ScreenChangeTransition.Instance.ChangeScreen(screenPause, false);
    }

    public void ShowGameOver()
    {
        ScreenChangeTransition.Instance.ChangeScreen(screenGameOver, true);
    }

    public void HideGameOver()
    {
        ScreenChangeTransition.Instance.ChangeScreen(screenGameOver, false);
    }
}