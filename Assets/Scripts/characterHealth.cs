using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterHealth : MonoBehaviour
{
    public static int healt = 100;

    public static characterHealth Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = new characterHealth();
        }
        else
        {
            Instance = this;
        }
    }
    private void Update()
    {
        //can yazdýrma
        Debug.Log(healt);
    }

}
