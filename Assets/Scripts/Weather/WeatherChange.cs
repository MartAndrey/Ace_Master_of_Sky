using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using LitJson;

public class WeatherChange : MonoBehaviour
{
    int currentWeather;

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
            JsonData jsonData = JsonMapper.ToObject(www.downloadHandler.text);
            currentWeather = (int ) jsonData["weather"] [0] ["id"];
            Debug.Log(currentWeather);
        }
    }
}
