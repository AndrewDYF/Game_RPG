using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : IActorManagerInterface
{
    // Start is called before the first frame update
    
    
    
    
    
    //// Update is called once per frame
    //void Update()
    //{
    //    
    //}
    private Collider weaponColL;
    private Collider weaponColR;
    //public ActorManager am;
    public GameObject whL;
    public GameObject whR;

    void Start()
    {
        try
        {
            whL = transform.DeepFind("weaponHandleL").gameObject;
            whR = transform.DeepFind("weaponHandleR").gameObject;

            weaponColL = whL.GetComponentInChildren<Collider>();
            weaponColR = whR.GetComponentInChildren<Collider>();
        }
        catch
        {
            print("can not find WeaponHandle");
        }
        //weaponCol = whR.GetComponentInChildren<Collider>();
        //print(transform.DeepFind("weaponHandleR"));
    }

    public void WeaponEnable()
    {
        if (am.ac.CheckStateTag("attackL"))
        {
            weaponColL.enabled = true;
            //print("Lenable");
        }
        else
        {
            weaponColR.enabled = true;
            //print("Renable");
        }
        
        
        
    }
    public void WeaponDisable()
    {
        weaponColR.enabled = false;
        weaponColL.enabled = false;
        //print("disable");
    }
}
