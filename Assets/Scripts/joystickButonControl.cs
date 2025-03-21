using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class joystickButonControl : MonoBehaviour
{

    Animator anim;

    private void Start()
    {
       anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            anim.SetBool("down", true);
            anim.SetBool("idle", false);
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            anim.SetBool("down", false);
            anim.SetBool("idle", true);
        }
    }
}
