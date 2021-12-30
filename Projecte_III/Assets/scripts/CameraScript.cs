using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] GameObject playerVehicle;
    [SerializeField] Vector3 posOffset;
    [SerializeField] Vector3 rotOffset;
    [SerializeField] float camSpeed;

    Quaternion rotOffsetQuat, lookBackRotOffset;

    private void Start()
    {
        rotOffsetQuat = Quaternion.Euler(rotOffset);
        lookBackRotOffset = Quaternion.Euler(0, 180, 0);

        this.transform.rotation = rotOffsetQuat;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.Lerp(this.transform.position + posOffset, new Vector3(playerVehicle.transform.position.x, playerVehicle.transform.position.y + 2, playerVehicle.transform.position.z), Time.deltaTime * camSpeed);

        if (Input.GetKeyDown(KeyCode.Tab))
            this.transform.rotation = rotOffsetQuat * lookBackRotOffset;
        else if (Input.GetKeyUp(KeyCode.Tab))
            this.transform.rotation = rotOffsetQuat;


        // Test Mode
        //if (Input.GetKey(KeyCode.Tab))
        //    this.transform.rotation = Quaternion.Euler(rotOffset) * lookBackRotOffset;
        //else
        //    this.transform.rotation = Quaternion.Euler(rotOffset);


        //if (Input.GetKey(KeyCode.Tab))
        //    this.transform.rotation = Quaternion.Lerp(this.transform.rotation, new Quaternion(this.transform.rotation.x, playerVehicle.transform.rotation.y, this.transform.rotation.z, playerVehicle.transform.rotation.w) * Quaternion.Euler(new Vector3(0, 180, 0)), Time.deltaTime * camSpeed);
        //else
        //    this.transform.rotation = Quaternion.Lerp(this.transform.rotation, new Quaternion(this.transform.rotation.x, playerVehicle.transform.rotation.y, this.transform.rotation.z, playerVehicle.transform.rotation.w), Time.deltaTime * camSpeed);
    }
}
