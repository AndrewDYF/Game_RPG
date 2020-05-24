using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum BossState
{
    idle,
    run,
    attack
}



public class DummyIUserInput : IUserInput
{
    public BossState CurrentState = BossState.idle;
    protected Animation anim;
    protected Transform player;
    protected NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animation>();
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

        switch (CurrentState)
        {
            case BossState.idle:

                if (distance > 1 && distance <= 15)
                {
                    CurrentState = BossState.run;
                }
                agent.isStopped = true;
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
                Attack = true;
                agent.isStopped = true;
                lb = true;
                break;
        }

        UpdateDmagDvec(UpDown,LeftRight);


    }



}
