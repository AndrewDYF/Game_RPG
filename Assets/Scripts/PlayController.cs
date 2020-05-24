using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayController : MonoBehaviour
{
    // Start is called before the first frame update


    public GameObject pausemenu;
    public GameObject helppage;

    public GameObject model;
    public CameraController camcon;
    public IUserInput pi;
    public float walkSpeed = 1.8f;//走路速度系数
    public float runMultiplier = 3.0f;//跑步速度系数
    public float jumpVelocity = 4.0f;//跳跃高度
    public float rollVelocity = 3.0f;//翻滚速度系数
    public float jabVelocity = 1.0f;
    public float jabMultiplier = 1.5f;

    [Space(10)]
    [Header("=====Friction Setting=====")]
    public PhysicMaterial fricitonOne;
    public PhysicMaterial frictionZero;

    [SerializeField]
    private StateManager sm;
    private Animator anim;  
    private Rigidbody rigid;
    private Vector3 planarVec;
    private Vector3 thrustVec;//玩家累积移动的量
    private bool lockPlanar = false;//是否锁定速度向量
    private bool trackDiretion = false;//是否在执行动作时锁定人物方向
    private bool canAttack;//人物处于可开启攻击状态
    private CapsuleCollider col;//摩擦力选择
    //private float lerpTarget;//记录要lerp的目标数，权重值将会向该数值靠近
    private Vector3 deltaPos;//声明一个记录模型移动情况的Vector3

    public bool leftIsShield = true;

    void Awake()
    {
        IUserInput[] inputs = GetComponents<IUserInput>();
        foreach(var input in inputs)
        {
            if (input.enabled == true)
            {
                pi = input;
                break;               
            }
                
            
        }

        anim = model.GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();//指针向玩家的物理碰撞模型
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float targetrunMulti = (pi.run) ? 2.0f : 1.0f;//跑步
        
        //anim.SetBool("defense", pi.defense);

        if (pi.lockon)
        {
            camcon.LockUnlock();
        }

        if (camcon.lockState == false)
        {
            anim.SetFloat("forward", pi.Dmag * Mathf.Lerp(anim.GetFloat("forward"), targetrunMulti, 0.5f));
            anim.SetFloat("right", 0);
        }
        else
        {
            Vector3 localDVec = transform.InverseTransformVector(pi.Dvec);//将世界向量转为local向量
            anim.SetFloat("forward", localDVec.z* targetrunMulti);
            anim.SetFloat("right", localDVec.x * targetrunMulti);
        }

        if(pi.roll || rigid.velocity.magnitude > 7.0f)
        {
            anim.SetTrigger("roll");
            canAttack = false;
        }

        //if (pi.mids)//暂停
        //{
        //    if (pausemenu.activeSelf)//如果已经暂停
        //    {
        //        pausemenu.SetActive(false);
        //        Cursor.lockState = CursorLockMode.Locked;
        //    }
        //    else
        //    {
        //        pausemenu.SetActive(true);
        //        helppage.SetActive(false);
        //        Cursor.lockState = CursorLockMode.None;
        //
        //    }
        //}

        //if(rigid.velocity.magnitude>1.0f)
        //{
        //    anim.SetTrigger("roll");
        //
        //}

        if((pi.Attack || pi.lb || pi.rb) && (CheckState("ground")|| CheckStateTag("attackR") || CheckStateTag("attackL")) && anim.GetBool("isGround") && canAttack)//是否接收到攻击信号，同时人物是否在地面且处于站立状态,并且允许攻击
        {
            if (pi.rb)
            {
                anim.SetBool("mirror", false);
                anim.SetTrigger("attack");//触发attack的Trigger
            }
            if (pi.lb && !leftIsShield)
            {
                anim.SetBool("mirror", true);
                anim.SetTrigger("attack");//触发attack的Trigger
            }

            
        }

        

        if (pi.jump == true)
        {
            anim.SetTrigger("jump");//出发jump信号
            canAttack = false;
        }

        if (camcon.lockState == false)
        {
            if (pi.Dmag > 0.1f)
            {
                Vector3 targetforward = Vector3.Slerp(model.transform.forward, pi.Dvec, 0.4f);
                model.transform.forward = targetforward;
            }

            if (lockPlanar == false)
            {
                planarVec = pi.Dmag * model.transform.forward * walkSpeed * ((pi.run) ? runMultiplier : 1.0f);//速度向量会更新
            }
        }

        else
        {
            if (trackDiretion == false)
            {
                model.transform.forward = transform.forward;//锁定前方
            }
            else
            {
                model.transform.forward = planarVec.normalized;//可被调变
            }
            
            if (lockPlanar == false)
            {
                planarVec = pi.Dvec * walkSpeed * ((pi.run) ? runMultiplier : 1.0f);
            }
                
        }

        if (leftIsShield == true)
        {
            
            if (CheckState("ground")||CheckState("blocked"))
            {
                anim.SetLayerWeight(anim.GetLayerIndex("defense"), 1);
                anim.SetBool("defense", pi.defense);
            }
            else
            {
                anim.SetBool("defense", false); ;//退出防御状态
            }
        }
        else
        {
            anim.SetLayerWeight(anim.GetLayerIndex("defense"), 0);
        }

        //if (CheckState("ground") && leftIsShield==true)
        //{
        //    anim.SetBool("defense", pi.defense);
        //    if (pi.defense)
        //        anim.SetLayerWeight(anim.GetLayerIndex("defense"), 1);
        //    else
        //        anim.SetLayerWeight(anim.GetLayerIndex("defense"), 0);
        //}




    }

    void FixedUpdate()
    {
        //rigid.position += planarVec * Time.fixedDeltaTime;
        rigid.position += deltaPos;
        rigid.velocity = new Vector3(planarVec.x, rigid.velocity.y, planarVec.z) + thrustVec;
        thrustVec = Vector3.zero;//清零
        deltaPos = Vector3.zero; //清零
    }

    public bool CheckState(string stateName,string layerName="Base Layer")//接收stateName来进行判断
    {
        int layerIndex = anim.GetLayerIndex(layerName);//取得layerName的索引值layerIndex
        bool result = anim.GetCurrentAnimatorStateInfo(layerIndex).IsName(stateName);//以layerIndex为索引值取得它现在状况，是否为layerName  
        return result;
    }

    public bool CheckStateTag(string tagName, string layerName = "Base Layer")//接收stateName来进行判断
    {
        int layerIndex = anim.GetLayerIndex(layerName);//取得layerName的索引值layerIndex
        bool result = anim.GetCurrentAnimatorStateInfo(layerIndex).IsTag(tagName);//以layerIndex为索引值取得它现在状况，是否为layerName  
        return result;
    }

    ///
    ///Message processing block
    ///

    public void OnJumpEnter()
    {
        
        thrustVec = new Vector3(0, jumpVelocity, 0);//给予y方向的速度
        pi.inputenble = false;
        lockPlanar = true;
        trackDiretion = true;
    }

    //public void OnJumpExit()
    //{
    //    pi.inputenble = true;
    //    lockPlanar = false;
    //}

    public void IsGround()//判断在地面
    {
        anim.SetBool("isGround", true);
    }

    public void IsNotGround()//判定不在地面
    {
        anim.SetBool("isGround", false);
    }

    public void OnGroundEnter()//判定是否着陆
    {
        pi.inputenble = true;
        lockPlanar = false;
        canAttack = true;
        col.material = fricitonOne;//使摩擦力为fricitonOne
        trackDiretion = false;
    }

    public void OnGroundExit()
    {
        col.material=frictionZero;//使摩擦力为fricitonZero
    }

    public void OnFallEnter()
    {
        pi.inputenble = false;
        lockPlanar = true;
    }

    public void OnAttack1hAEnter()
    {
        pi.inputenble = false;
        //lerpTarget = 1.0f;
    }

    public void OnAttack1hAUpdate()
    {
        thrustVec = model.transform.forward * anim.GetFloat("attack1hAVelocity");//使物体获得攻击方向的一个向量速度
        //float currentWeight = anim.GetLayerWeight(anim.GetLayerIndex("attack"));//获得Attack的当前权重值
        //currentWeight = Mathf.Lerp(currentWeight, lerpTarget, 0.4f);//使currentWeight的值以每次40%的速度向lerpTarget靠拢
        //anim.SetLayerWeight(anim.GetLayerIndex("attack"), currentWeight);//使Attack的权重趋向currentWeight
    }

    public void OnAttackExit()
    {
        model.SendMessage("WeaponDisable");
    }

    //public void OnAttackIdleEnter()
    //{
    //    pi.inputenble = true;
    //    //anim.SetLayerWeight(anim.GetLayerIndex("Attack"), 0);//使Attack的Layer的权重为0
    //   lerpTarget = 0f;//更新权重目标值为0
    //}

    //public void OnAttackIdleUpdate()
    //{
    //    float currentWeight = anim.GetLayerWeight(anim.GetLayerIndex("attack"));//获得Attack的当前权重值
    //    currentWeight = Mathf.Lerp(currentWeight, lerpTarget, 0.1f);//使currentWeight的值以每次10%的速度向lerpTarget靠拢
    //    anim.SetLayerWeight(anim.GetLayerIndex("attack"), currentWeight);//使Attack的权重等于currentWeight
    //}

    public void OnRollEnter()
    {
        thrustVec = new Vector3(0, rollVelocity, 0);
        pi.inputenble = false;
        lockPlanar = true;
        trackDiretion = true;
        
        //thrustVec = model.transform.forward * anim.GetFloat("jabVelocity")*2;
    }

    public void OnRollUpdate()
    {
        thrustVec = model.transform.forward * anim.GetFloat("rollVelocityxz") * 3.0f;
    }

    public void OnJabEnter()
    {
        pi.inputenble = false;
        lockPlanar = true;
        //thrustVec = -model.transform.forward*jabVelocity;
    }

    public void OnJabUpdate()
    {
        thrustVec = model.transform.forward * anim.GetFloat("jabVelocity")* jabMultiplier;//给模型一个jab的速度
    }

    public void OnUpdateRM(object _deltaPos)//接收一个object类型的区域变量_deltaPos
    {
        if (CheckState("attack1hC"))//检测是否在attack1hC中
        {
            deltaPos += (Vector3)_deltaPos;//累加接收到的移动量
        }
        
    }

    public void OnHitEnter()
    {
        pi.inputenble = false;
        planarVec = new Vector3(0, 0, -0.5f);
        
    }

    public void OnBlockedEnter()
    {
        pi.inputenble = false;
        planarVec = Vector3.zero;
    }

    public void OnDieEnter()
    {
        pi.inputenble = false;
        planarVec = Vector3.zero;
    }

    public void IssueTrigger(string triggerName)
    {
        anim.SetTrigger(triggerName);
    }


}
