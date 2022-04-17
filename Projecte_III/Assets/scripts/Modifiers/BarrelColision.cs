using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelColision : MonoBehaviour
{
    BarrelScript barrel;

    [SerializeField] float pushForce = 0.0f;

    private void Start()
    {
        barrel = transform.parent.GetComponent<BarrelScript>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Contains("Player"))
        {
            Debug.Log("Collision with player");

            //Get the vector between the player and the center of the explosion and sets the value of the magnitude between 0-1
            Vector3 pushVector = collision.transform.position - transform.position;

            //Normalize and set the pushForce as the new magnitude of this vector
            pushVector = pushVector.normalized * pushForce;

            //----------
                //Apply pushVector to the player velocity

            //----------

            if (barrel.GetType() == BarrelScript.BarrelType.EXPLOSIVE)
                barrel.Explode();
        }
    }
}
