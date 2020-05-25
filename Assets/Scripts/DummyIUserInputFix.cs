using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyIUserInputFix : IUserInput
{
    // Start is called before the first frame update


    
    PlayController ac;

    public bool HPbarEn=false;

    private Transform player;

    float distance = 0;
    void Awake()
    {

        ac = GetComponent<PlayController>();
        player= GameObject.FindWithTag("Player").transform;
        UpDown = 0f;
        LeftRight = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(player.position, transform.position);
        if (distance >= 15)
        {
            UpDown = 0;
            HPbarEn = false;
            if (ac.camcon.lockState)
            {
                lockon = true;
            }
        }

        if(distance<=10 && !ac.camcon.lockState)
        {
            lockon=true;
        }
        if (distance <= 10 && distance >= 2 && ac.camcon.lockState) 
        {
            HPbarEn = true;
            UpDown = 1;
        }
        if (distance < 2)
        {
            UpDown = 0;
            lb = true;
        }


        UpdateDmagDvec(UpDown, LeftRight);
    }

}
