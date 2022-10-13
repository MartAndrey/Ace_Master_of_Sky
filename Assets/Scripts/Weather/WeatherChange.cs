using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using LitJson;

public class WeatherChange : MonoBehaviour
{
    [SerializeField] DigitalRuby.RainMaker.RainScript2D rainMaker;

    int currentWeather;

    void Start()
    {
        StartCoroutine(GetWeather());
    }

    void WeatherChanger()
    {

        if (currentWeather >= 200 && currentWeather < 300)
        {
            // Storm
            rainMaker.RainIntensity += 1;
        }
        else if (currentWeather >= 300 && currentWeather < 400)
        {
            // Drizzle
            rainMaker.RainIntensity += 0.2f;
        }
        else if (currentWeather >= 400 && currentWeather < 500)
        {
            // Rain
            rainMaker.RainIntensity += 0.55f;
        }
        else if (currentWeather >= 500 && currentWeather < 600)
        {
            // Rain
            rainMaker.RainIntensity += 0.7f;
        }
        else if (currentWeather >= 700 && currentWeather < 800)
        {
            // Fog
            rainMaker.RainIntensity += 0.1f;
        }
        else if (currentWeather > 800)
        {
            // Clouds
            rainMaker.RainIntensity += 0.1f;
        }
        else if (currentWeather == 800)
        {
            // ClearSky
            rainMaker.gameObject.SetActive(false);
        }
    }

    IEnumerator GetWeather()
    {
        UnityWebRequest www = UnityWebRequest.Get("TokenWeather");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(www.error);
            currentWeather = 800;
        }
        else
        {
            JsonData jsonData = JsonMapper.ToObject(www.downloadHandler.text);
            currentWeather = (int)jsonData["weather"][0]["id"];
        }

        WeatherChanger();
        StopCoroutine(GetWeather());
    }
}
