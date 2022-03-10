using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraRot : MonoBehaviour
{
    [SerializeField] Vector3 newRot;
    [SerializeField] bool useInCustomRotSpeed = false;
    [SerializeField] float inCustomRotSpeed = 1.2f;
    [SerializeField] bool useOutCustomRotSpeed = false;
    [SerializeField] float outCustomRotSpeed = 1.2f;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerVehicle"))
        {
            //Debug.Log("In");
            CameraScript camera = Camera.main.GetComponentInParent<CameraScript>();
            if (!useInCustomRotSpeed)
                camera.ChangeRotation(newRot);
            else
                camera.ChangeRotation(newRot, inCustomRotSpeed);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerVehicle"))
        {
            //Debug.Log("Out");
            CameraScript camera = Camera.main.GetComponentInParent<CameraScript>();
            if (!useOutCustomRotSpeed)
                camera.ResetRotation();
            else
                camera.ResetRotation(outCustomRotSpeed);
        }

    }
}
