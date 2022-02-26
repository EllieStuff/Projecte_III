using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindZoneDirectionController : MonoBehaviour
{
    public float rotationVelocity;

    // Update is called once per frame
    void Update()
    {
        this.transform.eulerAngles += new Vector3(0, Random.Range(-rotationVelocity, rotationVelocity), 0); 
    }
}
