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
    public float EnemyMaxHP = 100f;
    public float EnemyCurrentHP = 80f;

    public Image HPBar;
    public Image HPBarBack;
    public Image EnemyHPBar;
    public Image EnemyHPFrame;

    public Text levelnum;

    public GameObject enemy;
    public GameObject helppage;
    public GameObject pausemenu;
    public GameObject item;

    DummyIUserInputFix DAI;


    public bool coolingDown = true;
    public bool pause = false;

    //public bool pause;







    void Start()
    {
        enemy = GameObject.FindWithTag("Enemy");
        
        //HPBar.fillAmount = 0.5f;
        currentHP = maxHP;
        
        am = GameObject.FindGameObjectWithTag("Player").GetComponent<ActorManager>();
        

        DAI = enemy.GetComponent<DummyIUserInputFix>();

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
        if(Input.GetKeyDown(KeyCode.B)|| Input.GetButtonDown("btn6"))
        {
            if (item.activeSelf)
            {
                item.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                item.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("btn7"))
        {
            if (item.activeSelf)
            {
                item.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
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
        }

        if (DAI.HPbarEn)
        {

            EnemyHPBar.enabled = true;
            EnemyHPFrame.enabled = true;
        }
        else
        {
            EnemyHPBar.enabled = false;
            EnemyHPFrame.enabled = false;
        }
        
        if (coolingDown == true)
        {
            levelnum.text = "lv. "+am.sm.playerLv.ToString() +"   EXP:"+am.sm.playerExp.ToString();
            currentHP = am.sm.HP;
            maxHP = am.sm.HPmax;
            HPBar.fillAmount = currentHP/maxHP;

            EnemyMaxHP = enemy.GetComponent<StateManager>().HPmax;
            EnemyCurrentHP = enemy.GetComponent<StateManager>().HP;

            EnemyHPBar.fillAmount = EnemyCurrentHP / EnemyMaxHP;
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
