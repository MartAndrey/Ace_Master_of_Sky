using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : BaseUI
{
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

    public override void OverSound()
    {
        if (this.GetComponent<Button>().interactable == true)
            audioSource.PlayOneShot(audioOver);
    }
}