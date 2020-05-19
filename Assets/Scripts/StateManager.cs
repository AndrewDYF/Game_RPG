using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : IActorManagerInterface
{

    //public ActorManager am;

    public float playerLv = 1.0f;
    public float HP = 50.0f;
    public float HPmax = 50.0f;
    public float playerATK = 20f;

    [Header("======1st order state flags======")]
    public bool isGround;
    public bool isJump;
    public bool isJab;
    public bool isAttack;
    public bool isFall;
    public bool isRun;
    public bool isDefense;
    public bool isDie;
    public bool isBlocked;
    public bool isHit;
    public bool isRoll;

    [Header("======2st order state flags======")]
    public bool isAllowDefense;
    public bool isImmortal;



    public void Test()
    {
        print(HP);
    }

    public void UpdateHP(float value)
    {
        HP += value;
        Mathf.Clamp(HP, 0, HPmax);
        
    }

    // Start is called before the first frame update
    void Start()
    {
        HP = HPmax;//刷新HP值
    }

    // Update is called once per frame
    void Update()
    {
        isGround = am.ac.CheckState("ground");
        isJump = am.ac.CheckState("jump");
        isJab = am.ac.CheckState("jab");
        isAttack = am.ac.CheckStateTag("attackL") || am.ac.CheckStateTag("attackR");
        isFall = am.ac.CheckState("fall");
        isRoll = am.ac.CheckState("roll");
        isRun = am.ac.CheckState("run");
        //isDefense = am.ac.CheckState("defense1h", "defense");
        isDie = am.ac.CheckState("die");
        isBlocked = am.ac.CheckState("blocked");
        isHit = am.ac.CheckState("hit");
        
        isAllowDefense = isGround || isBlocked;
        isDefense = isAllowDefense && am.ac.CheckState("defense1h", "defense");
        isImmortal = isRoll || isJab;

    }

    public void PlayerLevelUp()
    {
        HPmax = 50 + playerLv * 10;
        HP = HPmax;
        playerATK += 3;

    }

}
