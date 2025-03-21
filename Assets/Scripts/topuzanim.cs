using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class topuzanim : MonoBehaviour
{
    Animator anim;
    string TRIGGER_FORWARD = "forward";
    string TRIGGER_BACK = "back";
    string TRIGGER_LEFT = "left";
    string TRIGGER_RIGHT = "right";
    string TRIGGER_IDLE = "idle";
    string TRIGGER_SAGCAPRAZ = "solcapraz";
    string TRIGGER_SOLCAPRAZ = "sagcapraz";

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
      
        if (Input.GetKey(KeyCode.W))
        {
            anim.SetTrigger(TRIGGER_FORWARD);
            anim.ResetTrigger(TRIGGER_IDLE);
            if (Input.GetKey(KeyCode.A))
            {
                anim.SetTrigger(TRIGGER_SOLCAPRAZ);
                anim.ResetTrigger(TRIGGER_FORWARD);
            }
            if (Input.GetKey(KeyCode.D))
            { 
                anim.SetTrigger(TRIGGER_SAGCAPRAZ);
                anim.ResetTrigger(TRIGGER_FORWARD);
            }
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            anim.SetTrigger(TRIGGER_IDLE);
            anim.ResetTrigger(TRIGGER_FORWARD);
            anim.ResetTrigger(TRIGGER_SAGCAPRAZ);
            anim.ResetTrigger(TRIGGER_SOLCAPRAZ);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            anim.SetTrigger(TRIGGER_BACK);
            anim.ResetTrigger(TRIGGER_IDLE);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            anim.SetTrigger(TRIGGER_IDLE);
            anim.ResetTrigger(TRIGGER_BACK);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetTrigger(TRIGGER_LEFT);
            anim.ResetTrigger(TRIGGER_IDLE);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetTrigger(TRIGGER_IDLE);
            anim.ResetTrigger(TRIGGER_LEFT);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetTrigger(TRIGGER_RIGHT);
            anim.ResetTrigger(TRIGGER_IDLE);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetTrigger(TRIGGER_IDLE);
            anim.ResetTrigger(TRIGGER_RIGHT);
        }
       
    }


}
