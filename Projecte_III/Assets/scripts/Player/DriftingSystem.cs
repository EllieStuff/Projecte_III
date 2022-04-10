using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriftingSystem : MonoBehaviour
{
    [SerializeField] float driftTorqueInc = 3.0f;
    [SerializeField] float driftDuration = 2;
    [SerializeField] float driftForce = 0.015f;
    PlayerVehicleScript player;
    bool driftLeft;
    bool driftRight;
    Vector3 savedDir;
    Quaternion savedRot;
    Quaternion driftRot;
    bool reduceSpeed;

    private void Start()
    {
        player = GetComponent<PlayerVehicleScript>();
    }

    void Update()
    {

        if (reduceSpeed && player.vehicleMaxSpeed > player.savedMaxSpeed)
        {
            player.vehicleMaxSpeed -= Time.deltaTime * 10;
        }
        else if (reduceSpeed && player.vehicleMaxSpeed <= player.savedMaxSpeed)
        {
            reduceSpeed = false;
            player.vehicleMaxSpeed = player.savedMaxSpeed;
            player.vehicleAcceleration = player.savedAcceleration;
        }

        if (player.vehicleRB.velocity.magnitude >= player.minDriftSpeed && player.inputs.Forward && !player.inputs.Backward)
        {
            if (player.inputs.Left && player.inputs.Drift)
            {
                player.vehicleMaxSpeed = player.savedMaxSpeed + 0.2f;
                if (!driftLeft)
                {
                    player.vehicleRB.AddTorque(0, -player.vehicleTorque * driftTorqueInc, 0);
                    savedDir = player.vehicleRB.velocity;
                    //player.vehicleRB.velocity += new Vector3(0, 5, 0);
                    driftRot = player.vehicleRB.rotation * new Quaternion(0, -0.5f * player.inputs.LeftFloat, 0, 1).normalized;
                    savedRot = player.vehicleRB.rotation;
                }
                else if (driftRot.y <= savedRot.y)
                    savedRot *= new Quaternion(0, -0.002f * player.inputs.RightFloat, 0, 1);

                driftLeft = true;

                savedDir += transform.TransformDirection(-0.4f * player.inputs.LeftFloat, 0, 0);
                savedRot *= new Quaternion(0, -driftForce * player.inputs.LeftFloat, 0, 1).normalized;

                player.vehicleRB.velocity = new Vector3(savedDir.x, player.vehicleRB.velocity.y, savedDir.z);
                player.vehicleRB.rotation = savedRot;
            }
            else if (player.inputs.Right && player.inputs.Drift)
            {
                player.vehicleMaxSpeed = player.savedMaxSpeed + 5f;
                if (!driftRight)
                {
                    player.vehicleRB.AddTorque(0, player.vehicleTorque * driftTorqueInc, 0);
                    savedDir = player.vehicleRB.velocity;
                    //player.vehicleRB.velocity += new Vector3(0, 5, 0);
                    driftRot = player.vehicleRB.rotation * new Quaternion(0, 0.5f * player.inputs.RightFloat, 0, 1).normalized;
                    savedRot = player.vehicleRB.rotation;
                }
                else if (driftRot.y >= savedRot.y)
                    savedRot *= new Quaternion(0, 0.002f * player.inputs.RightFloat, 0, 1);

                driftRight = true;

                savedDir += transform.TransformDirection(0.4f * player.inputs.RightFloat, 0, 0);
                savedRot *= new Quaternion(0, driftForce * player.inputs.RightFloat, 0, 1).normalized;

                player.vehicleRB.velocity = new Vector3(savedDir.x, player.vehicleRB.velocity.y, savedDir.z);
                player.vehicleRB.rotation = savedRot;

                if (driftLeft)
                    driftLeft = false;
            }
            else
            {
                player.vehicleMaxSpeed = player.savedMaxSpeed;
                player.vehicleAcceleration = player.savedAcceleration;
                driftLeft = false;
                driftRight = false;
            }
        }
        else
        {
            player.vehicleMaxSpeed = player.savedMaxSpeed;
            player.vehicleAcceleration = player.savedAcceleration;
            driftLeft = false;
            driftRight = false;
        }
    }
}
