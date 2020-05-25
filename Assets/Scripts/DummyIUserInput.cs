using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum BossState
{
    idle,
    run,
    attack,
    defense
}




public class DummyIUserInput : IUserInput
{
    PlayController ac;

    public BossState CurrentState = BossState.idle;

    protected Transform player;
    protected NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponentInChildren<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;

        UpDown = 0f;
        LeftRight = 0f;
        //defense = true;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        aimPlayer();

        switch (CurrentState)
        {
            case BossState.idle:

                if (distance > 2 && distance <= 15)
                {
                    CurrentState = BossState.run;
                }
                agent.isStopped = true;
                defense = false;
                break;
            case BossState.run:
                if (distance <= 2)
                {
                    CurrentState = BossState.attack;
                }
                if (distance > 15)
                {
                    CurrentState = BossState.idle;
                }
                agent.isStopped = false;
                defense = false;
                UpDown = 1;
                agent.SetDestination(player.position);
                

                break;
            case BossState.attack:
                if (distance > 15)
                {
                    CurrentState = BossState.idle;
                }
                if (distance > 2)
                {
                    agent.isStopped = true;
                    CurrentState = BossState.run;
                }
                UpDown = 0;
                defense = false;
                Attack = true;
                agent.isStopped = true;
                lb = true;
                break;
            case BossState.defense:
                defense = true;
                if (distance <= 2)
                {
                    CurrentState = BossState.attack;
                }
                if (distance > 2 && distance <= 15)
                {
                    CurrentState = BossState.run;
                }
                if (distance > 15)
                {
                    CurrentState = BossState.idle;
                }
                break;
        }

        UpdateDmagDvec(UpDown,LeftRight);


    }

    void aimPlayer()
    {
        if (player.GetComponentInChildren<PlayController>().CheckStateTag("attackR"))
        {
            CurrentState = BossState.defense;
        }
        //Vector3 modelOrigin1 = ac.model.transform.position;//玩家坐标
        //Vector3 modelOrigin2 = modelOrigin1 + new Vector3(0, 1, 0);//玩家视线坐标
        //Vector3 boxCenter = modelOrigin2 + ac.model.transform.forward * 5.0f;
        //Collider[] cols = Physics.OverlapBox(boxCenter, new Vector3(0.5f, 0.5f, 5f), ac.model.transform.rotation, LayerMask.GetMask("Player"));
        //foreach (var col in cols)
        //{
        //    if (col.gameObject.transform.GetComponentInChildren<PlayController>().CheckStateTag("attackR"))
        //    {
        //        Debug.Log("ssss");
        //        CurrentState = BossState.defense;
        //    }
        //}
    }


}
