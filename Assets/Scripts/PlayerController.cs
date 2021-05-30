using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GroundChecker groundChecker;
    [SerializeField] private ObstacleChecker obstacleChecker;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Rigidbody rb;

    private GameObject targetTile;
    private Vector3 targetPos;

    [SerializeField] private float movingSpeed;

    [SerializeField] private bool isMoving;

    private void Start()
    {
        isMoving = false;
        targetTile = null;
    }

    void Update()
    {
        if (GameManager.instance.gameState == GameState.Playing)
        {
            GetInput();
            MovementProcess();
        }
        
        DestroyIfUnderground(-1f);
    }

    private void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Rotate(Directions.forward);
        }
        if (Input.GetKeyUp(KeyCode.W) && !isMoving)
        {
            StartCoroutine("CheckTargetTile");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Rotate(Directions.backward);
        }
        if (Input.GetKeyUp(KeyCode.S) && !isMoving)
        {
            StartCoroutine("CheckTargetTile");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Rotate(Directions.right);
        }
        if (Input.GetKeyUp(KeyCode.D) && !isMoving)
        {
            StartCoroutine("CheckTargetTile");
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Rotate(Directions.left);
        }
        if (Input.GetKeyUp(KeyCode.A) && !isMoving)
        {
            StartCoroutine("CheckTargetTile");
        }
    } // Get user input
    
    private void MovementProcess()
    {
        if (targetTile != null)
        {
            SetNewTargetPosition();
        }

        if (transform.position != targetPos && isMoving)
        {
            EasedMove();
        }

        if (Vector3.Distance(targetPos, transform.position) <= 0.15f)
        {
            isMoving = false;
        }

        if (!isMoving)
        {
            if (targetTile != null)
                transform.position = targetPos;
        }
    } // Main movement process

    private void DestroyIfUnderground(float value)
    {
        if(transform.position.y < value)
        {
            Destroy(gameObject);
        }
    } // Destroys object if it is lower than value along Y axis

    private void Rotate(Vector3 rotateDir) // Rotates player forward, backward, left or right 
    {
        transform.eulerAngles = rotateDir;
    }

    private void GetTargetTile() // Gets new target tile from checker 
    {
        if (groundChecker.GetTargetTile() != null && obstacleChecker.obstacle == null)
        {
            targetTile = groundChecker.GetTargetTile();
            isMoving = true;
        }
    }

    private void SetNewTargetPosition() // Set new position to move player to 
    {
        targetPos = targetTile.transform.position;
    }

    private void EasedMove() // Smoothly moves player to a new position 
    {
        transform.position = Vector3.Lerp(transform.position, targetPos, movingSpeed * Time.deltaTime);
    }

    IEnumerator CheckTargetTile() // Check if it's possible to get new target tile to move to
    {
        
        yield return new WaitForSeconds(0.05f);
        
        GetTargetTile();
        
        if (isMoving)
        {
            playerAnimator.SetTrigger("Jump");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Water" && GameManager.instance.gameState == GameState.Playing)
        {
            GameManager.instance.GameOver();
            rb.AddForce(Vector3.down * 1000, ForceMode.Impulse);
            GetComponent<Collider>().enabled = false;
            rb.constraints = RigidbodyConstraints.FreezePositionX;
            rb.constraints = RigidbodyConstraints.FreezePositionZ;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Killer" && GameManager.instance.gameState == GameState.Playing)
        {
            GameManager.instance.GameOver();
            transform.SetParent(targetTile.transform);
        }

        if (other.gameObject.tag == "Enemy" && GameManager.instance.gameState == GameState.Playing)
        {
            GameManager.instance.GameOver();

            transform.LookAt(other.transform);

            other.transform.LookAt(transform.position);
            other.gameObject.GetComponent<Enemy>().StartCoroutine("KillPlayer");

            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
        }
    }
}
