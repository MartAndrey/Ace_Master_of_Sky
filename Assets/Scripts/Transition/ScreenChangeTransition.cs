using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void ChangeScreen(Canvas screen, bool state, float time)
    {
        StartCoroutine(ChangeScreenRutiner(screen, state, time));
    }

    IEnumerator ChangeScreenRutiner(Canvas screen, bool state, float time)
    {
        yield return new WaitForSecondsRealtime(time);
        screen.enabled = state;
    }

    public void ChangeScreenScene(float time)
    {
        StartCoroutine(ChangeScreenRutiner(time));
    }

    IEnumerator ChangeScreenRutiner(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        SceneManager.LoadScene("MenuScene");
    }
}