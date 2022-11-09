using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scrollbars : MonoBehaviour
{
    [SerializeField] AudioClip audioPlay;
    [SerializeField] AudioClip audioOver;

    AudioSource audioSource;
    Scrollbar button;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        button = GetComponent<Scrollbar>();
    }

    void Start()
    {
        // button.onClick.AddListener(PlaySound);
    }

    void PlaySound()
    {
        audioSource.PlayOneShot(audioPlay);
    }

    public void OverSound()
    {
        audioSource.PlayOneShot(audioOver);
    }
}

