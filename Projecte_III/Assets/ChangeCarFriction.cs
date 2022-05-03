using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCarFriction : MonoBehaviour
{
    [SerializeField] float angularDrag = 3;
    [SerializeField] float maxSpeed;

    //List<PlayerVehicleScript> collidingPlayers = new List<PlayerVehicleScript>();
    //List<Rigidbody> collidingPlayersRB = new List<Rigidbody>();

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
        if (other.CompareTag("PlayerVehicle"))
        {
            // ToDo: afegir canvi de velocitat en player
            PlayerVehicleScript currPlayer = other.GetComponentInParent<PlayerVehicleScript>();
            currPlayer.speedIncrementEnabled = true;
            currPlayer.vehicleMaxSpeed = maxSpeed /** timeMultiplier*/;
            //collidingPlayers.Add(currPlayer);
            Rigidbody currPlayerRB = other.GetComponentInParent<Rigidbody>();
            currPlayerRB.angularDrag = angularDrag;
            //collidingPlayersRB.Add(currPlayerRB);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerVehicle"))
        {
            PlayerVehicleScript currPlayer = other.GetComponentInParent<PlayerVehicleScript>();
            currPlayer.vehicleMaxSpeed = currPlayer.savedMaxSpeed;
            currPlayer.speedIncrementEnabled = false;
            Rigidbody currPlayerRB = other.GetComponentInParent<Rigidbody>();
            currPlayerRB.angularDrag = currPlayer.savedAngularDrag;
            //collidingPlayers.Remove(currPlayer);
            //collidingPlayersRB.Remove(currPlayerRB);
        }
    }

}
