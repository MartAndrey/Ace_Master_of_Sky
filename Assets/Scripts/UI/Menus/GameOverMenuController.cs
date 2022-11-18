using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameOverMenuController : MonoBehaviour, ISwitchScene
{
    enum ConfigurationStates { StateNoClick, StateSettings, StateCredits }

    public static GameOverMenuController Instance;

    ConfigurationStates currentConfigurationStates = ConfigurationStates.StateNoClick;

    [Header("UI references"), Space(2)]

    [Header("Main Title")]
    [SerializeField] TMP_Text title;

    [Header("Main Menu")]
    [SerializeField] GameObject restartButton;
    [SerializeField] GameObject exitButton;
    [SerializeField] GameObject settingsButton;
    [SerializeField] GameObject score;
    [SerializeField] TMP_Text scoreNumber;

    [Header("Settings"), Tooltip("When the setup button is pressed")]
    [SerializeField] GameObject itemsSetting;
    [Tooltip("Array containing the configuration button images, 0 setting - 1 back")]
    [SerializeField] Image[] settingImages;
    [Tooltip("Array containing the references of the configurations")]
    [SerializeField] GameObject[] itemsSettingUI;

    [Header("Exit"), ]
    [Tooltip("Reference when clicking the Exit button")]
    [SerializeField] GameObject confirmationButton;
    [Tooltip("Reference to the confirm exit button when clicking the Exit button")]
    [SerializeField] GameObject confirmationButtonNo;

    [Header("Credits")]
    [SerializeField] GameObject creditsUI;

    [Space, Tooltip("Exit button animation")]
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

        exitButton.GetComponent<Button>().interactable = false;
        restartButton.GetComponent<Button>().interactable = false;
        settingsButton.GetComponent<Button>().interactable = false;
    }

    public void Settings()
    {
        if (currentConfigurationStates == ConfigurationStates.StateNoClick)
        {
            title.text = "Settings";
            score.SetActive(false);
            restartButton.SetActive(false);
            exitButton.SetActive(false);
            settingImages[0].enabled = false;
            settingImages[1].enabled = true;
            itemsSetting.SetActive(true);

            currentConfigurationStates = ConfigurationStates.StateSettings;
        }
        else
        {
            BackSettings();
        }
    }

    public void MainMenu()
    {
        SwitchScene("MenuScene");
    }

    public void Credits()
    {
        title.text = "Credits";
        creditsUI.SetActive(true);

        foreach (GameObject ob in itemsSettingUI)
        {
            ob.SetActive(false);
        }

        currentConfigurationStates = ConfigurationStates.StateCredits;
    }

    void BackSettings()
    {
        if (currentConfigurationStates == ConfigurationStates.StateSettings)
        {
            title.text = "YOU LOSE";
            score.SetActive(true);
            restartButton.SetActive(true);
            exitButton.SetActive(true);
            settingImages[0].enabled = true;
            settingImages[1].enabled = false;
            itemsSetting.SetActive(false);

            currentConfigurationStates = ConfigurationStates.StateNoClick;
        }
        else if (currentConfigurationStates == ConfigurationStates.StateCredits)
        {
            title.text = "Settings";
            creditsUI.SetActive(false);

            foreach (GameObject ob in itemsSettingUI)
            {
                ob.SetActive(true);
            }

            currentConfigurationStates = ConfigurationStates.StateSettings;
        }
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

        exitButton.GetComponent<Button>().interactable = true;
        restartButton.GetComponent<Button>().interactable = true;
        settingsButton.GetComponent<Button>().interactable = true;
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

    public void SwitchScene(string nameScene)
    {
        Fade fade = FindObjectOfType<Fade>();
        fade.FadeIn();

        ScreenManager.Instance.HideGame();
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
}