using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameParameters : MonoBehaviour
{
    public static GameParameters instance;

    [HideInInspector] public bool isRightLogSpawnerActive = true; 

    public float logSwingSpeed;

    public float MinLogsMovingSpeed;
    public float maxLogsMovingSpeed;

    public float logSpeedMultiplierInFakeZone;

    public int minLogsSpawnDistance;
    public int maxLogsSpawnDistance;

    public int minEnemiesSpawnDistance;
    public int maxEnemiesSpawnDistance;

    public float minEnemiesMovingSpeed;
    public float maxEnemiesMovingSpeed;

    public int distanceFromPlayerToDestroyModule;

    private void Awake()
    {
        instance = this;
    }
}
