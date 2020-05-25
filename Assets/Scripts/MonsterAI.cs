using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public enum EnemyState
{
    idle,
    run,
    attack,
    hit,
    die
}
public class MonsterAI : MonoBehaviour
{


    public EnemyState CurrentState= EnemyState.idle;

    protected Animation anim;
    protected Transform player;
    protected NavMeshAgent agent;

    public GameObject wh;
    private Collider whco;

    float atknum = 1;
    float monHP = 50;
    float monHPmax = 50;

    bool isHit = false;
    bool isDie = false;
    bool canatk=true;
    float stopTime = 0;
    float atkTime = 0;
    float distance = 0;

    public string str_run = "run";

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponentInChildren<Animation>();
        agent = GetComponentInChildren<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;


        wh = transform.DeepFind("weaponHandle").gameObject;
        whco = wh.GetComponent<CapsuleCollider>();

    }

    // Update is called once per frame
    void Update()
    {
        
        distance = Vector3.Distance(player.position, transform.position);
        switch (CurrentState)
        {
            case EnemyState.idle:
                
                if (distance > 2 && distance <= 15)
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

                if (canatk)
                {
                    whco.enabled = true;
                    stopTime = 0;
                    anim.Play("attack1");
                    atkTime++;

                }
                if (!canatk)
                {
                    whco.enabled = false;
                    atkTime = 0;
                    anim.Play("idle");
                    stopTime++;
                }
                if (stopTime >= 120)
                {
                    canatk = true;
                }
                if (atkTime >= 50)
                {
                    whco.enabled = false;
                    canatk = false;
                }
                break;

                case EnemyState.hit:

                if (!isHit)
                {
                    anim.Play("hit1");
                    stopTime = 0;
                    isHit = true;
                }
                if (isHit)
                {
                    stopTime++;
                }
                if (stopTime > 60)
                {
                    isHit = false;
                }
                
                break;

            case EnemyState.die:

                
                if (!isDie)
                {
                    anim.Play("die");
                    stopTime = 0;
                    isHit = true;
                }
                if (isDie)
                {
                    stopTime++;
                    if (stopTime > 60)
                {
                        whco.enabled = false;
                }
                }
                
                break;
        }
    }

    IEnumerator waittime()
    {
        yield return new WaitForSeconds(1.5f);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Weapon")
        {

            atknum = col.gameObject.GetComponent<WeaponData>().ATK+player.GetComponent<StateManager>().playerATK;
            monHP -= atknum;
            if (monHP <= 0)
            {
                CurrentState = EnemyState.die;
                player.GetComponent<StateManager>().playerExp += 20;
                if(player.GetComponent<StateManager>().playerExp>= player.GetComponent<StateManager>().playerExpMax)
                {
                    player.GetComponent<StateManager>().playerExpMax += 20;
                    player.GetComponent<StateManager>().PlayerLevelUp();
                }
            }
            else
            CurrentState = EnemyState.hit;
        }
    }

    void changeState()
    {
        if (distance > 15)
        {
            CurrentState = EnemyState.idle;
        }
        if (distance > 2 && distance <= 15)
        {
            CurrentState = EnemyState.run;
        }
        if (distance > 3)
        {
            agent.isStopped = false;
            CurrentState = EnemyState.run;
        }
    }


}
