using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleChecker : MonoBehaviour
{
    [HideInInspector] public GameObject obstacle;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Obstacle")
        {
            obstacle = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Obstacle")
        {
            obstacle = null;
        }
    }
}
