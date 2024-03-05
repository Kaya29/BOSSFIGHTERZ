using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] float sensitivity = 100;
    [SerializeField] Transform playerBody;

    float xRotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        //Editorden atama yapılmamışsa kod düzeltme yapsın
        if (playerBody == null)
        {
            //Debug.Log("<color=red>Character objesinin kamerasında, Inspector'de scripte character ataması yapılmamış</color>\nkod ile düzeltildi");
            playerBody = transform.parent.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //transform.localRotation = Quaternion.Euler(xRotation, -90f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
