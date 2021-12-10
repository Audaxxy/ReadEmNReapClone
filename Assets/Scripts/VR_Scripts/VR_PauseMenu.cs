using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using UnityEngine.XR.Interaction;

public class VR_PauseMenu : MonoBehaviour
{
    public Image Menu;

    public bool _inPauseMenu = false;

    public PlayerController PC;
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if(player != null)
        {
            PC = player.GetComponent<PlayerController>();
        }
        else
        {
            Debug.Log("The pause menu couldn't find player");
        }

        SetUIEnabled(false);
        Debug.Log("BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB");
    }

    // Update is called once per frame
    void Update()
    {
        //if(PC == null)
        //{
        //    //No player which means there is no need to pause
        //    return;
        //}
        //Debug.Log("CCCCCCCCCCCCCCCCCCCCCCCCCCCCC");
        //I am used to Unreal where Esc would close the editor so my alternate button was P
        if (Input.GetButtonDown("XRI_Left_SecondaryButton") || Input.GetButtonDown("XRI_Right_SecondaryButton"))
        {
            Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
            //TogglePause();
        }
    }

    void TogglePause()
    {
        //Toggle Pause
        _inPauseMenu = !_inPauseMenu;

        PC.SetInPauseMenu(_inPauseMenu);

        SetUIEnabled(_inPauseMenu);
    }

    void SetUIEnabled(bool en/*abled*/)
    {
        if (Menu != null)
        {
            Menu.gameObject.SetActive(en);
        }
    }

    public void OnClickedExit()
    {
        Application.Quit();
    }

    public void OnClickedResume()
    {
        TogglePause();
    }

    public void OnClickedMainMenu()
    {
        SceneManager.LoadScene("TempMainMenuScene");
    }

}
