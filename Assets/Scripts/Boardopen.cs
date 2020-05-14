using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boardopen : MonoBehaviour
{

    
    public dooropen icon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    private void OnTriggerStay(Collider other)
    {
        
        icon.SendMessage("dop");
        
    }

}
