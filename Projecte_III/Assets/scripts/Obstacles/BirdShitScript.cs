using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdShitScript : MonoBehaviour
{
    [SerializeField] GameObject shittyDecal;


    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Random.rotation;
        Destroy(gameObject, 10.0f);
    }


    private void OnCollisionEnter(Collision col)
    {
        if (col.transform.CompareTag("PlayerVehicle"))
        {
            Decals.SpawnDecal(shittyDecal, col);
            Destroy(gameObject);
        }
        else if (!Decals.TagToIgnore(col.transform.tag))
        {
            Decals.SpawnDecal(shittyDecal, col);
            GetComponent<Rigidbody>().mass = 20;
        }

    }

}
