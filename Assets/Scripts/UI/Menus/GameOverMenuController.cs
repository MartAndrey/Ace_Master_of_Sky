using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenuController : MonoBehaviour
{
    public static GameOverMenuController Instance;

    Animator animator;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        animator = GetComponent<Animator>();
    }

    public void StartFade()
    {
        animator.enabled = true;
        animator.Play("FadeIn");

        StartCoroutine(FadeRutiner());
    }

    IEnumerator FadeRutiner()
    {
        yield return new WaitForSecondsRealtime(1);
        animator.SetTrigger("Transition");
    }

    public void Restart()
    {
        SwitchScene("GameScene");
    }

    void SwitchScene(string nameScene)
    {
        Fade fade = FindObjectOfType<Fade>();
        fade.FadeIn();

        ScreenManager.Instance.HideGameOver();
        StartCoroutine(SwitchSceneRutiner(nameScene));
    }

    IEnumerator SwitchSceneRutiner(string nameScene)
    {
        yield return new WaitForSecondsRealtime(2);

        Time.timeScale = 1;

        if (nameScene == "MenuScene")
        {
            GameManager.Instance.StateMenu();
        }
        else if (nameScene == "GameScene")
        {
            GameManager.Instance.StateGame();
        }
    }
}