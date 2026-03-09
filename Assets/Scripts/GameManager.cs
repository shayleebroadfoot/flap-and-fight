using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    //  have a singleton so you can call it from anywhere
    public static GameManager instance;

    [SerializeField] private GameObject gameOverCanvas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        // set back to normal at start of game
        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        gameOverCanvas.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        // restand by loading current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
