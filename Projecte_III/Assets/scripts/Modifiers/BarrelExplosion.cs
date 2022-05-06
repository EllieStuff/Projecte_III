using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelExplosion : MonoBehaviour
{
    [SerializeField] float maxPushForce = 0.0f;
    [SerializeField] float minPushForce = 0.0f;
    [SerializeField] float collisionTimedown = 1.0f;

    float explosionTime = 2.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("Player"))
        {
            //Get the vector between the player and the center of the explosion and sets the value of the magnitude between 0-1
            float distance = Vector3.Magnitude(other.transform.position - transform.position) / GetComponent<SphereCollider>().radius;

            //Normalize and set the currentForce as the new magnitude of this vector
            float currentForce = maxPushForce * (1 - distance) + minPushForce;

            //----------
            //Apply pushVector to the player velocity
            other.transform.parent.GetComponent<VehicleTriggerAndCollisionEvents>().ApplyForce(currentForce, collisionTimedown);

            //Broken Motor Particles
            other.transform.parent.GetChild(3).GetChild(5).GetComponent<ParticleSystem>().Play();

            //----------
            Physics.IgnoreCollision(other, GetComponent<SphereCollider>());

            Debug.Log("Player inside exlosion");
        }
    }

    private void Update()
    {
        if(explosionTime < 0)
        {
            Destroy(gameObject);
        }
        explosionTime -= Time.deltaTime;
    }
}
