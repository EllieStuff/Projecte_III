using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraRot : MonoBehaviour
{
    [SerializeField] Vector3 newRot;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerVehicle"))
        {
            //Debug.Log("In");
            CameraScript camera = Camera.main.GetComponentInParent<CameraScript>();
            camera.ChangeRotation(newRot);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerVehicle"))
        {
            //Debug.Log("Out");
            CameraScript camera = Camera.main.GetComponentInParent<CameraScript>();
            camera.ResetRotation();
        }

    }
}
