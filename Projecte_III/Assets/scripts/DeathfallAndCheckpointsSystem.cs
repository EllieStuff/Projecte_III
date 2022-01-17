using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathfallAndCheckpointsSystem : MonoBehaviour
{
    //public Vector3 savedPosition;
    //public int ID = -1;
    GameObject chasis;
    //public GameObject fallEffectSound;

    // Start is called before the first frame update
    void Start()
    {
        chasis = GameObject.Find("vehicleChasis");
        //Set default checkpoint (should be the first of the level)
        if (this.name.Equals("Checkpoint"))
        {
            chasis.GetComponentInParent<PlayerVehicleScript>().respawnPosition = this.transform.position;
            chasis.GetComponentInParent<PlayerVehicleScript>().respawnRotation = this.transform.localEulerAngles;
            chasis.GetComponentInParent<PlayerVehicleScript>().respawnVelocity = new Vector3(0, 0, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //chasis = GameObject.Find("vehicleChasis");
        //Debug.Log(chasis);
        if (gameObject.tag.Equals("Respawn") && chasis == other.gameObject)
        {
            chasis.GetComponentInParent<PlayerVehicleScript>().respawnPosition = this.transform.position;
            chasis.GetComponentInParent<PlayerVehicleScript>().respawnRotation = this.transform.localEulerAngles;
            chasis.GetComponentInParent<PlayerVehicleScript>().respawnVelocity = new Vector3(0, 0, 0);
            //Debug.Log(chasis.GetComponentInParent<PlayerVehicleScript>().respawnPosition);
        }
        if (gameObject.tag.Equals("Death Zone") && chasis == other.gameObject)
        {
            //GameObject soundFall = Instantiate(fallEffectSound, this.transform.position, this.transform.rotation);
            AudioManager.Instance.Play_SFX("Fall_SFX");
            other.GetComponentInParent<Transform>().parent.position = chasis.GetComponentInParent<PlayerVehicleScript>().respawnPosition;
            other.GetComponentInParent<PlayerVehicleScript>().vehicleRB.velocity = chasis.GetComponentInParent<PlayerVehicleScript>().respawnVelocity;
            other.GetComponentInParent<Transform>().parent.localEulerAngles = chasis.GetComponentInParent<PlayerVehicleScript>().respawnRotation;
            other.GetComponentInParent<Transform>().parent.localEulerAngles += new Vector3(0, 90, 0);
            
            //Debug.Log(other.GetComponentInParent<Transform>().parent.position);
        }
    }
}
