using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathfallAndCheckpointsSystem : MonoBehaviour
{
    //public Vector3 savedPosition;
    //public int ID = -1;
    GameObject chasis;

    // Start is called before the first frame update
    void Start()
    {
        chasis = GameObject.Find("vehicleChasis");
    }

    private void OnTriggerEnter(Collider other)
    {
        chasis = GameObject.Find("vehicleChasis");
        //Debug.Log(chasis);
        if (gameObject.tag.Equals("Respawn") && chasis == other.gameObject)
        {
            chasis.GetComponentInParent<PlayerVehicleScript>().respawnPosition = this.transform.position;
            //Debug.Log(chasis.GetComponentInParent<PlayerVehicleScript>().respawnPosition);
        }
        if (gameObject.tag.Equals("Death Zone") && chasis == other.gameObject)
        {
            other.GetComponentInParent<Transform>().parent.position = chasis.GetComponentInParent<PlayerVehicleScript>().respawnPosition;
            //Debug.Log(other.GetComponentInParent<Transform>().parent.position);
        }
    }
}
