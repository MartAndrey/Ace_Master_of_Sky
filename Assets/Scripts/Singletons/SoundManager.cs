using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] AudioClip shoot;
    
    AudioSource audioSource;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        audioSource = GetComponent<AudioSource>();

        DontDestroyOnLoad(gameObject);
    }

    public void PlayShoot()
    {
        audioSource.PlayOneShot(shoot);
    }
}
