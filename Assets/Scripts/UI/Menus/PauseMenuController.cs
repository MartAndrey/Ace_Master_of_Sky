using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem.UI;

public class PauseMenuController : MonoBehaviour
{
    public static PauseMenuController Instance;

    public bool IsTransition { get; set; }
    public bool InputSystemUI { get { return inputSystemUI.enabled; } set { inputSystemUI.enabled = value; } }
    public float ScrollbarValueBackground { get { return scrollBarBackground.value; } }
    public float ScrollBarValueSFX { get { return scrollBarSFX.value; } }

    [SerializeField] Scrollbar scrollBarBackground;
    [SerializeField] Scrollbar scrollBarSFX;
    
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
        if (scrollBarBackground.value <= 0.0f)
        {
            scrollBarBackground.value = 0.0001f;
        }
        
        if (scrollBarSFX.value <= 0.0f)
        {
            scrollBarSFX.value = 0.0001f;
        }
    }

    void SaveData()
    {
        PlayerPrefs.SetFloat("ScrollbarValueBackground", scrollBarBackground.value);
        PlayerPrefs.SetFloat("ScrollbarValueSFX", scrollBarSFX.value);
    }

    void LoadData()
    {
        scrollBarBackground.value = PlayerPrefs.GetFloat("ScrollbarValueBackground");
        scrollBarSFX.value = PlayerPrefs.GetFloat("ScrollbarValueSFX");
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