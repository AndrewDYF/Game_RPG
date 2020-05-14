using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyIUserInput : IUserInput
{
    // Start is called before the first frame update
    void Start()
    {
        UpDown = 0f;
        LeftRight = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDmagDvec(UpDown,LeftRight);
    }



}
