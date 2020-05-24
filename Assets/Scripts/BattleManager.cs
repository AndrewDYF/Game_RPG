using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class BattleManager : IActorManagerInterface
{

    private CapsuleCollider defCol;

    public float atknum;
    //public ActorManager am;

    // Start is called before the first frame update
    void Start()
    {
        defCol = GetComponent<CapsuleCollider>();
        //defCol.center = new Vector3(0,1,0);
        defCol.center = Vector3.up * 1.0f;
        defCol.height = 2.0f;
        defCol.radius = 0.25f;
        defCol.isTrigger = true;
    }
    //
    //// Update is called once per frame
    //void Update()
    //{
    //    
    //}

    void OnTriggerEnter(Collider col)
    {
        //print(col.name);
        if (col.tag == "Weapon")
        {
            atknum= col.gameObject.GetComponent<WeaponData>().ATK;
            am.TryDoDamage();
        }
    }



}
