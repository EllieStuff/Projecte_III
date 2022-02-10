using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathfallAndCheckpointsSystem : MonoBehaviour
{
    PlayerVehicleScript vehicleScript;
    GameObject chasis;

    // Start is called before the first frame update
    void Start()
    {
        vehicleScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerVehicleScript>();
        chasis = vehicleScript.transform.Find("vehicleChasis").gameObject;
        //Set default checkpoint (should be the first of the level)
        if (name.Equals("Checkpoint"))
        {
            vehicleScript.respawnPosition = transform.position;
            vehicleScript.respawnRotation = transform.localEulerAngles;
            vehicleScript.respawnVelocity = Vector3.zero;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag.Equals("Respawn") && chasis == other.gameObject)
        {
            vehicleScript.respawnPosition = transform.position;
            vehicleScript.respawnRotation = transform.localEulerAngles;
            vehicleScript.respawnVelocity = Vector3.zero;
            //Debug.Log(chasis.GetComponentInParent<PlayerVehicleScript>().respawnPosition);
        }
        if (gameObject.tag.Equals("Death Zone") && chasis == other.gameObject)
        {
            AudioManager.Instance.Play_SFX("Fall_SFX");

            other.GetComponentInParent<Transform>().parent.position = vehicleScript.respawnPosition;
            other.GetComponentInParent<PlayerVehicleScript>().vehicleRB.velocity = vehicleScript.respawnVelocity;
            other.GetComponentInParent<PlayerVehicleScript>().vehicleRB.angularVelocity = vehicleScript.respawnVelocity;
            other.GetComponentInParent<PlayerVehicleScript>().vehicleRB.constraints = RigidbodyConstraints.FreezePositionX;
            other.GetComponentInParent<PlayerVehicleScript>().vehicleRB.constraints = RigidbodyConstraints.FreezePositionZ;
            other.GetComponentInParent<Transform>().parent.localEulerAngles = vehicleScript.respawnRotation;
            other.GetComponentInParent<Transform>().parent.localEulerAngles += new Vector3(0, 90, 0);

            
            
            //Debug.Log(other.GetComponentInParent<Transform>().parent.position);
        }
    }
}
