using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    public static PauseMenuController Instance;

    public bool IsTransition { get; set; }
    Animator animator;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        animator = GetComponent<Animator>();
        IsTransition = false;
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

    public IEnumerator CheckTransition()
    {
        IsTransition = true;
        yield return new WaitForSecondsRealtime(1);
        IsTransition = false;
    }
}