using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scrollbars : BaseUI
{
    public enum TypesScrollbar { BackgroundMusic, SoundEffects }

    public TypesScrollbar typesScrollbar { get { return typeScrollbar; } }

    public float ScrollBarValue { get { return scrollbar.value; } }

    [SerializeField] TypesScrollbar typeScrollbar;

    Scrollbar scrollbar;

    void OnEnable()
    {
        LoadData();
        GameManager.OnUpdateSound += SaveData;
    }

    void OnDisable()
    {
        SaveData();
        GameManager.OnUpdateSound -= SaveData;
    }

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        scrollbar = GetComponent<Scrollbar>();
    }

    void Update()
    {
        if (scrollbar.value <= 0.0f)
        {
            scrollbar.value = 0.0001f;
        }
    }

    void SaveData()
    {
        if (typeScrollbar == TypesScrollbar.BackgroundMusic)
        {
            PlayerPrefs.SetFloat("ScrollbarValue", scrollbar.value);
        }
        else
        {
            PlayerPrefs.SetFloat("ScrollbarValueSFX", scrollbar.value);
        }
    }

    void LoadData()
    {
        if (typeScrollbar == TypesScrollbar.BackgroundMusic)
        {
            scrollbar.value = PlayerPrefs.GetFloat("ScrollbarValue");
        }
        else
        {
            scrollbar.value = PlayerPrefs.GetFloat("ScrollbarValueSFX");
        }
    }

    public bool IsPayingSound() => audioSource.isPlaying;
}