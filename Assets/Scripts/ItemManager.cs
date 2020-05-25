using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{

    public GameObject player;
    public GameObject weapon1;
    public GameObject weapon2;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        weapon1 = player.transform.DeepFind("weapon1").gameObject;
        weapon2 = player.transform.DeepFind("weapon2").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChooseWeapon1()
    {
        weapon1.SetActive(true);
        weapon2.SetActive(false);
    }
    public void ChooseWeapon2()
    {
        weapon1.SetActive(false);
        weapon2.SetActive(true);
    }
}
