using System;
using UnityEngine;

public class MissileMover : MonoBehaviour
{
    public Transform target;
    public float speed = 10f;
    public float gravity = -9.8f;

    public Vector3 initialPosition;
    public Quaternion initialRotation;
    public float timeOfFlight;

    private void Start()
    {
        initialPosition = transform.position;
        initialRotation = Quaternion.Euler(-90f, 0f, 0f);

        CalculateTimeOfFlight();
    }

    private void Update()
    {
        MoveMissile();
    }

    private void CalculateTimeOfFlight()
    {
        float verticalDistance = target.position.y - initialPosition.y;
        float g = Mathf.Abs(gravity);
        float verticaltime = Mathf.Sqrt(2f * verticalDistance / g);
        timeOfFlight = verticaltime + verticaltime * Mathf.Sin(initialRotation.eulerAngles.x * Mathf.Deg2Rad);
    }

    private void MoveMissile()
    {
        timeOfFlight -= Time.deltaTime;
        if (timeOfFlight > 0f)
        {
            float horizontalDistance = speed * timeOfFlight * Mathf.Cos(initialRotation.eulerAngles.x * Mathf.Deg2Rad);
            float verticalDistance = speed * timeOfFlight * Mathf.Sin(initialRotation.x * Mathf.Deg2Rad) +
                                     0.5f * gravity * Mathf.Pow(timeOfFlight, 2);

            Vector3 newPosition = initialPosition + transform.forward * horizontalDistance +
                                  transform.up * verticalDistance;
            transform.position = newPosition;
            //transform.position = Quaternion.Slerp(transform.rotation,
            //    Quaternion.LookRotation(target.position - transform.position), Time.deltaTime);
            transform.LookAt(target);
        }
        else
        {
            //estroy(gameObject);
        }
    }
}