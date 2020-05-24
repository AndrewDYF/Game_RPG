using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    // Start is called before the first frame update
    ActorManager am;
    //StateManager sm;

    public float maxHP=100f;
    public float currentHP=80f;

    public Image HPBar;
    public Image HPBarBack;

    public Text levelnum;

    public GameObject helppage;
    public GameObject pausemenu;


    public bool coolingDown = true;
    public bool pause = false;

    //public bool pause;







    void Start()
    {
        //HPBar.fillAmount = 0.5f;
        currentHP = maxHP;
        
        
        am = GameObject.FindGameObjectWithTag("Player").GetComponent<ActorManager>();
        if (coolingDown)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {

        //if (pausemenu.activeSelf)//如果已经暂停
        //{
        //    pausemenu.SetActive(false);
        //    Cursor.lockState = CursorLockMode.Locked;
        //}
        //else
        //{
        //    pausemenu.SetActive(true);
        //    helppage.SetActive(false);
        //    Cursor.lockState = CursorLockMode.None;
        //
        //}

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("btn7"))
        {
            if (helppage.activeSelf == false)
            {
                if (pause == false)
                {
                    pausemenu.SetActive(true);
                    helppage.SetActive(false);
                    Cursor.lockState = CursorLockMode.None;
                    PauseGame();
                }
                else
                {
                    pausemenu.SetActive(false);

                    Cursor.lockState = CursorLockMode.Locked;
                    StartGame();
                }

            }
            else
            {
                pausemenu.SetActive(true);
                helppage.SetActive(false);
            }
        }


        
        if (coolingDown == true)
        {
            levelnum.text = "lv. "+am.sm.playerLv.ToString();
            currentHP = am.sm.HP;
            maxHP = am.sm.HPmax;
            HPBar.fillAmount = currentHP/maxHP;
            if (HPBarBack.fillAmount > HPBar.fillAmount)
            {
                HPBarBack.fillAmount -= 0.01f;
            }
            else
            {
                HPBarBack.fillAmount = HPBar.fillAmount;
            }
        }
        

    }




    public void StartGame()
    {
        pause = false;
        Time.timeScale = 1;

    }
    public void PauseGame()
    {
        pause = true;
        Time.timeScale = 0;
    }

}
