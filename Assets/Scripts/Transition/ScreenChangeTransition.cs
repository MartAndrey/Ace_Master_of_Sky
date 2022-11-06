using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenChangeTransition : MonoBehaviour
{
    public static ScreenChangeTransition Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void ChangeScreen(Canvas screen, bool state, float time = 1)
    {
        StartCoroutine(ChangeScreenRutiner(screen, state, time));
    }

    IEnumerator ChangeScreenRutiner(Canvas screen, bool state, float time)
    {
        yield return new WaitForSecondsRealtime(time);
        screen.enabled = state;
    }
}