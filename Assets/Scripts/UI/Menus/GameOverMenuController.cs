using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameOverMenuController : MonoBehaviour
{
    public static GameOverMenuController Instance;

    [SerializeField] GameObject restartButton;
    [SerializeField] GameObject exitButton;
    [SerializeField] GameObject confirmationButton;
    [SerializeField] GameObject confirmationButtonNo;
    [SerializeField] GameObject itemsSetting;
    [SerializeField] GameObject score;
    [SerializeField] Image[] settingImages;
    [SerializeField] TMP_Text title;
    [SerializeField] TMP_Text scoreNumber;
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
        yield return new WaitForSecondsRealtime(1);
        EventSystem.current.SetSelectedGameObject(restartButton);
        GameManager.Instance.InputSystemUI.enabled = true;
    }

    public void Restart()
    {
        SwitchScene("GameScene");
    }

    public void Exit()
    {
        animatorExit.enabled = true;
        animatorExit.Play("FadeIn");

        confirmationButton.SetActive(true);
        score.SetActive(false);
        EventSystem.current.SetSelectedGameObject(confirmationButtonNo);
    }

    public void Settings()
    {
        title.text = "Settings";
        score.SetActive(false);
        restartButton.SetActive(false);
        exitButton.SetActive(false);
        settingImages[0].enabled = false;
        settingImages[1].enabled = true;
        itemsSetting.SetActive(true);
    }

    public void SetScore()
    {
        scoreNumber.text = UpdateUI.Instance.GetScore();
    }

    public void ExitConfirmationNo()
    {
        animatorExit.SetTrigger("Transition");
        StartCoroutine(ExitConfirmationNoRutiner());
        EventSystem.current.SetSelectedGameObject(exitButton);
    }

    IEnumerator ExitConfirmationNoRutiner()
    {
        yield return new WaitForSecondsRealtime(1);
        confirmationButton.SetActive(false);
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