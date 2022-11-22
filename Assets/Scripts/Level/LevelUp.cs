using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    public static LevelUp Instance;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public float LevelUpStats(float stat) => stat = (stat * 10) / 100;
}
