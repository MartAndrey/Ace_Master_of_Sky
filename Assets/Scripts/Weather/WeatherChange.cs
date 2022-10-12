using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WeatherChange : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(GetWeather());
    }
    
    IEnumerator GetWeather()
    {
        UnityWebRequest www = UnityWebRequest.Get("TokenWeather");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
        }
    }
}
