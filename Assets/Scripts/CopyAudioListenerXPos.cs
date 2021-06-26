using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyAudioListenerXPos : MonoBehaviour
{
    [SerializeField] private GameObject _audioListener;

    private void Awake()
    {
        _audioListener = GameObject.Find("AudioListener");    
    }

    private void Update()
    {
        transform.position = new Vector3(_audioListener.transform.position.x, transform.position.y, transform.position.z);
    }
}
