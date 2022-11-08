using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;

    void Start()
    {
        ControlAudio(PauseMenuController.Instance.ScrollbarValue);       
    }

    public void ControlAudio(float scrollbarAudio)
    {
        audioMixer.SetFloat("Music Volume", Mathf.Log10(scrollbarAudio) * 20);
    }
}
