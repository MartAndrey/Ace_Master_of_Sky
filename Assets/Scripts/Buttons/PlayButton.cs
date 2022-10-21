using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    AudioSource audioSource;
    Button button;
    Animator animator;

    [SerializeField] AudioClip audioPlay;
    [SerializeField] AudioClip audioOver;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        button = GetComponent<Button>();
        animator = GetComponent<Animator>();
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
