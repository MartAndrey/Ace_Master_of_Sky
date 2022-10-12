using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    delegate void SimpleMessage();
    SimpleMessage simpleMessage;

    void Start()
    {
        simpleMessage = SendConsoleMessage;
        simpleMessage.Invoke();
    }

    void SendConsoleMessage()
    {
        Debug.Log("Message Send to console");
    }
}
