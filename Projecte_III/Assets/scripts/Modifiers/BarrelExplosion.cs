using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelExplosion : MonoBehaviour
{
    [SerializeField] float pushMaxForce = 0.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("Player"))
        {
            //Get the vector between the player and the center of the explosion and sets the value of the magnitude between 0-1
            float distance = Vector3.Magnitude(other.transform.position - transform.position) / GetComponent<SphereCollider>().radius;

            //Normalize and set the currentForce as the new magnitude of this vector
            float currentForce = pushMaxForce * (1 - distance);

            //----------
            //Apply pushVector to the player velocity
            other.transform.parent.GetComponent<VehicleTriggerAndCollisionEvents>().ApplyForce(currentForce);

            //----------

            Debug.Log("Player inside exlosion");
        }
    }
}
