using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopuzMovement : MonoBehaviour
{
    private Vector3 mouseStartPos;
    private bool isDragging = false;

    public float sensitivity = 0.3f; 

    public  float currentX = 0f;
    public float currentZ = 0f;

    public float deltaX;
    public float deltaY;

    public Vector3 currentMousePos;

    private void OnMouseDown()
    {
        mouseStartPos = Input.mousePosition;
        isDragging = true;
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }

    private void OnMouseDrag()
    {
        if (!isDragging) return;

        currentMousePos = Input.mousePosition;

        deltaY = currentMousePos.y - mouseStartPos.y; 
        deltaX = currentMousePos.x - mouseStartPos.x; 

        currentX -= deltaY * sensitivity;
        currentZ -= deltaX * sensitivity;

        currentX = Mathf.Clamp(currentX, -20f, 30f);
        currentZ = Mathf.Clamp(currentZ, -20f, 20f);

        transform.localEulerAngles = new Vector3(currentX, 0f, -currentZ);
       
        mouseStartPos = currentMousePos;
    }
}
