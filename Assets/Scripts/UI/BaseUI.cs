using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseUI : MonoBehaviour
{
    [SerializeField] AudioClip audioPlay;
    [SerializeField] protected AudioClip audioOver;

    [SerializeField] Image imageResume;
    [SerializeField] TMP_Text textResume;

    protected AudioSource audioSource;

    public void PlaySound()
    {
        audioSource.PlayOneShot(audioPlay);
    }

    public virtual void OverSound()
    {
        audioSource.PlayOneShot(audioOver);
    }

    public void Select(bool IsResume = false)
    {
        if (IsResume)
        {
            imageResume.color = Color.white;
            textResume.color = Color.white;
        }

        OverSound();
    }

    public void Deselect(bool IsResume = false)
    {
        if (IsResume)
        {
            imageResume.color = Color.black;
            textResume.color = Color.black;
        }

        OverSound();
    }
}