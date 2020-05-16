using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IUserInput : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("===Output signals===")]
    public float UpDown;
    public float LeftRight;
    public float Dmag;
    public Vector3 Dvec;
    public float JUp;
    public float JRight;


    public bool run;
    public bool defense;
    public bool roll;

    public bool jump;
    protected bool lastJump;
    public bool Attack;
    protected bool lastAttack;
    public bool lockon;
    public bool lb;
    public bool lt;
    public bool rb;
    public bool rt;
    public bool mids;

    [Header("===Others===")]
    public bool inputenble = true;

    protected float targetUpDown;
    protected float targetLeftRight;
    protected float velocityLeftRight;
    protected float velocityUpDown;

    protected Vector2 SquareToCircle(Vector2 input)//把方形的速度坐标转为圆形
    {
        Vector2 output = Vector2.zero;
        output.x = input.x * Mathf.Sqrt(1 - (input.y * input.y) / 2);
        output.y = input.y * Mathf.Sqrt(1 - (input.x * input.x) / 2);
        return output;
    }

    protected void UpdateDmagDvec(float UpDown2,float LeftRight2)
    {
        Dmag = Mathf.Sqrt(UpDown2 * UpDown2 + LeftRight2 * LeftRight2);
        Dvec = UpDown2 * transform.forward + LeftRight2 * transform.right;
    }
}
