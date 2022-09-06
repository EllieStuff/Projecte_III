using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidObstacleScript : MonoBehaviour
{
    List<Collider> affectedRoads = new List<Collider>();
    MapWarningObstacle warningSystem;

    private void Start()
    {
        warningSystem = GameObject.Find("HUD").transform.Find("MapEventWarning").GetComponent<MapWarningObstacle>();
        warningSystem.InstantiateWarning(new Vector3(Mathf.Clamp(transform.parent.parent.localPosition.x * 50, -400, 400), 480, 0), 1.5f);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Road"))
        {
            //Debug.Log(other.transform.position);
            other.GetComponent<MeshRenderer>().material.renderQueue = 3002;
            affectedRoads.Add(other);
        }
        if (other.CompareTag("PlayerVehicle"))
        {
            for (int i = 0; i < affectedRoads.Count; i++)
            {
                Physics.IgnoreCollision(other, affectedRoads[i], true);
            }
        }
        if (other.CompareTag("vehicleElement"))
        {
            for (int i = 0; i < affectedRoads.Count; i++)
            {
                other.GetComponent<WheelCollider>().enabled = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Road"))
        {
            Debug.Log(other.transform.position);
            other.GetComponent<MeshRenderer>().material.renderQueue = 2000;
            //Collider _col = affectedRoads.Find(_col => _col == other);
            //if (_col != null) affectedRoads.Remove(other);
            affectedRoads.Remove(other);
        }
        if (other.CompareTag("PlayerVehicle"))
        {
            for (int i = 0; i < affectedRoads.Count; i++)
            {
                Physics.IgnoreCollision(other, affectedRoads[i], false);
            }
        }
        if (other.CompareTag("vehicleElement"))
        {
            for (int i = 0; i < affectedRoads.Count; i++)
            {
                other.GetComponent<WheelCollider>().enabled = true;
            }
        }
    }
}
