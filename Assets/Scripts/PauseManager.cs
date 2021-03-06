using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [HideInInspector] public bool gameIsPaused = false;

    [SerializeField] GameObject pauseUI;
    [SerializeField] GameObject settingsUI;
    [SerializeField] Transform content;

    public static PauseManager instance;

    void Awake()
    {
        //create instance for the script
        if (instance != null)
        {
            Debug.LogWarning("There is multiple PauseManager instances!");
            return;
        }

        instance = this;
    }

    void Update()
    {
        //detect pause input
        if (Input.GetKeyDown(KeyCode.Escape) &! GameOverManager.instance.gameIsOver)
        {
            if (!gameIsPaused)
            {
                //stop time and display pause menu
                PlayerController.instance.canMove = false;
                Time.timeScale = 0f;
                pauseUI.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                gameIsPaused = true;
            }
            else
            {
                ResumeButton();
            }
        }
    }

    public void ResumeButton()
    {
        //unpause game
        Time.timeScale = 1f;
        PlayerController.instance.canMove = true;
        settingsUI.SetActive(false);
        pauseUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameIsPaused = false;
    }

    public void RestartButton()
    {
        //reload scene
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SettingsButton()
    {
        settingsUI.SetActive(true);
    }

    public void MainMenuButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
