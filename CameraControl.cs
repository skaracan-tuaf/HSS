using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Camera[] cameras;

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            CloseAllCameras();
            cameras[0].gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            CloseAllCameras();
            cameras[1].gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            CloseAllCameras();
            cameras[2].gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            CloseAllCameras();
            cameras[3].gameObject.SetActive(true);
        }
    }

    void CloseAllCameras()
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(false);
        }
    }
}