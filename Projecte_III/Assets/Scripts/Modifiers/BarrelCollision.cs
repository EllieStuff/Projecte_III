using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelCollision : MonoBehaviour
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

            //Particules d'explosio del barril (si es el barril explosiu fum i tal)

            //

            GetComponent<MeshRenderer>().enabled = false;
            if (barrel.GetType() == BarrelScript.BarrelType.EXPLOSIVE)
            {
                barrel.Explode();
                AudioManager.Instance.Play_SFX("ExplodingBarrel_SFX");
                Physics.IgnoreCollision(other, transform.GetComponentInChildren<SphereCollider>());
            }

            GetComponent<CapsuleCollider>().enabled = false;
        }
    }
}
