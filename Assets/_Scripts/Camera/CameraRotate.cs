using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{

    public GameObject player;
    public float rotateSpeed = 500f;

    private float minRotate = 1f;
    private float maxRotate = 80f;


    private void Update()
    {
        // if left mouse button is being help down rotate camera
        if (Input.GetKey(KeyCode.Mouse0))
        {
            // RotateXY();
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            Cursor.lockState = CursorLockMode.Locked;
            RotateX();
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void LateUpdate()
    {
        // lock the position to the player
        transform.position = player.transform.position;

        // face the camera in the same direction the player is facing
        if (!Input.GetKey(KeyCode.Mouse0))
        {

        }

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, player.transform.eulerAngles.y, 0);


    }

    private void RotateX()
    {
        float mouseYInput = -Input.GetAxis("Mouse Y");

        float newXAngle = transform.eulerAngles.x + mouseYInput * rotateSpeed * Time.deltaTime;
        newXAngle = Mathf.Clamp(newXAngle, minRotate, maxRotate);
        transform.rotation = Quaternion.Euler(360 + newXAngle, 0, 0);
    }

    private void RotateXY()
    {
        float mouseYInput = -Input.GetAxis("Mouse Y");
        float mouseXInput = Input.GetAxis("Mouse X");

        float newXAngle = transform.eulerAngles.x + mouseYInput * rotateSpeed * Time.deltaTime;
        float newYAngle = transform.eulerAngles.y + mouseXInput * rotateSpeed * Time.deltaTime;
        newXAngle = Mathf.Clamp(newXAngle, minRotate, maxRotate);
        transform.rotation = Quaternion.Euler(360 + newXAngle, 360 + newYAngle, 0);
    }
}
