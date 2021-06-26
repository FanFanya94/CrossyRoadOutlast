using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathSensor : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private PlayerController _playerController;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Water" && GameManager.instance.gameState == GameState.Playing)
        {
            FindObjectOfType<AudioManager>().PlaySound("WaterSplash");
            GameManager.instance.GameOver();
            _rb.AddForce(Vector3.down * 1000, ForceMode.Impulse);
            GetComponent<Collider>().enabled = false;
            _rb.constraints = RigidbodyConstraints.FreezePositionX;
            _rb.constraints = RigidbodyConstraints.FreezePositionZ;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Killer" && GameManager.instance.gameState == GameState.Playing)
        {
            GameManager.instance.GameOver();
            transform.SetParent(_playerController.targetTile.transform);
        }

        if (other.gameObject.tag == "Enemy" && GameManager.instance.gameState == GameState.Playing)
        {
            FindObjectOfType<AudioManager>().PlaySound("DeathScream");
            GameManager.instance.GameOver();

            transform.LookAt(other.transform);

            other.transform.LookAt(transform.position);
            other.gameObject.GetComponent<Enemy>().StartCoroutine("KillPlayer");

            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
        }
    }
}
