using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void Start()
    {
    }

 
    public void ExitButtonClicked()
    {
        Application.Quit();
    }

    public void PlayButtonClicked()
    {
        SceneManager.LoadScene("Main");
    }

    public void VRButtonClicked()
    {
        //SceneManager.LoadScene("VRScene");
    }
}
