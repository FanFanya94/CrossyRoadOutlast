using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Vector3 chasingPoint;
    [SerializeField] private float chasingSpeed;
    private float xPos;
    private float yPos;
    private float zPos;

    private void Awake()
    {
        chasingPoint = player.transform.position;
    }

    void Update()
    {
        if (GameManager.instance.gameState == GameState.Playing)
        {
            xPos = player.transform.position.x;
            yPos = player.transform.position.y;

            if (transform.position.z < player.transform.position.z)
            {
                zPos = player.transform.position.z;
            }

            else
            {
                zPos = transform.position.z;
            }

            chasingPoint = new Vector3(Mathf.Clamp(xPos, -3, 3), yPos, zPos);
            transform.position = Vector3.Lerp(transform.position, chasingPoint, chasingSpeed);
        }
    }
}
