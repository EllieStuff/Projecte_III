using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleTriggerAndCollisionEvents : MonoBehaviour
{
    private PlayerVehicleScript player;
    private Transform centerRespawn;
    public Vector3 respawnPosition, respawnRotation, respawnVelocity;
    bool paintingChecked = false, oilChecked = false;
    [SerializeField] float boostPadDuration;
    private bool reduceSpeed;
    public float boostPadMultiplier;
    private Transform outTransform;
    private Rigidbody outVehicleRB;

    private void Start()
    {
        centerRespawn = GameObject.Find("Main Camera").transform;
        player = GetComponent<PlayerVehicleScript>();
        respawnPosition = new Vector3(0, 0, 0);
        respawnRotation = new Vector3(0, 0, 0);
        respawnVelocity = new Vector3(0, 0, 0);
        paintingChecked = oilChecked = false;
    }

    private void Update()
    {
        respawnPosition = new Vector3(centerRespawn.position.x, centerRespawn.position.y - 25, centerRespawn.position.z);
        respawnPosition += centerRespawn.TransformDirection(new Vector3(0, 0, 30)) + new Vector3(0, 25, 0);
        respawnRotation = centerRespawn.rotation.eulerAngles;
        if (player.vehicleReversed)
        {
            //Vehicle recover zone
            player.timerReversed += Time.deltaTime;
            if (DeathScript.DeathByFlipping(player.timerReversed, transform, player.vehicleRB, respawnPosition, respawnRotation, respawnVelocity, out outTransform, out outVehicleRB))
            {

                transform.position = outTransform.position;

                transform.rotation = outTransform.rotation;

                player.vehicleRB.velocity = outVehicleRB.velocity;
            }
            //_________________________________________________________________________________________________________________________________________________________________
        }
        else if(DeathScript.DeathByFalling(false, transform, player.vehicleRB, respawnPosition, respawnRotation, respawnVelocity, out outTransform, out outVehicleRB))
        {
            transform.position = outTransform.position;
            transform.rotation = outTransform.rotation;
            player.vehicleRB.velocity = outVehicleRB.velocity;
            player.lifes--;
        }

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
    }

    void OnCollisionStay(Collision other)
    {
        //------Player Death------
        player.vehicleReversed = (other.gameObject.tag.Equals("ground"));
        //------------------------
    }

    IEnumerator WaitEndBoost()
    {
        yield return new WaitForSeconds(boostPadDuration);
        reduceSpeed = true;
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Boost Pad"))
        {
            float angle = Vector3.Angle(transform.forward, other.transform.forward);
            angle *= Mathf.Deg2Rad;
            angle = Mathf.Cos(angle);
            player.vehicleAcceleration = 2;
            player.vehicleMaxSpeed = boostPadMultiplier * angle;
            if (player.vehicleMaxSpeed < player.savedMaxSpeed)
                player.vehicleMaxSpeed = player.savedMaxSpeed;
        }

        /*if (other.CompareTag("Water"))
        {
            vehicleRB.AddForce(other.GetComponent<WaterStreamColliderScript>().Stream, ForceMode.Force);
            onWater = true;
        }*/

        if (!paintingChecked && other.CompareTag("Painting"))
        {
            paintingChecked = true;
            if (player.vehicleRB.velocity.magnitude > 1.0f)
            {
                AddFriction(1000, 2.0f);
            }
        }
        if (!oilChecked && other.CompareTag("Oil"))
        {
            oilChecked = true;
            if (player.vehicleRB.velocity.magnitude > 1.0f)
            {
                AddFriction(-1200, 0.7f);
            }
        }

        //Terrain
        if (other.CompareTag("Sand") && player.touchingGround)
            OnSand(other);

    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Sand"))
            StartCoroutine(WaitEndBoost());

        if (other.CompareTag("Painting") || other.CompareTag("Oil"))
        {
            ResetFriction();
        }

        /*if(other.CompareTag("Water"))
        {
            onWater = false;
        }*/

        //Terrain
        if (other.CompareTag("Sand") || !player.touchingGround)
        {
            //if (vehicleMaxSpeed == savedMaxSpeed / sandVelocityMultiplier)
            //{
            player.vehicleMaxSpeed = player.savedMaxSpeed;
            player.vehicleAcceleration = player.savedAcceleration;
            //}
        }
    }

    void OnCollisionExit(Collision other)
    {
        player.vehicleReversed = false;
        player.timerReversed = 0;
    }

    void ResetFriction()
    {
        player.vehicleRB.angularDrag = player.savedAngularDrag;
    }

    void AddFriction(float _frictionForce, float _dragInc)
    {
        Vector3 velFrictionVec = -player.vehicleRB.velocity.normalized * _frictionForce * player.vehicleRB.velocity.magnitude;
        player.vehicleRB.AddForce(velFrictionVec, ForceMode.Force);
        player.vehicleRB.angularDrag = player.savedAngularDrag * _dragInc;
    }

    void OnSand(Collider other)
    {
        if (player.vehicleMaxSpeed <= player.savedMaxSpeed && player.vehicleMaxSpeed > player.savedMaxSpeed / player.sandVelocityMultiplier)
        {
            player.vehicleMaxSpeed = player.savedMaxSpeed / player.sandVelocityMultiplier;
            player.vehicleAcceleration = player.savedAcceleration / player.sandAccelerationMultiplier;
        }
    }

    internal IEnumerator LerpVehicleMaxSpeed(float _targetValue, float _lerpTime)
    {
        float lerpTimer = 0;
        while (player.vehicleMaxSpeed != _targetValue)
        {
            yield return new WaitForEndOfFrame();
            lerpTimer += Time.deltaTime;
            player.vehicleMaxSpeed = Mathf.Lerp(player.vehicleMaxSpeed, _targetValue, lerpTimer / _lerpTime);
        }
    }
}
