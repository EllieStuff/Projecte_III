using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject playerVehicle;
    public float camSpeed;

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(playerVehicle.transform.position.x, playerVehicle.transform.position.y + 2, playerVehicle.transform.position.z), Time.deltaTime * camSpeed);

        if (Input.GetKey(KeyCode.Tab))
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, new Quaternion(this.transform.rotation.x, playerVehicle.transform.rotation.y, this.transform.rotation.z, playerVehicle.transform.rotation.w) * Quaternion.Euler(new Vector3(0, 180, 0)), Time.deltaTime * camSpeed);
        else
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, new Quaternion(this.transform.rotation.x, playerVehicle.transform.rotation.y, this.transform.rotation.z, playerVehicle.transform.rotation.w), Time.deltaTime * camSpeed);
    }
}
