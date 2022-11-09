using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    [SerializeField] AudioClip audioPlay;
    [SerializeField] AudioClip audioOver;

    AudioSource audioSource;
    Button button;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        button = GetComponent<Button>();
    }

    void Start()
    {
        button.onClick.AddListener(PlaySound);
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
