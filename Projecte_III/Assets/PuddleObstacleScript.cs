using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuddleObstacleScript : MonoBehaviour
{
    const float INIT_ANGULAR_DRAG = 5.0f;

    [SerializeField] float puddleFricction = 9999.9f;
    [SerializeField] float angularDragIncrease = 0.01f;
    [SerializeField] float maxSpeedIncrease = 999.9f;
    
    List<Rigidbody> playerRBs = new List<Rigidbody>();
    float initMaxSpeed, initMaxAngularSpeed;

    void Start()
    {
        transform.rotation = Quaternion.Euler(-90.0f, Random.Range(0.0f, 360.0f), 0.0f);
    }

    private void FixedUpdate()
    {
        if (playerRBs.Count > 0)
        {
            for (int i = 0; i < playerRBs.Count; i++)
            {
                playerRBs[i].AddForce(playerRBs[i].velocity * puddleFricction, ForceMode.Force);
            }
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerVehicle"))
        {
            PlayerVehicleScript playerScript = other.transform.parent.GetComponent<PlayerVehicleScript>();
            initMaxSpeed = playerScript.vehicleMaxSpeed;
            initMaxAngularSpeed = playerScript.vehicleMaxTorque;
            playerScript.vehicleMaxSpeed = initMaxSpeed * maxSpeedIncrease;
            playerScript.vehicleMaxTorque = initMaxAngularSpeed * maxSpeedIncrease;

            Rigidbody playerRB = playerScript.GetComponent<Rigidbody>();
            playerRB.angularDrag *= angularDragIncrease;
            playerRBs.Add(playerRB);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerVehicle"))
        {
            PlayerVehicleScript playerScript = other.transform.parent.GetComponent<PlayerVehicleScript>();
            playerScript.vehicleMaxSpeed = initMaxSpeed;
            playerScript.vehicleMaxTorque = initMaxAngularSpeed;

            Rigidbody playerRB = playerScript.GetComponent<Rigidbody>();
            playerRB.angularDrag = INIT_ANGULAR_DRAG;
            playerRBs.Remove(playerRB);
        }
    }
}
