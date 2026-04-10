using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private GameObject startCanvas;

    private bool gameStarted = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        Time.timeScale = 0f;
    }

    private void Start()
    {
        startCanvas.SetActive(true);
        gameOverCanvas.SetActive(false);
    }

    public void StartGame()
    {
        gameStarted = true;
        startCanvas.SetActive(false);
        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        if (!gameStarted)
        {
            return;
        }
        
        gameOverCanvas.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}