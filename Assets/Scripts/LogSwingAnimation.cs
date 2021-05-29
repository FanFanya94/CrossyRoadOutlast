using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogSwingAnimation : MonoBehaviour
{
    [SerializeField] private Vector3 swingPosition;
    [SerializeField] private Vector3 defaultPosition;
    [SerializeField] private bool isSwinging = false;
    [SerializeField] private float swingSpeed;

    private void Start()
    {
        swingSpeed = GameParameters.instance.logSwingSpeed;    
    }

    void Update()
    {
        if (Vector3.Distance(transform.localPosition, swingPosition) < 0.01f)
        {
            isSwinging = false;
        }

        if (isSwinging)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, swingPosition, swingSpeed * Time.deltaTime);
        }

        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, defaultPosition, swingSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerModel")
        {
            isSwinging = true;
        }
    }
}
