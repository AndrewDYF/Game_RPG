using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGroundSenser : MonoBehaviour
{
    // Start is called before the first frame update

    public CapsuleCollider capaol;
    public float offset = 0.1f;//用于使物体碰撞体积偏下以修正着陆判定偏慢的值

    private Vector3 point1;
    private Vector3 point2;
    private float radius;

    void Awake()
    {
        radius = capaol.radius-0.05f;//用于使物体碰撞体积偏窄的值
        

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        point1 = transform.position + transform.up * (radius-offset);
        point2 = transform.position + transform.up * (capaol.height-offset) - transform.up * radius;
        Collider[] outputCols = Physics.OverlapCapsule(point1, point2, radius,LayerMask.GetMask("Ground"));//检测有无碰撞地板
        if (outputCols.Length!=0)
        {
            SendMessageUpwards("IsGround");
        }
        else
        {
            SendMessageUpwards("IsNotGround");
        }
    }
}
