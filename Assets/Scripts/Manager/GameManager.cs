using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public delegate void PlayerDeath();
    public static event PlayerDeath OnPlayerDeath;
    public static Action OnUpdateScore;

    [SerializeField] GameObject GameOverScreen;

    void OnEnable()
    {
        OnUpdateScore += UpdateScoreUI;
    }

    void Awake()
    {
        GameOverScreen.SetActive(false);
        OnPlayerDeath += ShowGameOverScreem;
    }

    void ShowGameOverScreem()
    {
        GameOverScreen.SetActive(true);
    }

    public void PlayerKilled()
    {
        OnPlayerDeath?.Invoke();
    }

    public void UpdateScoreUI()
    {
        // Change the value of the score in the UI
        Debug.Log("Score Update");
    }
}
