using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour
{
    public int length;
    [HideInInspector] public float movingSpeed;
    public Vector3 movingDirection;
    [SerializeField] private int maxDestroyDistance = 15;
    
    private void Start()
    {
        if (transform.position.x < 0)
        {
            movingDirection = new Vector3(1, 0, 0);
        }

        if (transform.position.x > 0)
        {
            movingDirection = new Vector3(-1, 0, 0);
        }
    }

    private void Update()
    {
        if (transform.position.x > 6 || transform.position.x < -6)
        {
            transform.Translate(movingDirection * GameParameters.instance.logSpeedMultiplierInFakeZone * Time.deltaTime);
        }
        else
        {
            transform.Translate(movingDirection * movingSpeed * Time.deltaTime);
        }

        if (transform.position.x < -maxDestroyDistance || transform.position.x > maxDestroyDistance)
        {
            Destroy(gameObject);
        }
    }
}
