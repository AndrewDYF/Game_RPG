using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    CanvasGroup CanvasGroup;//控制ui

    public GameObject menu;
    public GameObject helppage;
    


    void Start()
    {
        
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);//进入下一个场景
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void GoHelpPage()
    {
        helppage.SetActive(true);
        menu.SetActive(false);
    }

    public void BackMainMenu()
    {
        helppage.SetActive(false);
        menu.SetActive(true);
    }

}
