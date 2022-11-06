using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    Fade fade;
    
    public void Play()
    {
        fade = FindObjectOfType<Fade>();

        StartCoroutine(PlayRutiner());
    }

    IEnumerator PlayRutiner()
    {
        yield return new WaitForSeconds(1);
        fade.FadeIn();
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("GameScene");
    }
}