// using UnityEngine;
// using UnityEngine.SceneManagement;

// public class GameManager : MonoBehaviour
// {

//     //  have a singleton so you can call it from anywhere
//     public static GameManager instance;

//     [SerializeField] private GameObject gameOverCanvas;
//     [SerializeField] GameObject startCanvas;
//     // Start is called once before the first execution of Update after the MonoBehaviour is created
//     void Start()
//     {
//         startCanvas.SetActive(true);
//     }

//     // Update is called once per frame
//     void Update()
//     {

//     }

//     private void Awake()
//     {
//         if (instance == null)
//         {
//             instance = this;
//             startCanvas.SetActive(false);
//         }

//         // set back to normal at start of game
//         Time.timeScale = 1f;
//     }

//     public void GameOver()
//     {
//         gameOverCanvas.SetActive(true);
//         Time.timeScale = 0f;
//     }

//     public void RestartGame()
//     {
//         // restand by loading current scene
//         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
//     }
// }
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