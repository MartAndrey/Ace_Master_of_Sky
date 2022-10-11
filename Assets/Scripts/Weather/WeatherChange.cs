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
        UnityWebRequest www = UnityWebRequest.Get("https://api.openweathermap.org/data/2.5/weather?q=bogota&appid=865e00e1d05dfa074e0eb466d5a9b0e1");
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
