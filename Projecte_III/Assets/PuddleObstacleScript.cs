using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuddleObstacleScript : MonoBehaviour
{
    const float INIT_ANGULAR_DRAG = 5.0f;

    [SerializeField] float puddleFricction = 9999.9f;
    [SerializeField] float angularDragIncrease = 0.01f;
    [SerializeField] float maxSpeedIncrease = 999.9f;
    
    //class PlayerData
    //{
    //    public Rigidbody rb;
    //    public Vector3 lastPos = Vector3.zero;
    //    public Vector3 currDir = Vector3.zero;
    //    public PlayerData(Rigidbody _rb) { rb = _rb; }
    //}
    //List<PlayerData> playersData = new List<PlayerData>();
    List<Rigidbody> playersRB = new List<Rigidbody>();
    //float initMaxSpeed, initMaxAngularSpeed;

    void Start()
    {
        transform.rotation = Quaternion.Euler(-90.0f, Random.Range(0.0f, 360.0f), 0.0f);
    }

    private void FixedUpdate()
    {
        //if(playersRB.Count > 0)
        //{
        //    for (int i = 0; i < playersRB.Count; i++)
        //    {
        //        playersRB[i].AddForce(playersRB[i].velocity * puddleFricction, ForceMode.Force);
        //    }
        //}

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerVehicle"))
        {
            PlayerVehicleScript playerScript = other.transform.parent.GetComponent<PlayerVehicleScript>();
            playerScript.speedIncrementEnabled = true;
            //initMaxSpeed = playerScript.vehicleMaxSpeed;
            //initMaxAngularSpeed = playerScript.vehicleMaxTorque;
            playerScript.vehicleMaxSpeed = playerScript.savedMaxSpeed * maxSpeedIncrease;
            playerScript.vehicleMaxTorque = playerScript.savedVehicleTorque * maxSpeedIncrease;

            Rigidbody playerRB = playerScript.GetComponent<Rigidbody>();
            playerRB.angularDrag *= angularDragIncrease;
            //playersRB.Add(playerRB);
            //playersData.Add(new PlayerData(playerRB));
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerVehicle"))
        {
            PlayerVehicleScript playerScript = other.transform.parent.GetComponent<PlayerVehicleScript>();
            playerScript.vehicleMaxSpeed = playerScript.savedMaxSpeed;
            playerScript.vehicleMaxTorque = playerScript.savedVehicleTorque;
            playerScript.speedIncrementEnabled = false;

            Rigidbody playerRB = playerScript.GetComponent<Rigidbody>();
            playerRB.angularDrag = playerScript.savedAngularDrag;
            //playersRB.Remove(playerRB);
            //int playerIdx = playersData.FindIndex(_players => _players.rb == playerRB);
            //playersData.RemoveAt(playerIdx);
        }
    }
}
