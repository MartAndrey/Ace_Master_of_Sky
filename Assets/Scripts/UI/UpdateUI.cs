using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateUI : MonoBehaviour
{
    [SerializeField] TMP_Text score;
    [SerializeField] TMP_Text level;

    int startScore = 0;
    int startLevel = 1;

    void Start()
    {
        score.text = startScore.ToString();
        level.text = startLevel.ToString();
    }

    void Update()
    {
        score.text = GameManager.Instance.Score.ToString();
    }
}