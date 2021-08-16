using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [HideInInspector] public bool gameIsOver;

    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject inGameUI;

    public static GameOverManager instance;

    void Awake()
    {
        //create instance for the script
        if (instance != null)
        {
            Debug.LogWarning("There is multiple GameOverManager instances!");
            return;
        }

        instance = this;
    }

    void Start()
    {
        gameIsOver = false;
    }

    public void GameOver()
    {
        //disable player and ingame ui, enable gameover menu
        gameIsOver = true;
        PlayerController.instance.canMove = false;
        Interact.instance.canInteract = false;
        inGameUI.SetActive(false);
        gameOverUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        //save best score
        if (PlayerPrefs.GetFloat("bestScore") < ScoreCounter.instance.totalScore)
        {
            PlayerPrefs.SetFloat("bestScore", ScoreCounter.instance.totalScore);
        }
    }

    public void RetryButton()
    {
        //reload scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
    }
}
