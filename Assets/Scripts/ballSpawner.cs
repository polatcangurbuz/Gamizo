using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballSpawner : MonoBehaviour
{
    Rigidbody rb;
    GameObject instant;
   
    public static int value = 0;
    GameObject[] balls;
    public GameObject ball;
    public static ballSpawner Instance { get; private set; }


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        rb = GetComponent<Rigidbody>();
        balls = new GameObject[5];
    }
    public void ballInstant()
    {
        if (value >= balls.Length)
        {
            return; // Eðer sýnýr aþýlmýþsa iþlem yapýlmaz
        }

        instant = Instantiate(ball, enemySpawner.Instance.instant.transform.position, Quaternion.identity);
        balls[value] = instant;
        value++;
    }

    
  
  




}//class
