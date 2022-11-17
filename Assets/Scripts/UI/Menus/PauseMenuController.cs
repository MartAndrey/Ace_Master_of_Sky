using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PauseMenuController : MonoBehaviour, ISwitchScene
{
    public static PauseMenuController Instance;

    public bool IsTransition { get; set; }

    [SerializeField] GameObject scrollbarsBackgroundMusic;
    [SerializeField] GameObject scrollbarsSFX;

    Animator animator;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        animator = GetComponent<Animator>();
        IsTransition = false;
    }

    public void FadeIn()
    {
        animator.enabled = true;
        animator.Play("FadeIn");
    }

    public void FadeOut()
    {
        animator.SetTrigger("Transition");
    }

    public void MainMenu()
    {
        SwitchScene("MenuScene");
    }

    public void Restart()
    {
        SwitchScene("GameScene");
    }

    public void Resume()
    {
        GameManager.Instance.StatePause();
    }

    public void SwitchScene(string nameScene)
    {
        Fade fade = FindObjectOfType<Fade>();
        fade.FadeIn();

        ScreenManager.Instance.HidePause(0);
        StartCoroutine(SwitchSceneRutiner(nameScene));
    }

    public IEnumerator SwitchSceneRutiner(string nameScene)
    {
        yield return new WaitForSecondsRealtime(2);

        if (nameScene == "MenuScene")
        {
            GameManager.Instance.StateMenu();
        }
        else if (nameScene == "GameScene")
        {
            GameManager.Instance.StateGame();
        }
    }

    public IEnumerator CheckTransition()
    {
        IsTransition = true;
        yield return new WaitForSecondsRealtime(1);
        IsTransition = false;
    }

    public void DisableScrollbars()
    {
        scrollbarsBackgroundMusic.SetActive(false);
        scrollbarsSFX.SetActive(false);
    }

    public void EnableScrollbars()
    {
        scrollbarsBackgroundMusic.SetActive(true);
        scrollbarsSFX.SetActive(true);
    }
}