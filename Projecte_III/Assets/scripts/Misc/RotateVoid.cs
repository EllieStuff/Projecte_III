using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateVoid : MonoBehaviour
{
    public bool xAffected = true, yAffected = true, zAffected = true;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 randRot = transform.rotation.eulerAngles;
        if (xAffected) randRot.x = 90 * Random.Range(0, 4);
        if (yAffected) randRot.y = 90 * Random.Range(0, 4);
        if (zAffected) randRot.z = 90 * Random.Range(0, 4);
        transform.rotation = Quaternion.Euler(randRot);
    }

}
