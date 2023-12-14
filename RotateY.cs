using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateY : MonoBehaviour
{
    public float maxRotationSpeed = 5000f;
    public float droneSpeed = 1000f;

    void Start()
    {
    }


    void Update()
    {
        transform.Rotate(Vector3.forward, droneSpeed * Time.deltaTime);
    }
}