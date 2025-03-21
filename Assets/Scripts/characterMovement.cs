using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterMovement : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    Rigidbody rb;
    Vector3 temp;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        
    }

    private void FixedUpdate() {
        temp = transform.position;
        temp.y = Mathf.Clamp(temp.y, 1.624f, 2.713f);
        temp.z = Mathf.Clamp(temp.z, -0.288f, 0.879f);
        transform.position = temp;
         if (Input.GetKey(KeyCode.A)) rb.AddForce(Vector3.forward);
         if (Input.GetKey(KeyCode.D)) rb.AddForce(Vector3.back);
         if (Input.GetKey(KeyCode.W)) rb.AddForce(Vector3.up);
         if (Input.GetKey(KeyCode.S)) rb.AddForce(Vector3.down);

    }
}
