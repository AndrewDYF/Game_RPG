using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public enum EnemyState
{
    idle,
    run,
    attack
}
public class MonsterAI : MonoBehaviour
{


    public EnemyState CurrentState= EnemyState.idle;

    protected Animation anim;
    protected Transform player;
    protected NavMeshAgent agent;

    public string str_run = "run";

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animation>();
        agent = GetComponentInChildren<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        switch (CurrentState)
        {
            case EnemyState.idle:
                
                if (distance > 1 && distance <= 15)
                {
                    CurrentState = EnemyState.run;
                }
                anim.Play("idle");
                agent.isStopped = true;
                break;
            case EnemyState.run:
                if (distance <= 3)
                {
                    CurrentState = EnemyState.attack;
                }
                if (distance > 15)
                {
                    CurrentState = EnemyState.idle;
                }
                
                
                anim.Play(str_run);
                
                

                agent.isStopped = false;
                agent.SetDestination(player.position);
                

                break;
            case EnemyState.attack:
                if (distance > 15)
                {
                    CurrentState = EnemyState.idle;
                }
                if (distance > 3)
                {
                    agent.isStopped = false;
                    CurrentState = EnemyState.run;
                }
                agent.isStopped = true;
                anim.Play("attack1");

                break;
        }
    }
}
