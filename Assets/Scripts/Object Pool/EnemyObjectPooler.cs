using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectPooler : MonoBehaviour
{
    public static EnemyObjectPooler Instance;

    [SerializeField] List<GameObject> enemiesPrefab;
    [SerializeField] List<GameObject> enemyList;
    [SerializeField] int enemiesSize;

    void OnEnable()
    {
        GameManager.OnStatsUp += StatsUp;
    }

    void OnDisable()
    {
        GameManager.OnStatsUp -= StatsUp;
    }

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        AddEnemiesToPool();
    }

    void AddEnemiesToPool()
    {
        for (int i = 0; i < enemiesSize; i++)
        {
            GameObject enemy;
            float random = Random.Range(0.0f, 1f);

            if (random < 0.3)
            {
                enemy = Instantiate(enemiesPrefab[1]);
            }
            else
            {
                enemy = Instantiate(enemiesPrefab[0]);
            }

            enemy.SetActive(false);
            enemy.transform.parent = transform;
            enemyList.Add(enemy);
        }
    }

    public GameObject GetPoolObject()
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            if (!enemyList[i].activeSelf)
            {
                enemyList[i].SetActive(true);

                return enemyList[i];
            }
        }

        AddEnemiesToPool();
        enemyList[enemyList.Count - 1].SetActive(true);

        return enemyList[enemyList.Count - 1];
    }

    void StatsUp()
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            if (enemyList[i].activeInHierarchy && enemyList[i].activeSelf)
            {
                enemyList[i].GetComponent<Pathfinding.AIPath>().maxSpeed += LevelUp.Instance.LevelUpStats(enemyList[i].GetComponent<Pathfinding.AIPath>().maxSpeed);
                enemyList[i].GetComponent<Pathfinding.AIPath>().rotationSpeed += LevelUp.Instance.LevelUpStats(enemyList[i].GetComponent<Pathfinding.AIPath>().rotationSpeed);
            }
        }
    }
}