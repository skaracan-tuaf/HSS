using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterController : MonoBehaviour
{
    public float Speed = 50.0f; // Speed
    public float SpeedMax = 60.0f; // Max speed
    public float RotationSpeed = 50.0f; // Turn Speed
    public float SpeedPitch = 2; // rotation X
    public float SpeedRoll = 3; // rotation Z
    public float SpeedYaw = 1; // rotation Y
    private float MoveSpeed = 10; // normal move speed

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, 0, Speed * Time.deltaTime));
    }
}