using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
    [SerializeField]
    private float mouseSensitivity = 3.0f;

    private float rotationY;
    private float rotationX;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private float distanceFromTarget = 10.0f;
    

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        rotationY += mouseX;
        rotationX += -mouseY;

        rotationX = Mathf.Clamp(rotationX, -70, 70);

        if(mouseSensitivity < 0)
        {
            mouseSensitivity = 0;
        }

        if(distanceFromTarget < 0)
        {
            distanceFromTarget = 0;
        }

        

        if(Input.GetMouseButton(1))
        {
            transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);
            transform.position = target.position - transform.forward * distanceFromTarget;
        }
    }
}
