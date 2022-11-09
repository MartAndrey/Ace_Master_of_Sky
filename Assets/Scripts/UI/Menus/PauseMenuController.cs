using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem.UI;

public class PauseMenuController : MonoBehaviour
{
    public static PauseMenuController Instance;

    public bool IsTransition { get; set; }
    public bool InputSystemUI { get { return inputSystemUI.enabled; } set { inputSystemUI.enabled = value; } }
    public float ScrollbarValue { get { return scrollBar.value; } }

    [SerializeField] Scrollbars scrollBar;
    [SerializeField] Image imageResume;
    [SerializeField] TMP_Text textResume;
    [SerializeField] InputSystemUIInputModule inputSystemUI;

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

    void OnEnable()
    {
        LoadData();
    }

    void OnDisable()
    {
        SaveData();
    }

    void Update()
    {
        if (scrollBar.value <= 0.0f)
        {
            scrollBar.value = 0.0001f;
        }
    }

    void SaveData()
    {
        PlayerPrefs.SetFloat("ScrollbarValue", scrollBar.value);
    }

    void LoadData()
    {
        scrollBar.value = PlayerPrefs.GetFloat("ScrollbarValue");
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

    public IEnumerator CheckTransition()
    {
        IsTransition = true;
        yield return new WaitForSecondsRealtime(1);
        IsTransition = false;
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

    public void SelectResume()
    {
        imageResume.color = Color.white;
        textResume.color = Color.white;
    }

    public void DeselectResume()
    {
        imageResume.color = Color.black;
        textResume.color = Color.black;
    }

    void SwitchScene(string nameScene)
    {
        Fade fade = FindObjectOfType<Fade>();
        fade.FadeIn();

        ScreenManager.Instance.HidePause(0);
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