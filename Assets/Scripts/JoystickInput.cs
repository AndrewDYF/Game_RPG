using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickInput : IUserInput
{
    // Start is called before the first frame update
    [Header("=====Joystick Setting=====")]
    public string axisX = "axisX";
    public string axisY = "axisY";
    public string axisJright = "axis4";
    public string axisJup = "axis5";
    public string btnA = "btn0";
    public string btnB = "btn1";
    public string btnX = "btn2";
    public string btnY = "btn3";
    public string btnS = "btn7";

    public string btnLB = "btn4";
    public string btnLT = "axis9";
    public string btnRB = "btn5";
    public string btnRT = "axis10";
    public string btnJstick = "btn9";

    public PlayerButton buttonA = new PlayerButton();
    public PlayerButton buttonX = new PlayerButton();
    public PlayerButton buttonY = new PlayerButton();
    public PlayerButton buttonB = new PlayerButton();
    public PlayerButton buttonLB = new PlayerButton();
    public PlayerButton buttonLT = new PlayerButton();
    public PlayerButton buttonRB = new PlayerButton();
    public PlayerButton buttonRT = new PlayerButton();
    public PlayerButton buttonJstick = new PlayerButton();
    public PlayerButton buttonS = new PlayerButton();









    //public PlayerButton pb=new PlayerButton();
    //[Header("===Output signals===")]
    //public float UpDown;
    //public float LeftRight;
    //public float Dmag;
    //public Vector3 Dvec;
    //public float JUp;
    //public float JRight;

    //public bool run;
    //public bool jump;
    //private bool lastJump;
    //public bool Attack;
    //private bool lastAttack;

    //[Header("===Others===")]
    //public bool inputenble = true;

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
        buttonA.Tick(Input.GetButton(btnA));
        buttonX.Tick(Input.GetButton(btnX));
        buttonY.Tick(Input.GetButton(btnY));
        buttonB.Tick(Input.GetButton(btnB));
        buttonLB.Tick(Input.GetButton(btnLB));
        buttonLT.TickTrigger(Input.GetAxis(btnLT));
        buttonRB.Tick(Input.GetButton(btnRB));
        buttonRT.TickTrigger(Input.GetAxis(btnRT));
        buttonJstick.Tick(Input.GetButton(btnJstick));
        buttonS.Tick(Input.GetButton(btnS));






        JUp = Input.GetAxis(axisJup);
        JRight = -1*Input.GetAxis(axisJright)*1.3f;

        targetUpDown = Input.GetAxis(axisY);//接收控制方向
        targetLeftRight = Input.GetAxis(axisX);

        if (inputenble == false)
        {
            targetLeftRight = 0;
            targetUpDown = 0;
        }

        UpDown = Mathf.SmoothDamp(UpDown, targetUpDown, ref velocityUpDown, 0.2f);//数值增加到移动方向
        LeftRight = Mathf.SmoothDamp(LeftRight, targetLeftRight, ref velocityLeftRight, 0.2f);

        Vector2 tempDAxis = SquareToCircle(new Vector2(LeftRight, UpDown));

        float UpDown2 = tempDAxis.y;//套用新的x,y速度;
        float LeftRight2 = tempDAxis.x;

        Dmag = Mathf.Sqrt(UpDown2 * UpDown2 + LeftRight2 * LeftRight2);
        Dvec = UpDown2 * transform.forward + LeftRight2 * transform.right;

        roll = buttonA.OnRepressed && buttonA.IsDelaying;
        run = ((buttonA.IsPressing && !buttonA.IsDelaying) || buttonA.IsExtending)&&!roll;
        defense = buttonLT.IsPressing;
        
        lb = buttonLB.OnPressed;
        lt = buttonLT.OnPressed;
        rb = buttonRB.OnPressed;
        rt = buttonRT.OnPressed;
        
        mids= buttonS.OnPressed;

        //Attack = buttonX.OnPressed;
        //Attack = buttonLT.OnPressed;
        jump = buttonA.OnPressed && buttonA.IsExtending;
        lockon = buttonJstick.OnPressed;
        

        //run = buttonA.IsPressing;
        //defense = buttonLB.IsPressing;
        //Attack = buttonX.OnPressed;
        //jump = buttonB.OnPressed;

        //run = Input.GetButton(btnA);
        //defense = Input.GetButton(btnLB);

        //bool newJump = Input.GetButton(btnX);//接收跳跃信息
        //if (newJump != lastJump && newJump == true)//判断是否是此次控制键按下；
        //{
        //    jump = true;
        //}
        //else if (newJump == lastJump)//不是此次控制键按下的情况
        //{
        //    jump = false;
        //}
        //
        //lastJump = newJump;//更新记录跳跃按键情况的值



        //bool newAttack = Input.GetButton(btnX);//接收攻击信息
        //if (newAttack != lastAttack && newAttack == true)//判断是否是此次控制键按下；
        //{
        //    Attack = true;
        //}
        //else if (newAttack == lastAttack)//不是此次控制键按下的情况
        //{
        //    Attack = false;
        //}
        //
        //lastAttack = newAttack;


    }

    //private Vector2 SquareToCircle(Vector2 input)//把方形的速度坐标转为圆形
    //{
    //    Vector2 output = Vector2.zero;
    //    output.x = input.x * Mathf.Sqrt(1 - (input.y * input.y) / 2);
    //    output.y = input.y * Mathf.Sqrt(1 - (input.x * input.x) / 2);
    //    return output;
    //}

}
