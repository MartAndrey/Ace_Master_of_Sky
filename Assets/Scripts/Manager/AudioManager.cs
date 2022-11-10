using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    bool isFirstTime;

    [SerializeField] AudioMixer audioMixerBackground;
    [SerializeField] AudioMixer audioMixerSFX;

    private void Start()
    {
        isFirstTime = false;

        ControlAudioBackground(PauseMenuController.Instance.ScrollbarValueBackground);
        ControlAudioSFX(PauseMenuController.Instance.ScrollBarValueSFX);
    }

    public void ControlAudioBackground(float scrollbarAudio)
    {
        audioMixerBackground.SetFloat("Background Music", Mathf.Log10(scrollbarAudio) * 20);

        if (isFirstTime)
        {
            Scrollbars scrollbar = FindObjectOfType<Scrollbars>();
            scrollbar.OverSound();
        }
    }

    public void ControlAudioSFX(float scrollbarAudio)
    {
        audioMixerSFX.SetFloat("Sound Effects", Mathf.Log10(scrollbarAudio) * 20);

        if (isFirstTime)
        {
            Scrollbars scrollbar = FindObjectOfType<Scrollbars>();
            scrollbar.OverSound();
        }

        isFirstTime = true;
    }
}