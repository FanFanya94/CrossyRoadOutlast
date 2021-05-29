using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] spawners;
    [SerializeField] GameObject[] enemiesPrefabs;
    private GameObject lastSpawnedEnemy = null;

    private Vector3 spawnPosition;

    private float distanceToSpawnNext;
    private float enemyMovingSpeed;

    private int activeSpawnerIndex;

    void Start()
    {
        enemyMovingSpeed = Random.Range(GameParameters.instance.minEnemiesMovingSpeed, GameParameters.instance.maxEnemiesMovingSpeed);
        activeSpawnerIndex = Random.Range(0, 2);
        spawnPosition = spawners[activeSpawnerIndex].transform.position;

        if (activeSpawnerIndex == 0)
        {
            Vector3 randomPos = new Vector3(Random.Range(1, 5), spawnPosition.y, spawnPosition.z);
            SpawnNewEnemy(randomPos);
            randomPos = new Vector3(Random.Range(5, 10), spawnPosition.y, spawnPosition.z);
            SpawnNewEnemy(randomPos);
        }

        else if(activeSpawnerIndex == 1)
        {
            Vector3 randomPos = new Vector3(Random.Range(-5, -1), spawnPosition.y, spawnPosition.z);
            SpawnNewEnemy(randomPos);
            randomPos = new Vector3(Random.Range(-10, -5), spawnPosition.y, spawnPosition.z);
            SpawnNewEnemy(randomPos);
        }

        SpawnNewEnemy(spawnPosition);
    }


    void Update()
    {
        if (lastSpawnedEnemy != null && Vector3.Distance(spawnPosition, lastSpawnedEnemy.transform.position) > distanceToSpawnNext)
        {
            SpawnNewEnemy(spawnPosition);
        }
    }

    private void SpawnNewEnemy(Vector3 spawnPos)
    {
        int randomIndex = Random.Range(0, enemiesPrefabs.Length);
        GameObject enemy = Instantiate(enemiesPrefabs[randomIndex], spawnPos, Quaternion.identity);
        enemy.GetComponent<Enemy>().movingSpeed = enemyMovingSpeed;
        lastSpawnedEnemy = enemy.gameObject;
        lastSpawnedEnemy.transform.SetParent(spawners[activeSpawnerIndex].transform);
        distanceToSpawnNext = Random.Range(GameParameters.instance.minEnemiesSpawnDistance, GameParameters.instance.maxEnemiesSpawnDistance);
    }
}
