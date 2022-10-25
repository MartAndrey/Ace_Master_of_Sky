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

    public void ChangeScreen(GameObject screen)
    {
        StartCoroutine(ChangeScreenRutiner(screen, time));
    }

    IEnumerator ChangeScreenRutiner(GameObject screen, float time)
    {
        yield return new WaitForSeconds(time);
        screen.SetActive(true);
    }
}
