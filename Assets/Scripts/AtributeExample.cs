using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtributeExample : MonoBehaviour
{
    [SerializeField] int number;
    [Space, Header ("Hello"), SerializeField, TextArea(2,6)] string nma;
}
