using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;
    public float smoothSpeed = 10f;

    public Vector3 fixPos = new Vector3(0, 5, 10);
    public Vector3 fixAngle = new Vector3(-10, 0, 0);

    void Start()
    {
    }

    void LateUpdate()
    {
        if (Target == null)
        {
            Debug.LogWarning("Takip edilecek kameraya atama yapılmadı.");
            return;
        }

        Vector3 targetPos = Target.position - Vector3.forward * fixPos.z + Vector3.up * fixPos.y;
        targetPos += Vector3.right * fixPos.x;
        Quaternion targetRot = Quaternion.Euler(CalculateXRotation(), fixAngle.y, fixAngle.z);

        transform.position = Vector3.Lerp(transform.position, targetPos, smoothSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, smoothSpeed * Time.deltaTime);
    }

    float CalculateXRotation()
    {
        Vector3 dir = Target.position - transform.position;
        float xRotation = -Mathf.Atan2(dir.y, dir.z) * Mathf.Rad2Deg + fixAngle.x;
        return xRotation;
    }
}