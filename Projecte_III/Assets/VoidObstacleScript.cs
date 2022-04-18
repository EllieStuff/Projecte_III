using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidObstacleScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Road"))
        {
            other.GetComponent<MeshRenderer>().material.renderQueue = 3002;
            other.GetComponent<MeshCollider>().enabled = false;
        }
    }
}
