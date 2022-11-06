using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    public static PauseMenuController Instance;

    Animator animator;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        
        animator = GetComponent<Animator>();
    }

    public void FadeIn()
    {
        animator.enabled = true;
        animator.Play("FadeIn");
    }

    public void FadeOut()
    {
        animator.SetTrigger("Transition");
    }
}