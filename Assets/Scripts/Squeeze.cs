using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squeeze : MonoBehaviour
{
    private Vector3 defaultScale;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Vector3 squeezeScale;
    [SerializeField] private float squeezeSpeed;
    [SerializeField] private float backSqueezeSpeed;

    void Start()
    {
        defaultScale = transform.localScale;
    }

    void Update()
    {
        if (GameManager.instance.gameState == GameState.Playing)
        {
            if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
            {
                transform.localScale = Vector3.Lerp(transform.localScale, squeezeScale, squeezeSpeed * Time.deltaTime);
            }

            else
            {
                transform.localScale = Vector3.Lerp(transform.localScale, defaultScale, backSqueezeSpeed * Time.deltaTime);
            }
        }        
    }
}
