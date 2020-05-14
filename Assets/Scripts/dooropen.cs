using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dooropen : MonoBehaviour
{
    
    private float xmax=4;
    private float xsrt;
    // Start is called before the first frame update
    void Start()
    {
        xsrt = transform.position.x;
}

    // Update is called once per frame
    void Update()
    {
        
            

    }
    public void dop()
    {
        
        if (transform.position.x < xsrt + 4.75f)
        {
            transform.position = new Vector3(transform.position.x + 0.1f, transform.position.y, transform.position.z);

        }
    }


}
