using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject infoTab;
    public GameObject menuTab;
    public GameObject mainCanvas;

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Info()
    {
        infoTab.active = !infoTab.active;

    }
    public void Exit()
    {
        Application.Quit(0);
    }

    public void OpenMenu()
    {
        menuTab.active = !menuTab.active;
        mainCanvas.active = !mainCanvas.active;
    }

}
