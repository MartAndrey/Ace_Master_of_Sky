using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManuManager : MonoBehaviour
{
    public void Play()
    {
        Fade fade = FindObjectOfType<Fade>();
        fade.FadeIn();
    
        StartCoroutine(PlayRutiner());
    }

    IEnumerator PlayRutiner()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Main");
    }
}
