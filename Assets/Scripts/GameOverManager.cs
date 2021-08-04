using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
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

    public void GameOver()
    {
        //disable player and ingame ui, enable gameover menu
        PlayerController.instance.canMove = false;
        Interact.instance.canInteract = false;
        inGameUI.SetActive(false);
        gameOverUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RetryButton()
    {
        //reload scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenuButton()
    {

    }
}
