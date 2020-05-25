using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoNextLevel : MonoBehaviour
{
    // Start is called before the first frame update

    //string KeyE = 
    string btnB = "btn1";
    bool gokey = false;
    bool gojoy = false;
    bool go = false;
    public Text talk;
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gokey = Input.GetKey(KeyCode.E);
        gojoy = Input.GetButtonDown(btnB);
        if ((gojoy || gokey) && (go))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);//进入下一个场景
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            go = true;
            talk.text = "Press E or START to Go to next level";
            talk.enabled = true;
            if (gojoy|| gokey)
            {
                
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);//进入下一个场景
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            go = false;
            talk.enabled = false;

        }
    }
}
