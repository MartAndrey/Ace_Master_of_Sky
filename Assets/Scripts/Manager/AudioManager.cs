using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixerBackground;
    [SerializeField] AudioMixer audioMixerSFX;
    [SerializeField] Scrollbars scrollbarsBackgroundMusic;
    [SerializeField] Scrollbars scrollbarsSFX;

    bool isFirstTime = true;

    private void Start()
    {
        ControlAudioBackground(scrollbarsBackgroundMusic.ScrollBarValue);
        ControlAudioSFX(scrollbarsSFX.ScrollBarValue);

        StartCoroutine(SwitchValueIsFirstTime());
    }

    public void ControlAudioBackground(float scrollbarAudio)
    {
        audioMixerBackground.SetFloat("Background Music", Mathf.Log10(scrollbarAudio) * 20);

        if (!isFirstTime && !scrollbarsBackgroundMusic.IsPayingSound())
        {
            scrollbarsBackgroundMusic.OverSound();
        }
    }

    public void ControlAudioSFX(float scrollbarAudio)
    {
        audioMixerSFX.SetFloat("Sound Effects", Mathf.Log10(scrollbarAudio) * 20);

        if (!isFirstTime && !scrollbarsSFX.IsPayingSound())
        {
            scrollbarsSFX.OverSound();
        }
    }

    IEnumerator SwitchValueIsFirstTime()
    {
        yield return new WaitForSeconds(1);
        isFirstTime = false;
    }
}