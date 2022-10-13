using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public delegate void PlayerDeath();
    public static event PlayerDeath OnPlayerDeath;

    public GameObject GameOverScreen;

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
}
