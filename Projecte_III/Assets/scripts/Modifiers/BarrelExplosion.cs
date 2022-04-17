using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelExplosion : MonoBehaviour
{
    [SerializeField] float pushMaxForce = 0.0f;
    float currentForce = 0.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("Player"))
        {
            //Get the vector between the player and the center of the explosion and sets the value of the magnitude between 0-1
            Vector3 pushVector = other.transform.position - transform.position;
            float distance = Vector3.Magnitude(pushVector) / GetComponent<SphereCollider>().radius;

            //Normalize and set the currentForce as the new magnitude of this vector
            currentForce = pushMaxForce * (1 - distance);
            pushVector = pushVector.normalized * currentForce;

            //----------
                //Apply pushVector to the player velocity

            //----------

            Debug.Log("Player inside exlosion");
        }
    }
}
