using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatCam : MonoBehaviour
{
    public float turnSpeed = 4.0f;
    private float xRotate = 0.0f;

    void Update()
    {
        MouseRotation();
    }

    void MouseRotation()
    {
        float yRotateSize = Input.GetAxis("Mouse X") * turnSpeed;
        float yRotate = transform.eulerAngles.y + yRotateSize;

        float xRotateSize = -Input.GetAxis("Mouse Y") * turnSpeed;
        xRotate = Mathf.Clamp(xRotate + xRotateSize, -45, 80);

        transform.eulerAngles = new Vector3(xRotate, yRotate, 0);
    }

}
