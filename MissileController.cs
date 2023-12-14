using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    public Transform target;
    public float speed = 10f;
    public float height = 10f;
    public float gravity = -9.8f;

    public float timeToTarget;
    private bool launchMissile = false;

    void Start()
    {
        CalculateTimeToTarget();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            launchMissile = true;
        }

        if (launchMissile)
        {
            MoveMissile();
        }
    }

    private void CalculateTimeToTarget()
    {
        float distanceToTarget = Vector3.Distance(transform.position, target.position);
        timeToTarget = distanceToTarget / speed;
    }


    private void MoveMissile()
    {
        // zamanı azalt
        timeToTarget -= Time.deltaTime;

        if (timeToTarget > 0f)
        {
            //füzenin yüksekliğini parabolik şekilde değiştir
            float verticalDistance = height - 0.5f * gravity * Mathf.Pow(timeToTarget, 2);
            float horizontalDistance = Vector3.Distance(transform.position, target.position);

            //hedefe doğru yönel
            Vector3 targetDirection = (target.position - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);

            //Vector3 horizontalDirection = new Vector3(target.position.x - transform.position.x, 0, target.position.z - transform.position.z).normalized;
            //Quaternion targetRotation = Quaternion.LookRotation(horizontalDirection);
            //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);

            //Quaternion.LookRotation(horizontalDirection);

            //füze hareketi
            float missileSpeed = horizontalDistance / timeToTarget;
            transform.Translate(Vector3.forward * missileSpeed * Time.deltaTime);
            transform.Translate(Vector3.up * verticalDistance * Time.deltaTime);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}