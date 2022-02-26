using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCollidersManager : MonoBehaviour
{
    [SerializeField] Vector3 streamDir;
    [SerializeField] float streamForce;
    public Vector3 StreamVector { get { return streamDir.normalized * streamForce; } }


}
