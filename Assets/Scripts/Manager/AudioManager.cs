using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixerBackground;
    [SerializeField] AudioMixer audioMixerSFX;
    [SerializeField] Scrollbars scrollbarBackground;
    [SerializeField] Scrollbars scrollbarSFX ;

    private void Start()
    {
        ControlAudioBackground(scrollbarBackground.ScrollBarValue);
        ControlAudioSFX(scrollbarSFX.ScrollBarValue);
    }

    public void ControlAudioBackground(float scrollbarAudio)
    {
        audioMixerBackground.SetFloat("Background Music", Mathf.Log10(scrollbarAudio) * 20);
    }

    public void ControlAudioSFX(float scrollbarAudio)
    {
        audioMixerSFX.SetFloat("Sound Effects", Mathf.Log10(scrollbarAudio) * 20);
    }
}