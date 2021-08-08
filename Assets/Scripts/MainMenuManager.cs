using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] int startScene;

    [SerializeField] GameObject settingsMenu;

    public void StartGameButton()
    {
        SceneManager.LoadScene(startScene);
    }

    public void SettingsButton()
    {
        settingsMenu.SetActive(true);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void CreditsButton()
    {
        System.Diagnostics.Process.Start("https://youtu.be/dQw4w9WgXcQ");
    }
}
