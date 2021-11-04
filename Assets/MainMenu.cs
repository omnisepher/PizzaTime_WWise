using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject infoTab;
    public GameObject menuTab;
    public GameObject mainCanvas;
    public GameObject endLevelCanvas;

    private void Awake()
    {
        print("reloaded");
        menuTab.SetActive(false);
        mainCanvas.SetActive(true);
        endLevelCanvas.SetActive(false);
    }

    private void Update()
    {
        if (GameManager.levelClear || GameManager.levelLose)
        {
            menuTab.SetActive(false);
            mainCanvas.SetActive(false);
            endLevelCanvas.SetActive(true);
        }
    }
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
        Time.timeScale = menuTab.active ? 0f : 1f;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
