using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballCollision : MonoBehaviour
{
    //characterHealth characterHealth;
    private void Awake()
    {
        //characterHealth = new characterHealth();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (characterHealth.Instance != null)
            {
                characterHealth.Instance.health -= 5;
                Debug.Log("Karakterin caný: " + characterHealth.Instance.health);
            }
        }
    }

    

}
