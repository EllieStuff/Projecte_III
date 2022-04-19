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
    float initMaxSpeed, initMaxAngularSpeed;

    void Start()
    {
        transform.rotation = Quaternion.Euler(-90.0f, Random.Range(0.0f, 360.0f), 0.0f);
    }

    private void FixedUpdate()
    {
        if(playersRB.Count > 0)
        {
            for (int i = 0; i < playersRB.Count; i++)
            {
                playersRB[i].AddForce(playersRB[i].velocity * puddleFricction, ForceMode.Force);
            }
        }

        //if (playersData.Count > 0)
        //{
        //    for (int i = 0; i < playersData.Count; i++)
        //    {
        //        /// Funciona pero es una mica meh
        //        //playersData[i].rb.AddForce(playersData[i].rb.velocity * puddleFricction, ForceMode.Force);

        //        PlayerData tmpPlayer = playersData[i];
        //        if (tmpPlayer.lastPos != Vector3.zero)
        //        {
        //            tmpPlayer.currDir = (tmpPlayer.rb.position - tmpPlayer.lastPos).normalized * 8;
        //            Debug.Log("CurrDir: " + tmpPlayer.currDir);
        //            Debug.Log("CurrVel: " + tmpPlayer.rb.velocity);
        //        }
        //        tmpPlayer.lastPos = tmpPlayer.rb.position;
        //        if (tmpPlayer.currDir != Vector3.zero)
        //        {
        //            playersData[i].rb.AddForce(tmpPlayer.currDir * puddleFricction, ForceMode.Force);
        //        }
        //        playersData[i] = tmpPlayer;
        //    }
        //}

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
            playersRB.Add(playerRB);
            //playersData.Add(new PlayerData(playerRB));
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
            playersRB.Remove(playerRB);
            //int playerIdx = playersData.FindIndex(_players => _players.rb == playerRB);
            //playersData.RemoveAt(playerIdx);
        }
    }
}
