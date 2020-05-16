using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update


    public PlayController ac;
    //public KeyboardInput ki;
    //public JoystickInput ji;

    public GameObject player;
    public GameObject pausemenu;
    public GameObject helppage;
    public void ContinueGame()
    {
        pausemenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void GoHelpPage()
    {
        helppage.SetActive(true);
        pausemenu.SetActive(false);
    }

    public void BackPauseMenu()
    {
        helppage.SetActive(false);
        pausemenu.SetActive(true);
    }

    public void ChooseKeyboard()
    {
        //ac.GetComponent<KeyboardInput>().enabled = true;
        //ac.GetComponent<JoystickInput>().enabled = false;
        ac.GetComponent<PlayController>().pi = new KeyboardInput();
    }

    public void ChooseJoystick()
    {
        ac.GetComponent<KeyboardInput>().enabled = false;
        ac.GetComponent<JoystickInput>().enabled = true;
        ac.GetComponent<PlayController>().pi = new JoystickInput();





    }

}
