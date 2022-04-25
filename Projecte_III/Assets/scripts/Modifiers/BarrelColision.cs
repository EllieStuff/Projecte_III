using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelColision : MonoBehaviour
{
    BarrelScript barrel;

    [SerializeField] float pushForce = 2.0f;
    [SerializeField] float collisionTimedown = 1.0f;
    private void Start()
    {
        barrel = transform.parent.GetComponent<BarrelScript>();

        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("Player"))
        {
            Debug.Log("Collision with player");

            //----------
            //Apply pushVector to the player velocity
            other.transform.parent.GetComponent<VehicleTriggerAndCollisionEvents>().ApplyForce(pushForce, collisionTimedown);
            //----------

            Physics.IgnoreCollision(other, GetComponent<MeshCollider>());
            if (barrel.GetType() == BarrelScript.BarrelType.EXPLOSIVE)
            {
                barrel.Explode();
                Physics.IgnoreCollision(other, transform.GetComponentInChildren<SphereCollider>());
            }

            GetComponent<CapsuleCollider>().enabled = false;
        }
    }
}
