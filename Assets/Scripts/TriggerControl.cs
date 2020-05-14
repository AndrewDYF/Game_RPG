using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerControl : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void ResetTrigger(string triggerName)
    {
        anim.ResetTrigger(triggerName);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
