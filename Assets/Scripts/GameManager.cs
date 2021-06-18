using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public enum GameState { Start, Playing, GameOver };

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject startMenu;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject cameraSpotLight;
    [SerializeField] private GameObject globalVolume;

    [HideInInspector] public static GameManager instance;
    [HideInInspector] public GameState gameState;

    [HideInInspector] public int score = 0;

    private void Awake()
    {
        instance = this;
        gameState = GameState.Start;
    }

    private void Update()
    {
        CameraZoom();
    }

    private void CameraZoom()
    {
        if (gameState == GameState.GameOver)
        {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 2.2f, 3f * Time.deltaTime);
        }
    }

    public void StartGame()
    {
        gameState = GameState.Playing;
        startMenu.gameObject.SetActive(false);
        Invoke("EnableNightVision", 1f);
    }

    public void GameOver()
    {
        gameState = GameState.GameOver;
        gameOverPanel.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void UpdateScore()
    {
        score++;
        scoreText.text = "SCORE: " + score;
    }

    public void EnableNightVision()
    {
        FindObjectOfType<AudioManager>().PlaySound("NightVision");
        globalVolume.SetActive(true);
        cameraSpotLight.SetActive(true);
    }
}
