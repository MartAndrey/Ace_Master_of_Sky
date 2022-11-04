using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenChangeTransition : MonoBehaviour
{
    public static ScreenChangeTransition Instance;

    float time = 1;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void ChangeScreen(GameObject screen, bool state)
    {
        StartCoroutine(ChangeScreenRutiner(screen, state, time));
    }

    IEnumerator ChangeScreenRutiner(GameObject screen, bool state, float time)
    {
        yield return new WaitForSeconds(time);
        screen.SetActive(state);
    }
}