using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraScript : MonoBehaviour
{
    [SerializeField] Vector3 posOffset;
    [SerializeField] Vector3 rotOffset;

    public QuadControls controls;

    PlayerVehicleScript playerScript;

    float camPosSpeed = 5.0f;
    [SerializeField] float camRotSpeed = 1.1f;
    float savedRotSpeed;
    Quaternion rotOffsetQuat, lookBackRotOffset;
    private Camera cam;
    private Rigidbody vehicleRB;
    PlayersManager playersManager;

    private void Start()
    {
        cam = transform.GetChild(0).GetComponent<Camera>();
        controls = new QuadControls();
        controls.Enable();

        rotOffsetQuat = Quaternion.Euler(rotOffset);
        lookBackRotOffset = Quaternion.Euler(0, 180, 0);
        savedRotSpeed = camRotSpeed;

        playersManager = GameObject.FindGameObjectWithTag("PlayersManager").GetComponent<PlayersManager>();

        //if(camIndex == 0)
        //    playerVehicle = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).GetChild(0).gameObject;
        //else if(camIndex == 1)
        //    playerVehicle = GameObject.FindGameObjectWithTag("Player2").transform.GetChild(0).GetChild(0).gameObject;
       
        transform.rotation = rotOffsetQuat;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 targetPosP1 = new Vector3(playersManager.GetPlayer(0).position.x, playersManager.GetPlayer(0).position.y + 5, playersManager.GetPlayer(0).position.z);
        Vector3 targetPosP2 = new Vector3(playersManager.GetPlayer(1).position.x, 0, playersManager.GetPlayer(1).position.z);
        Vector3 targetPosP3 = new Vector3(playersManager.GetPlayer(2).position.x, 0, playersManager.GetPlayer(2).position.z);
        Vector3 targetPosP4 = new Vector3(playersManager.GetPlayer(3).position.x, 0, playersManager.GetPlayer(3).position.z);

        Vector3 targetPos = Vector3.zero;

        float savedFov = 20 + Vector3.Distance(targetPosP1, targetPosP2);

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, savedFov, Time.deltaTime * 1.5f);

        switch (playersManager.numOfPlayers)
        {
            case 1:
                targetPos = targetPosP1;
                break;
            case 2:
                targetPos = (targetPosP1/2) + (targetPosP2/2);
                break;
        }

        transform.position = Vector3.Lerp(this.transform.position + posOffset, targetPos, Time.deltaTime * camPosSpeed);
        //transform.rotation = Quaternion.Euler(rotOffset);
        
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, rotOffsetQuat, Time.deltaTime * camRotSpeed * 100);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotOffsetQuat, Time.deltaTime * camRotSpeed);
        
        //transform.rotation = rotOffsetQuat;
      
    }


    public void ChangeRotation(Vector3 _newRot)
    {
        rotOffsetQuat = Quaternion.Euler(_newRot);
    }
    public void ChangeRotation(Vector3 _newRot, float _customRotSpeed)
    {
        rotOffsetQuat = Quaternion.Euler(_newRot);
        camRotSpeed = _customRotSpeed;
    }

    public void ResetRotation()
    {
        rotOffsetQuat = Quaternion.Euler(rotOffset);
        camRotSpeed = savedRotSpeed;
    }
    public void ResetRotation(float _customRotSpeed)
    {
        rotOffsetQuat = Quaternion.Euler(rotOffset);
        camRotSpeed = _customRotSpeed;
    }

}
