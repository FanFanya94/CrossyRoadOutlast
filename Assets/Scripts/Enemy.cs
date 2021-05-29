using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public float movingSpeed;
    public Vector3 movingDirection;
    public Vector3 enemyCurrentRotation;
    public bool isHitting = false;

    void Start()
    {
        if (transform.position.x < 0)
        {
            RotateEnemy(Directions.right);
        }

        if (transform.position.x > 0)
        {
            RotateEnemy(Directions.left);
        }

        
        SetMovingDirection();
    }

    void Update()
    {
        if (!isHitting)
        {
            transform.Translate(movingDirection * movingSpeed * Time.deltaTime, Space.World);
        }
    }

    private void RotateEnemy(Vector3 direction)
    {
            transform.eulerAngles = direction;
            enemyCurrentRotation = transform.eulerAngles;
    }

    private void SetMovingDirection()
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

    public IEnumerator KillPlayer()
    {
        isHitting = true;

        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);

        yield return new WaitForSeconds(0.25f);

        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(0).gameObject.SetActive(true);

        RotateEnemy(enemyCurrentRotation);

        isHitting = false;
    }
}
