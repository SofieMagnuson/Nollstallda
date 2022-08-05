using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Camera_Follow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float smooth;

    private void Start()
    {
        Vector3 targetPosition = target.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smooth);
        transform.position = smoothPosition;
    }
    void FixedUpdate()
    {

    }
    


    
}
