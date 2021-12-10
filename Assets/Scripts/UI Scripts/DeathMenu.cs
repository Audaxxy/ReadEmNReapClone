using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMenu : MonoBehaviour
{
    public UnityEngine.UI.Image Menu;

    void Start()
    {
        Menu.gameObject.SetActive(false);
    }

    public void OpenDeathMenu()
    {
        Menu.gameObject.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("TempMainMenuScene");
    }

    public void PlayAgain()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }

}
