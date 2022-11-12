using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenuController : MonoBehaviour
{
    public static GameOverMenuController Instance;

    [SerializeField] GameObject confirmation;
    [SerializeField] GameObject score;
    [SerializeField] Animator animatorExit;

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

    public void Exit()
    {
        animatorExit.enabled = true;
        animatorExit.Play("FadeIn");

        confirmation.SetActive(true);
        score.SetActive(false);
    }

    public void ExitConfirmationNo()
    {
        animatorExit.SetTrigger("Transition");
        StartCoroutine(ExitConfirmationNoRutiner());
    }

    IEnumerator ExitConfirmationNoRutiner()
    {
        yield return new WaitForSecondsRealtime(1);
        confirmation.SetActive(false);
        score.SetActive(true);
    }

    public void ExitConfirmationYes()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
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