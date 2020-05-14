using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTimer
{
    public enum STATE//创建枚举
    {
        IDLE,
        RUN,
        FINISHED
    }

    public STATE state;

    public float duration = 1.0f;

    private float elapsedTime = 0;

    public void Tick()
    {
        switch (state)
        {
            case STATE.IDLE://初始状态
                break;
            case STATE.RUN:
                elapsedTime += Time.deltaTime;//开始计时
                if(elapsedTime>=duration)
                {
                    state = STATE.FINISHED;//超过时间，计时结束
                }
                break;
            case STATE.FINISHED://计时结束
                break;
            default:
                Debug.Log("error");
                    break;
        }
    }

    public void Go()
    {
        elapsedTime = 0;
        state = STATE.RUN;//开始计时
    }

}
