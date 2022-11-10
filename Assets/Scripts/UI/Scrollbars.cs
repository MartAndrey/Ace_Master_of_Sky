using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scrollbars : BaseUI
{
    Scrollbar scrollbar;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        scrollbar = GetComponent<Scrollbar>();
    }
}