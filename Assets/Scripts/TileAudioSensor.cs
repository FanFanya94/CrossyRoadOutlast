using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileAudioSensor : MonoBehaviour
{
    [SerializeField] private string _soundName;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PlayerModel" && GameManager.instance.gameState == GameState.Playing)
        {
            FindObjectOfType<AudioManager>().PlaySound(_soundName);
        }
    }
}
