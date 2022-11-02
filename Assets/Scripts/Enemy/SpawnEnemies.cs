using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public static SpawnEnemies Instance;

    [SerializeField] PlayerController player;
    [SerializeField] GameObject[] listSpawn;
    void Awake()

    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void ActiveEnemy()
    {
        int random = Random.Range(0, listSpawn.Length);
        GameObject enemy = EnemyObjectPooler.Instance.GetPoolObject();
        enemy.transform.position = listSpawn[random].transform.position;
        enemy.GetComponent<Pathfinding.AIDestinationSetter>().target = player.transform;
    }
}