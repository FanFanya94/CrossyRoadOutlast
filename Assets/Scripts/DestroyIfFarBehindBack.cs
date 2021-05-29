using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyIfFarBehindBack : MonoBehaviour
{
    private GameObject player;
    private int distanceFromPlayerToDestroy;

    private void Start()
    {
        player = GameObject.Find("Player");
        distanceFromPlayerToDestroy = GameParameters.instance.distanceFromPlayerToDestroyModule;
    }

    void Update()
    {
        if(GameManager.instance.gameState == GameState.Playing)
        {
            if (transform.position.z < player.transform.position.z && Vector3.Distance(transform.position, player.transform.position) > distanceFromPlayerToDestroy)
            {
                Destroy(gameObject);
            }
        }
    }
}
