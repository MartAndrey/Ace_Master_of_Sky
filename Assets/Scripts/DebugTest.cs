using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTest : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Hi");

        for(int i = 0; i < 10; i++)
        {
            Debug.LogFormat($"<color=blue> {i} </color>");
        }
    }
}
