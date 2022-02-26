using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterStreamColliderScript : MonoBehaviour
{
    WaterCollidersManager manager;
    //Vector3 stream;
    public Vector3 Stream { get { return manager.StreamVector; } }

    // Start is called before the first frame update
    void Start()
    {
        //stream = GetComponentInParent<WaterCollidersManager>().StreamVector;
        manager = GetComponentInParent<WaterCollidersManager>();
    }


}
