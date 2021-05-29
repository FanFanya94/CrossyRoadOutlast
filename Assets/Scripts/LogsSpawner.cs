using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogsSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] spawners;
    [SerializeField] GameObject[] logsPrefabs;

    [SerializeField] GameObject leftPlayerKiller;
    [SerializeField] GameObject rightPlayerKiller;

    [SerializeField] GameObject leftObstacle;
    [SerializeField] GameObject rightObstacle;

    private GameObject lastSpawnedLog = null;

    private Vector3 spawnPosition;

    private float distanceToSpawnNext;
    [SerializeField] private float logMovingSpeed;

    private int activeSpawnerIndex;

    private void Start()
    {
        logMovingSpeed = Random.Range(GameParameters.instance.MinLogsMovingSpeed, GameParameters.instance.maxLogsMovingSpeed);
        
        if(GameParameters.instance.isRightLogSpawnerActive)
        {
            activeSpawnerIndex = 0;
            GameParameters.instance.isRightLogSpawnerActive = !GameParameters.instance.isRightLogSpawnerActive;
        }
        else
        {
            activeSpawnerIndex = 1;
            GameParameters.instance.isRightLogSpawnerActive = !GameParameters.instance.isRightLogSpawnerActive;
        }

        SetKillersAndObstacles();

        spawnPosition = spawners[activeSpawnerIndex].transform.position;

        if (activeSpawnerIndex == 0)
        {
            Vector3 randomPos = new Vector3(Random.Range(1, 9), spawnPosition.y, spawnPosition.z);
            SpawnNewLog(randomPos);
        }

        else if (activeSpawnerIndex == 1)
        {
            Vector3 randomPos = new Vector3(Random.Range(-9, -1), spawnPosition.y, spawnPosition.z);
            SpawnNewLog(randomPos);
        }

        SpawnNewLog(spawnPosition);
    }

    void Update()
    {
        if (lastSpawnedLog != null && Vector3.Distance(spawnPosition, lastSpawnedLog.transform.position) > distanceToSpawnNext)
        {
            SpawnNewLog(spawnPosition);
        }
    }

    private void SpawnNewLog(Vector3 spawnPos)
    {
        int randomIndex = Random.Range(0, logsPrefabs.Length);
        GameObject log = Instantiate(logsPrefabs[randomIndex], spawnPos, logsPrefabs[randomIndex].transform.rotation);
        log.GetComponent<Log>().movingSpeed = logMovingSpeed;
        lastSpawnedLog = log.gameObject;
        lastSpawnedLog.transform.SetParent(spawners[activeSpawnerIndex].transform); 
        distanceToSpawnNext = log.gameObject.GetComponent<Log>().length + Random.Range(GameParameters.instance.minLogsSpawnDistance, GameParameters.instance.maxLogsSpawnDistance);
    }

    private void SetKillersAndObstacles()
    {
        if (activeSpawnerIndex == 0)
        {
            leftPlayerKiller.SetActive(true);
            rightObstacle.SetActive(true);
        }

        if (activeSpawnerIndex == 1)
        {
            rightPlayerKiller.SetActive(true);
            leftObstacle.SetActive(true);
        }
    }
}
