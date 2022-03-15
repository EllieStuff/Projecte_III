using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 posOffset;
    [SerializeField] private Vector3 rotOffset;
    [SerializeField] private Vector3 posVelocity;
    [SerializeField] private float rotSpeed;
    [SerializeField] private float maxDampSpeed;
    float timer;


    private void Start()
    {
        //Mateu helpppp
        target = GameObject.FindGameObjectWithTag("Player").transform;
        transform.position = target.position;
    }
    private void Update()
    {
        HandlePosition();
        HandleRotation();
        RaycastCamera();

    }


    private void HandlePosition()
    {
        Vector3 targetPos = target.TransformPoint(posOffset);
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref posVelocity, Time.deltaTime, maxDampSpeed);

    }

    private void HandleRotation()
    {
        timer += Time.deltaTime;
        Vector3 dir = ((target.position + rotOffset) - transform.position).normalized;
        Quaternion targetRot = Quaternion.LookRotation(dir, Vector3.up);
        Vector3 euler = targetRot.eulerAngles;

        targetRot = Quaternion.Euler(euler.x, euler.y, 0);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, timer / 5);

    }

    private void RaycastCamera()
    {
        Ray ray = new Ray(target.position, target.TransformDirection(new Vector3(0, 0.1f, -1)));
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue))
        {
            float dist = Vector3.Distance(transform.position, target.position);
            if(dist < 30)
            transform.position = raycastHit.point;
        }
    }


}
