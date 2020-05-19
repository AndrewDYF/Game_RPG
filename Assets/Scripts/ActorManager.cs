using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoBehaviour
{

    public BattleManager bm;
    public WeaponManager wm;
    public StateManager sm;
    public PlayController ac;
    float damageValue = -10;

    // Start is called before the first frame update
    void Awake()
    {
        ac = GetComponent<PlayController>();

        GameObject sensor = transform.Find("sensor").gameObject;
        GameObject model = ac.model;
        


        bm = Bind<BattleManager>(sensor);
        wm = Bind<WeaponManager>(model);
        sm = Bind<StateManager>(gameObject);

        //bm = sensor.GetComponent<BattleManager>();
        //if (bm == null)
        //{
        //    bm=sensor.AddComponent<BattleManager>();//增加BattleManager
        //}
        //bm.am = this;//使BattleManager关联ActorManager


        //wm = model.GetComponent<WeaponManager>();
        //if (wm == null)
        //{
        //    wm = model.AddComponent<WeaponManager>();//增加
        //}
        //wm.am = this;


        //sm = GetComponent<StateManager>();
        //if (sm == null)
        //{
        //    sm = gameObject.AddComponent<StateManager>();
        //}
        //sm.am = this;
        sm.Test();

    }

    private T Bind<T>(GameObject go) where T : IActorManagerInterface //通过泛型绑定
    {
        T tempInstance;
        tempInstance = go.GetComponent<T>();
        if (tempInstance == null)
        {
            tempInstance = go.AddComponent<T>();
        }
        tempInstance.am = this;
        return tempInstance;
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void TryDoDamage()
    {
        damageValue = -sm.playerATK;
        if (sm.isImmortal) { 
        }
        else if (sm.isDefense == true)
        {
            blocked();
        }
        else
        {
            if (sm.HP > 0)
            {
                
                sm.UpdateHP(damageValue);
                if (sm.HP > 0)
                {
                    Hit();
                }
                else
                {
                    Die();
                }
            }

        }
        
        
        

    }

    public void blocked()
    {
        ac.IssueTrigger("blocked");
    }


    public void Hit()
    {
        ac.IssueTrigger("hit");
    }

    public void Die()
    {
        ac.IssueTrigger("die");
        ac.pi.enabled = false;
        if (ac.camcon.lockState == true)
        {
            ac.camcon.LockUnlock();
        }
        ac.camcon.enabled = false;
    }

}
