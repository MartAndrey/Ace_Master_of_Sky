using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TernaryOp : MonoBehaviour
{
    public string a = "1";
    public string b = "1";

    void Start()
    {
        string name = a == b? "Same" : "Not same";
        Debug.Log(name);
    }

    
}
