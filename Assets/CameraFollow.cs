using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform Target;
    public Vector3 Offset;
    public float SmoothTime = 0.3f;
    private Transform camTransform;
    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        camTransform = GetComponent<Transform>();
        Offset = camTransform.position - Target.position;
        Target = GameObject.FindGameObjectsWithTag("Player")[0].transform;
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = Target.position + Offset;
        camTransform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, SmoothTime);
    }
}

