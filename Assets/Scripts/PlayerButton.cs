using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerButton
{
    // Start is called before the first frame update
    public bool IsPressing=false;//按压中
    public bool OnPressed=false;//按压
    public bool OnRepressed=false;//松开时
    public bool IsExtending = false;//松开后持续状态
    public bool IsDelaying = false;//按下后持续状态

    private bool curState = false;//目前状态
    private bool lastState = false;//前次状态

    private MyTimer extTimer = new MyTimer();
    private MyTimer delayTimer = new MyTimer();

    float extendingDuration = 0.2f;//松开后状态持续时间
    float delayingDuration = 0.15f;//按下后状态持续时间

    public void Tick(bool input)
    {
        
        extTimer.Tick();
        delayTimer.Tick();

        IsExtending = false;
        IsDelaying = false;

        curState = input;
        IsPressing = curState;

        OnPressed = false;
        OnRepressed = false;
        if (curState != lastState)//本次操作
        {
            if (curState == true)
            {
                OnPressed = true;
                StartTimer(delayTimer, delayingDuration);//按压开始计时
            }
            else
            {
                OnRepressed = true;
                StartTimer(extTimer, extendingDuration);//松开开始计时
            }
        }
        lastState = curState;

        if (extTimer.state == MyTimer.STATE.RUN)
            IsExtending = true;

        if (delayTimer.state == MyTimer.STATE.RUN)
            IsDelaying = true;

    }

    public void TickTrigger(float input)
    {
        extTimer.Tick();
        delayTimer.Tick();

        IsExtending = false;
        IsDelaying = false;

        if (input == 1)//将LT的float信号转成bool
        {
            curState = true;
        }
        else
        {
            curState = false;
        }

        //curState = input;
        IsPressing = curState;

        OnPressed = false;
        OnRepressed = false;
        if (curState != lastState)//本次操作
        {
            if (curState == true)
            {
                OnPressed = true;
                StartTimer(delayTimer, delayingDuration);//按压开始计时
            }
            else
            {
                OnRepressed = true;
                StartTimer(extTimer, extendingDuration);//松开开始计时
            }
        }
        lastState = curState;

        if (extTimer.state == MyTimer.STATE.RUN)
            IsExtending = true;

        if (delayTimer.state == MyTimer.STATE.RUN)
            IsDelaying = true;
    }

    private void StartTimer(MyTimer timer,float duration)
    {
        timer.duration = duration;
        timer.Go();
    }

}
