using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPoolItem
{
    public GameObject objectToPool;
    public int amountToPool;
}

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;

    public List<ObjectPoolItem> itemsToPool;
    public List<GameObject> instancedObjects;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        instancedObjects = new List<GameObject>();

        foreach (ObjectPoolItem item in itemsToPool)
        {
            
        }
    }
}
