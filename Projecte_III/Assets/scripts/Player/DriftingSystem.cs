using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriftingSystem : MonoBehaviour
{
    [SerializeField] float driftTorqueInc = 3.0f;
    [SerializeField] float driftDuration = 2;
    PlayerVehicleScript player;
    float driftTimer = 0;
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
                if (!driftLeft)
                {
                    player.vehicleRB.AddTorque(0, -player.vehicleTorque * driftTorqueInc, 0);
                    savedDir = player.vehicleRB.velocity;
                    player.vehicleRB.velocity += new Vector3(0, 5, 0);
                    driftRot = player.vehicleRB.rotation * new Quaternion(0, -0.5f * player.inputs.LeftFloat, 0, 1).normalized;
                    savedRot = player.vehicleRB.rotation;
                }
                else if (driftRot.y <= savedRot.y)
                    savedRot *= new Quaternion(0, -0.002f * player.inputs.RightFloat, 0, 1);

                driftLeft = true;

                savedDir += transform.TransformDirection(-0.4f * player.inputs.LeftFloat, 0, 0);
                savedRot *= new Quaternion(0, -0.015f * player.inputs.LeftFloat, 0, 1).normalized;

                player.vehicleRB.velocity = new Vector3(savedDir.x, player.vehicleRB.velocity.y, savedDir.z);
                player.vehicleRB.rotation = savedRot;

                if (driftRight)
                {
                    driftTimer = 1;
                    driftRight = false;
                }
                if (driftTimer > 0)
                {
                    driftTimer -= Time.deltaTime;
                    player.particleMat.color = Color.yellow;
                }
                else
                    player.particleMat.color = Color.red;
            }
            else if (player.inputs.Right && player.inputs.Drift)
            {
                if (!driftRight)
                {
                    player.vehicleRB.AddTorque(0, player.vehicleTorque * driftTorqueInc, 0);
                    savedDir = player.vehicleRB.velocity;
                    player.vehicleRB.velocity += new Vector3(0, 5, 0);
                    driftRot = player.vehicleRB.rotation * new Quaternion(0, 0.5f * player.inputs.RightFloat, 0, 1).normalized;
                    savedRot = player.vehicleRB.rotation;
                }
                else if (driftRot.y >= savedRot.y)
                    savedRot *= new Quaternion(0, 0.002f * player.inputs.RightFloat, 0, 1);

                driftRight = true;

                savedDir += transform.TransformDirection(0.4f * player.inputs.RightFloat, 0, 0);
                savedRot *= new Quaternion(0, 0.015f * player.inputs.RightFloat, 0, 1).normalized;

                player.vehicleRB.velocity = new Vector3(savedDir.x, player.vehicleRB.velocity.y, savedDir.z);
                player.vehicleRB.rotation = savedRot;

                if (driftLeft)
                {
                    driftTimer = 1;
                    driftLeft = false;
                }

                if (driftTimer > 0)
                {
                    driftTimer -= Time.deltaTime;
                    player.particleMat.color = Color.yellow;
                }
                else
                    player.particleMat.color = Color.red;
            }
            else if (driftTimer <= 0)
            {
                player.particleMat.color = player.defaultColorMat;
                player.vehicleAcceleration = 2;
                player.vehicleMaxSpeed = 35.5f;
                driftTimer = 1;
                StartCoroutine(WaitEndBoost());
            }
            else
            {
                player.particleMat.color = player.defaultColorMat;
                if (driftTimer != 1)
                    driftTimer = 1;
                driftLeft = false;
                driftRight = false;
            }
        }
        else if (driftTimer <= 0)
        {
            player.particleMat.color = player.defaultColorMat;
            player.vehicleAcceleration = 2;
            player.vehicleMaxSpeed = 35.5f;
            driftTimer = 1;
            StartCoroutine(WaitEndBoost());
        }
        else
        {
            player.particleMat.color = player.defaultColorMat;
            if (driftTimer != 1)
                driftTimer = 1;
            driftLeft = false;
            driftRight = false;
        }
    }

    IEnumerator WaitEndBoost()
    {
        yield return new WaitForSeconds(driftDuration);
        reduceSpeed = true;
    }
}
