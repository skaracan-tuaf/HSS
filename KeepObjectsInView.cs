using System;
using UnityEngine;


public class KeepObjectsInView : MonoBehaviour
{
    public Camera mainCamera;
    public float smoothness = 5f;
    public float distanceMultiplier = 5f;

    private void LateUpdate()
    {
        Bounds bounds = CalculateBounds(transform);

        float distance = (bounds.size.x + bounds.size.y + bounds.size.z) / 3f /
                         Mathf.Tan(mainCamera.fieldOfView * .5f * Mathf.Deg2Rad);

        float increasedistance = distance * distanceMultiplier;
    
        Vector3 targetPosition = bounds.center - mainCamera.transform.forward * increasedistance;
        Quaternion targetRotation = Quaternion.LookRotation(bounds.center - mainCamera.transform.position, Vector3.up);


        mainCamera.transform.position =
            Vector3.Lerp(mainCamera.transform.position, targetPosition,
                Time.deltaTime * smoothness); //bounds.center - mainCamera.transform.forward * distance;
        mainCamera.transform.rotation =
            Quaternion.Slerp(mainCamera.transform.rotation, targetRotation, Time.deltaTime * smoothness);
        //mainCamera.transform.LookAt(bounds.center);
    }

    private Bounds CalculateBounds(Transform target)
    {
        Bounds mBounds = new Bounds(target.position, Vector3.zero);
        Renderer renderer = target.GetComponent<Renderer>();
        if (renderer != null)
        {
            mBounds.Encapsulate(renderer.bounds);
        }

        foreach (Transform child in target)
        {
            mBounds.Encapsulate(CalculateBounds(child));
        }

        return mBounds;
    }
}