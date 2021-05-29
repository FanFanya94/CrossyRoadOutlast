using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] levelModules;
    [SerializeField] private GameObject player;
    [SerializeField] private int startModlulesCount;
    private Vector3 spawnPosition;
    private Vector3 playerPosition;

    private void Awake()
    {
        spawnPosition = transform.position;
    }

    void Start()
    {
        playerPosition = player.transform.position;

        for (int i = 0; i < startModlulesCount; i++)
        {
            SpawnNextModule();
            UpdateSpawnPosition();
        }
    }

    void Update()
    {
        if (GameManager.instance.gameState == GameState.Playing && player.transform.position.z - playerPosition.z  >= 1)
        {
            playerPosition = player.transform.position;
            SpawnNextModule();
            UpdateSpawnPosition();
            GameManager.instance.UpdateScore();
        }
    }

    private void SpawnNextModule()
    {
        int randomIndex = Random.Range(0, levelModules.Length);
        Instantiate(levelModules[randomIndex], spawnPosition, Quaternion.identity);
    }

    private void UpdateSpawnPosition()
    {
        spawnPosition += new Vector3(0, 0, 1);
    }
}
