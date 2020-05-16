using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : IUserInput
{
    // Start is called before the first frame update

        [Header("===Key setting===")]
    public string KeyUp="w";
    public string KeyDown="s";
    public string KeyLeft="a";
    public string KeyRight="d";

    public string KeyA = "left shift";
    public string KeyB = "space";
    public string KeyC= "mouse 0";
    public string KeyD= "mouse 1";
    public string KeyE= "escape";
    public string KeyJUp;
    public string KeyJRight;
    public string KeyJLeft;
    public string KeyJDown;

    public PlayerButton buttonA = new PlayerButton();
    public PlayerButton buttonB = new PlayerButton();
    public PlayerButton buttonC = new PlayerButton();
    public PlayerButton buttonD = new PlayerButton();
    public PlayerButton buttonE = new PlayerButton();
    public PlayerButton buttonF = new PlayerButton();

    [Header("=====Mouse setting=====")]
    public bool mouseEnabled = false;
    public float mouseSensitivityX = 1.0f;//鼠标倍速
    public float mouseSensitivityY = 1.0f;

    //[Header("===Output signals===")] 
    //public float UpDown;
    //public float LeftRight;
    //public float Dmag;
    //public Vector3 Dvec;
    //public float JUp;
    //public float JRight;
    //public float JLeft;
    //public float JDown;
    //
    //public bool run;
    //public bool jump;
    //private bool lastJump;
    //public bool Attack;
    //private bool lastAttack;
    //
    //[Header("===Others===")]
    //public bool inputenble = true;
    //
    //private float targetUpDown;
    //private float targetLeftRight;
    //private float velocityLeftRight;
    //private float velocityUpDown;   

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        buttonA.Tick(Input.GetKey(KeyA));
        buttonB.Tick(Input.GetKey(KeyB));
        buttonC.Tick(Input.GetKey(KeyC));
        buttonD.Tick(Input.GetKey(KeyD));
        buttonE.Tick(Input.GetKey(KeyE));

        //鼠标控制
        JUp = Input.GetAxis("Mouse Y")* 2 * mouseSensitivityY;
        JRight = Input.GetAxis("Mouse X") * 3 * mouseSensitivityX;

        //JUp = (Input.GetKey(KeyJUp) ? 1.0f : 0) - (Input.GetKey(KeyJDown) ? 1.0f : 0);
        //JRight = (Input.GetKey(KeyJRight) ? 1.0f : 0) - (Input.GetKey(KeyJLeft) ? 1.0f : 0);


        targetUpDown = (Input.GetKey(KeyUp) ? 1.0f : 0) - (Input.GetKey(KeyDown) ? 1.0f : 0);//接收控制方向
        targetLeftRight = (Input.GetKey(KeyRight) ? 1.0f : 0) - (Input.GetKey(KeyLeft) ? 1.0f : 0);

        if(inputenble==false)
        {
            targetLeftRight = 0;
            targetUpDown = 0;
        }

        UpDown = Mathf.SmoothDamp(UpDown,targetUpDown,ref velocityUpDown,0.2f);//数值增加到移动方向
        LeftRight = Mathf.SmoothDamp(LeftRight, targetLeftRight, ref velocityLeftRight, 0.2f);

        

        Vector2 tempDAxis = SquareToCircle(new Vector2(LeftRight,UpDown));

        float UpDown2 = tempDAxis.y;//套用新的x,y速度;
        float LeftRight2 = tempDAxis.x;

        Dmag = Mathf.Sqrt(UpDown2 * UpDown2 + LeftRight2 * LeftRight2);
        Dvec= UpDown2 * transform.forward + LeftRight2 * transform.right;

        run = buttonA.IsPressing;
        jump = buttonB.OnPressed && run;
        defense = buttonD.IsPressing;
        Attack = buttonC.OnPressed;
        rb= buttonC.OnPressed;

        roll = buttonB.OnPressed;

        mids = buttonE.OnPressed;



        //run = Input.GetKey(KeyA);//按住KeyA切换跑步
        //defense = Input.GetKey(KeyD);

        //bool newJump = Input.GetKey(KeyB);//接收跳跃信息
        //if(newJump!=lastJump && newJump==true)//判断是否是此次控制键按下；
        //{
        //    jump = true;
        //}
        //else if(newJump==lastJump)//不是此次控制键按下的情况
        //{
        //    jump = false;
        //}
        //
        //lastJump = newJump;//更新记录跳跃按键情况的值
        //
        //bool newAttack = Input.GetKey(KeyC);//接收攻击信息
        //if (newAttack != lastAttack && newAttack == true)//判断是否是此次控制键按下；
        //{
        //    Attack = true;
        //}
        //else if (newJump == lastJump)//不是此次控制键按下的情况
        //{
        //    Attack = false;
        //}
        //
        //lastAttack = newAttack;//更新记录攻击按键情况的值

    }

    //private Vector2 SquareToCircle(Vector2 input)//把方形的速度坐标转为圆形
    //{
    //    Vector2 output = Vector2.zero;
    //    output.x = input.x * Mathf.Sqrt(1 - (input.y*input.y) / 2);
    //    output.y = input.y * Mathf.Sqrt(1 - (input.x * input.x) / 2);
    //    return output;
    //}

}
