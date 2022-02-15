using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathfallAndCheckpointsSystem : MonoBehaviour
{
    [SerializeField] private bool multiplayerMode;
    PlayerVehicleScript vehicleScript;
    PlayerVehicleScriptP2 vehicleScriptP2;
    GameObject chasis;
    GameObject chasisP2;

    // Start is called before the first frame update
    void Start()
    {
        vehicleScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerVehicleScript>();
        if(multiplayerMode)
        vehicleScriptP2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerVehicleScriptP2>();

        chasis = vehicleScript.transform.Find("vehicleChasis").gameObject;
        if(multiplayerMode)
        chasisP2 = vehicleScriptP2.transform.Find("vehicleChasis").gameObject;
        //Set default checkpoint (should be the first of the level)
        if (name.Equals("Checkpoint"))
        {
            vehicleScript.respawnPosition = transform.position;
            vehicleScript.respawnRotation = transform.localEulerAngles;
            vehicleScript.respawnVelocity = Vector3.zero;

            if(multiplayerMode)
            {
                vehicleScriptP2.respawnPosition = transform.position;
                vehicleScriptP2.respawnRotation = transform.localEulerAngles;
                vehicleScriptP2.respawnVelocity = Vector3.zero;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //PLAYER1
        if (gameObject.tag.Equals("Respawn") && chasis == other.gameObject)
        {
            vehicleScript.respawnPosition = transform.position;
            vehicleScript.respawnRotation = transform.localEulerAngles;
            vehicleScript.respawnVelocity = Vector3.zero;
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
        }
        //____________________________________________________________________________________________________________________________

        //PLAYER2
        if (multiplayerMode && gameObject.tag.Equals("Respawn") && chasisP2 == other.gameObject)
        {
            vehicleScriptP2.respawnPosition = transform.position;
            vehicleScriptP2.respawnRotation = transform.localEulerAngles;
            vehicleScriptP2.respawnVelocity = Vector3.zero;
            //Debug.Log(chasis.GetComponentInParent<PlayerVehicleScript>().respawnPosition);
        }
        if (multiplayerMode && gameObject.tag.Equals("Death Zone") && chasisP2 == other.gameObject)
        {
            AudioManager.Instance.Play_SFX("Fall_SFX");

            other.GetComponentInParent<Transform>().parent.position = vehicleScriptP2.respawnPosition;
            other.GetComponentInParent<PlayerVehicleScript>().vehicleRB.velocity = vehicleScriptP2.respawnVelocity;
            other.GetComponentInParent<PlayerVehicleScript>().vehicleRB.angularVelocity = vehicleScriptP2.respawnVelocity;
            other.GetComponentInParent<PlayerVehicleScript>().vehicleRB.constraints = RigidbodyConstraints.FreezePositionX;
            other.GetComponentInParent<PlayerVehicleScript>().vehicleRB.constraints = RigidbodyConstraints.FreezePositionZ;
            other.GetComponentInParent<Transform>().parent.localEulerAngles = vehicleScriptP2.respawnRotation;
            other.GetComponentInParent<Transform>().parent.localEulerAngles += new Vector3(0, 90, 0);



            //Debug.Log(other.GetComponentInParent<Transform>().parent.position);
        }
        //____________________________________________________________________________________________________________________________
    }
}
