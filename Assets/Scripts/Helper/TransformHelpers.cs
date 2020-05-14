using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformHelpers 
{
    public static void sd(this Transform trans)
    {
        Debug.Log(trans.name);
    }

    public static Transform DeepFind(this Transform parent,string targetName)//将函数下挂到transform中
    {
        Transform temp =null;
        foreach (Transform child in parent)
        {
            
            if(child.name == targetName)
            {
                return child;
            }
            else
            {
                temp = DeepFind(child, targetName);
                if (temp != null)
                {
                    return temp;
                }
            }
             
        }
        return null;
    }

}
