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
    [SerializeField] private float maxLerpSpeed;
    float timer;
    //Rigidbody vehicleRB;


    private void Start()
    {
        //Mateu helpppp
        target = GameObject.FindGameObjectWithTag("CameraObjective").transform;
        //vehicleRB = target.GetComponent<PlayerVehicleScript>().vehicleRB;
        transform.position = target.position + posOffset;
        transform.rotation = Quaternion.LookRotation(-transform.up);
        //transform.rotation.eulerAngles = rotOffset;
    }
    private void Update()
    {
        fovSystem();
        HandlePosition();
        HandleRotation();
        //RaycastCamera();
        //Debug.Log("Timer: " + timer * rotSpeed);
    }

    private void fovSystem()
    {
        PlayerVehicleScript pScript = target.GetComponent<PlayerVehicleScript>();
        Camera cam = GetComponent<Camera>();

        //float savedFov = new Vector3(vehicleRB.velocity.x, 0, vehicleRB.velocity.z).magnitude * 80 / pScript.vehicleMaxSpeed;

        //if (pScript.vehicleMaxSpeed > pScript.savedMaxSpeed && savedFov >= 60)
        //    cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, savedFov, Time.deltaTime * 0.8f);
        //else
        //    cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 60, Time.deltaTime * 5);
    }

    private void HandlePosition()
    {
        Vector3 targetPos = target.TransformPoint(posOffset);
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * maxLerpSpeed);

    }

    private void HandleRotation()
    {
        timer += Time.deltaTime;
        Vector3 dir = ((target.position + rotOffset) - transform.position).normalized;
        Quaternion targetRot = Quaternion.LookRotation(dir, Vector3.up);
        Vector3 euler = targetRot.eulerAngles;

        targetRot = Quaternion.Euler(euler.x, euler.y, 0);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, Time.deltaTime * rotSpeed);

    }

    private void RaycastCamera()
    {
        Ray ray = new Ray(target.position, (transform.position - target.position).normalized);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, Vector3.Distance(transform.position, target.position), -1, QueryTriggerInteraction.Ignore))
        {
            transform.position = raycastHit.point;
        }
    }


}
