using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    // Start is called before the first frame update
    ActorManager am;

    public float maxHP=100f;
    public float currentHP=80f;

    public Image HPBar;
    public Image HPBarBack;
    
    public bool coolingDown = true;

    public bool pause;







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
        currentHP = am.sm.HP;
        maxHP = am.sm.HPmax;
        if (coolingDown == true)
        {
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
}
