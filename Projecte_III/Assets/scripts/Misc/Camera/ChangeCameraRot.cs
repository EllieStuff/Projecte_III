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

    CameraManager cameraManager;

    private void Start()
    {
        cameraManager = GameObject.FindGameObjectWithTag("CamerasManager").GetComponent<CameraManager>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerVehicle"))
        {
            //Debug.Log("In");
            int playerId = other.transform.parent.GetComponentInParent<QuadSceneManager>().playerId;
            CameraScript camera = cameraManager.GetCamera(playerId).GetComponentInParent<CameraScript>();
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
            int playerId = other.transform.parent.GetComponentInParent<QuadSceneManager>().playerId;
            CameraScript camera = cameraManager.GetCamera(playerId).GetComponentInParent<CameraScript>();
            if (!useOutCustomRotSpeed)
                camera.ResetRotation();
            else
                camera.ResetRotation(outCustomRotSpeed);
        }

    }
}
