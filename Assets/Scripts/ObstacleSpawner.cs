using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private int minObstacleCount;
    [SerializeField] private int maxObstacleCount;
    [SerializeField] private int obstacleCount = 0;

    [SerializeField] private GameObject[] obstaclePrefabs;

    private GameObject[] childTiles;

    void Awake()
    {
        obstacleCount = Random.Range(minObstacleCount, maxObstacleCount); // Generate random obstacle count between minObstacleCount and maxObstacleCount
        
        childTiles = new GameObject[transform.childCount];

        FillArrayWithChilds(childTiles); 

        ShuffleArray(childTiles);
        ShuffleArray(obstaclePrefabs);

        SpawnObstacleRandomly(obstacleCount, childTiles, obstaclePrefabs); 
    }

    private void FillArrayWithChilds(GameObject[] array) // Fill childTiles array with child objects
    {
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = transform.GetChild(i).gameObject;
        }
    }

    //private void SpawnObstacleRandomly(int count, GameObject[] objectsArray, GameObject[] prefabsArray) // Spawn obstacle at random tile position
    //{
    //    for (int i = 0; i < count; i++)
    //    {
    //        GameObject obs = Instantiate(prefabsArray[i], objectsArray[i].transform.position, Quaternion.identity);
    //        obs.transform.SetParent(objectsArray[i].transform);
    //    }
    //}

    private void SpawnObstacleRandomly(int count, GameObject[] tilesArray, GameObject[] prefabsArray) // Spawn obstacle at random tile position
    {
        for (int i = 0, j = 0; i < count; i++, j++)
        {
            if (j >= prefabsArray.Length)
            {
                j = 0;
            }
            GameObject obs = Instantiate(prefabsArray[j], tilesArray[i].transform.position, Quaternion.identity);
            obs.transform.SetParent(tilesArray[i].transform);
        }
    }

    private void ShuffleArray(GameObject[] array) // Randomly shuffle childTiles array
    {
        for (int i = array.Length - 1; i >= 1; i--)
        {
            int j = Random.Range(0, i + 1);
            GameObject temp = array[j];
            array[j] = array[i];
            array[i] = temp;
        }
    }
}
