using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class characterMovement : MonoBehaviour
{
    [SerializeField] float speed = 5f, forceSpeed = 2f;
    Rigidbody rb;
    Vector3 temp;
    Vector3 moveDirection = Vector3.zero;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Input'u Update içinde kontrol et
        moveDirection = Vector3.zero; // Her karede hareket yönünü sýfýrla

        if (Input.GetKey(KeyCode.A)) moveDirection += Vector3.forward;
        if (Input.GetKey(KeyCode.D)) moveDirection += Vector3.back;
        if (Input.GetKey(KeyCode.W)) moveDirection += Vector3.up;
        if (Input.GetKey(KeyCode.S)) moveDirection += Vector3.down;
    }

    private void FixedUpdate()
    {
        // Pozisyon sýnýrlamasýný uygula
        temp = transform.position;
        temp.y = Mathf.Clamp(temp.y, 1.620f, 2.713f); // 624
        temp.z = Mathf.Clamp(temp.z, -0.259f, 0.900f);// 879 -288
        transform.position = temp;

        // Update içinde belirlenen yöne kuvvet uygula
        if (moveDirection != Vector3.zero)
        {
            rb.AddForce(moveDirection * forceSpeed);
        }
    }
}