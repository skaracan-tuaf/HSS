using System;
using UnityEngine;


public class Propeller : MonoBehaviour
{
    public Transform propeller1;
    public Transform propeller2;
    public Transform propeller3;
    public Transform propeller4;

    public float rotationSpeed = 100f;

    private void Update()
    {
        propeller1.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        propeller2.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        propeller3.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        propeller4.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}