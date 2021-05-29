using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public GameObject preferedTile;
    public GameObject secondaryTile;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Ground")
        {
            preferedTile = other.gameObject;
        }

        if (other.tag == "Water")
        {
            secondaryTile = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ground")
        {
            preferedTile = null;
        }
        if(other.tag == "Water")
        {
            secondaryTile = null;
        }
    }

    public GameObject GetTargetTile()
    {
        GameObject targetTile = null;
        
        if (preferedTile != null)
        {
            targetTile = preferedTile;
        }

        else if (preferedTile == null && secondaryTile != null)
        {
            targetTile = secondaryTile;
        }

        else
        {
            targetTile = null;
        }

        return targetTile;
    }
}
