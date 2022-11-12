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
    int startLevel = 1;

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
        level.text = startLevel.ToString();
    }

    void Update()
    {
        score.text = GameManager.Instance.Score.ToString();
    }

    public string GetScore() => score.text;
}