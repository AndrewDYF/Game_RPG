using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMotionControl : MonoBehaviour
{

    private Animator anim;
    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void OnAnimatorMove()
    {
        //Vector3 temp = anim.deltaPosition;//接收模型的攻击的移动量
        SendMessageUpwards("OnUpdateRM", (object)anim.deltaPosition);//对anim.deltaPosition进行装箱以支持sendMessage到playController
    }

}
