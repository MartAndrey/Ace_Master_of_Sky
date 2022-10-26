using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sea : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Color color;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ChangeSeaColor(bool changeColor)
    {
        if (changeColor)
        {
            ColorUtility.TryParseHtmlString("#477CFF", out color);
            spriteRenderer.color = color;
        }
        else
        {
            ColorUtility.TryParseHtmlString("#26F5F5", out color);
            spriteRenderer.color = color;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerLife>().Death();
        }
    }
}
