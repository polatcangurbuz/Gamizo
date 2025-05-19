using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class characterMovement : MonoBehaviour
{
    [SerializeField] float forceSpeed = 2f;

    Rigidbody rb;
    Vector3 temp;
    Vector3 moveDirection;

    public float movSpeed = 0.08f;

    [SerializeField] TopuzMovement TopuzMovement;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        moveDirection = Vector3.zero; // Önce vektörü sýfýrla
        //moveDirection.z = joystick.Horizontal;  // X ekseni için joystick kontrolü
        //moveDirection.y = joystick.Vertical;    // Z ekseni için joystick kontrolü
        moveDirection.z = -TopuzMovement.currentZ * movSpeed;
        moveDirection.y = -TopuzMovement.currentX * movSpeed ;
    }



    private void FixedUpdate()
    {
        // Pozisyon sýnýrlamasýný uygula
        temp = transform.position;
        temp.y = Mathf.Clamp(temp.y, 1.624f, 2.713f);
        temp.z = Mathf.Clamp(temp.z, -0.288f, 0.879f);
        transform.position = temp;

        // Hareketi uygula
        rb.velocity = new Vector3(rb.velocity.x, moveDirection.y * forceSpeed, -moveDirection.z * forceSpeed);
    }


}