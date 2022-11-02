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

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            other.collider.GetComponent<PlayerLife>().Death();
        }
    }
}