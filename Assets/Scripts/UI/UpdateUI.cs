using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateUI : MonoBehaviour
{
    public static UpdateUI Instance;

    [SerializeField] TMP_Text score;
    [SerializeField] TMP_Text level;

    int startScore = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        score.text = startScore.ToString();
    }

    void Update()
    {
        score.text = GameManager.Instance.Score.ToString();
        level.text = GameManager.Instance.Level.ToString();
    }

    public string GetScore() => score.text;
}