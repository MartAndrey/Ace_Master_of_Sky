using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] int poolSize = 10;
    [SerializeField] List<GameObject> bulletList;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        AddBulletToPool(poolSize);
    }

    void AddBulletToPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bulletList.Add(bullet);
            bullet.transform.parent = transform;
        }
    }

    public GameObject GetPoolObject()
    {
        for (int i = 0; i < bulletList.Count; i++)
        {
            if (!bulletList[i].activeSelf)
            {
                bulletList[i].SetActive(true);

                return bulletList[i];
            }
        }

        AddBulletToPool(1);
        bulletList[bulletList.Count - 1].SetActive(true);

        return bulletList[bulletList.Count - 1];
    }
}