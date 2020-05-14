using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftArmAminFix : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 leftArmEulerAngles;
    public PlayController ac;

    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        ac = GetComponentInParent<PlayController>();
    }

    

    private void OnAnimatorIK()
    {
        if (ac.leftIsShield)
        {
            if (anim.GetBool("defense") == false)
            {
                Transform leftLowerArm = anim.GetBoneTransform(HumanBodyBones.LeftLowerArm);
                leftLowerArm.localEulerAngles += leftArmEulerAngles;
                anim.SetBoneLocalRotation(HumanBodyBones.LeftLowerArm, Quaternion.Euler(leftLowerArm.localEulerAngles));//将改变的角度EulerAngles代回左臂
            }
        }
        
    }
}


