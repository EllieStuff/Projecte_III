using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerVehicleScript : MonoBehaviour
{
    [SerializeField] Vector3 centerOfMass = new Vector3(0.0f, -0.7f, 0.0f);
    [SerializeField] float boostPadDuration;

    private Material chasisMat;
    private Vector3 savedVelocity;
    private float savedMaxVelocity;
    private bool reduceSpeed;
    
    QuadControls controls;

    public Rigidbody vehicleRB;
    public float vehicleAcceleration;
    public float vehicleTorque;
    public float vehicleMaxSpeed;
    public float vehicleMaxTorque;
    public WheelCollider[] wheelCollider;
    public GameObject[] wheels;
    public int lifeVehicle;
    public bool touchingGround;
    public bool vehicleReversed;
    public float minDriftSpeed;
    public Vector3 respawnPosition, respawnRotation, respawnVelocity;
    public float boostPadMultiplier;
    private float chasisElevationTimer;
    [SerializeField] private bool chasisElevation;

    // Start is called before the first frame update
    void Start()
    {
        controls = new QuadControls();
        controls.Enable();

        this.GetComponent<AudioSource>().enabled = false;
        chasisMat = new Material(this.transform.GetChild(0).GetComponent<MeshRenderer>().material);
        this.transform.GetChild(0).GetComponent<MeshRenderer>().material = chasisMat;
        chasisMat.color = Color.red;
        Physics.gravity = new Vector3(0, -9.8f * 2, 0);
        vehicleRB = this.GetComponent<Rigidbody>();
        vehicleRB.centerOfMass = centerOfMass;
        savedMaxVelocity = vehicleMaxSpeed;
    }

    private void Awake()
    {
        this.transform.name = "Player";
        respawnPosition = new Vector3(0, 0, 0);
        respawnRotation = new Vector3(0, 0, 0);
        respawnVelocity = new Vector3(0, 0, 0);
    }

    void Update()
    {
        touchingGround = false;

        //HERE WE SET THE POSITION AND ROTATION FROM THE WHEELS RENDERERS
        for (int i = 0; i < wheelCollider.Length; i++)
        {
            if (wheelCollider[i].GetGroundHit(out var touchingGroundV))
            {
                touchingGround = true;
            }
            wheelCollider[i].GetWorldPose(out var pos, out var rot);
            wheels[i].transform.position = pos;
            wheels[i].transform.rotation = rot;
        }
        //_______________________________________________________________
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        vehicleMovement();
    }

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag.Equals("ground"))
            vehicleReversed = true;
    }

    void vehicleMovement()
    {
        var locVel = transform.InverseTransformDirection(vehicleRB.velocity);
        if (touchingGround && lifeVehicle > 0)
        {
            //MAIN MOVEMENT KEYS______________________________________________________________________________________________________________________
            //FORWARD
            if (controls.Quad.Forward.ReadValue<float>() > 0 && controls.Quad.Backward.ReadValue<float>() == 0 && controls.Quad.Right.ReadValue<float>() == 0 && controls.Quad.Left.ReadValue<float>() == 0 && transform.rotation.ToEulerAngles().x > -1)
            {
                if (vehicleRB.velocity.y <= vehicleMaxSpeed / 2 && controls.Quad.Drift.ReadValue<float>() == 0)
                    vehicleRB.velocity += transform.TransformDirection(new Vector3(0, 0, vehicleAcceleration));
            }
            else if (controls.Quad.Forward.ReadValue<float>() > 0 && controls.Quad.Backward.ReadValue<float>() == 0 && controls.Quad.Right.ReadValue<float>() == 0 && controls.Quad.Left.ReadValue<float>() > 0)
            {
                if (vehicleRB.velocity.y <= vehicleMaxSpeed / 2 && controls.Quad.Drift.ReadValue<float>() == 0)
                    vehicleRB.velocity += transform.TransformDirection(new Vector3(0, 0, vehicleAcceleration));

                vehicleRB.AddTorque(new Vector3(0, -vehicleTorque, 0));
            }
            else if (controls.Quad.Forward.ReadValue<float>() > 0 && controls.Quad.Backward.ReadValue<float>() == 0 && controls.Quad.Right.ReadValue<float>() > 0 && controls.Quad.Left.ReadValue<float>() == 0)
            {
                if (vehicleRB.velocity.y <= vehicleMaxSpeed / 2 && controls.Quad.Drift.ReadValue<float>() == 0)
                    vehicleRB.velocity += transform.TransformDirection(new Vector3(0, 0, vehicleAcceleration));

                vehicleRB.AddTorque(new Vector3(0, vehicleTorque, 0));
            }

            //LEFT
            else if (controls.Quad.Left.ReadValue<float>() > 0 && controls.Quad.Forward.ReadValue<float>() == 0 && controls.Quad.Backward.ReadValue<float>() == 0 && controls.Quad.Right.ReadValue<float>() == 0)
            {
                vehicleRB.AddTorque(new Vector3(0, -vehicleTorque, 0));
            }
            //RIGHT
            else if (controls.Quad.Right.ReadValue<float>() > 0 && controls.Quad.Forward.ReadValue<float>() == 0 && controls.Quad.Backward.ReadValue<float>() == 0 && controls.Quad.Left.ReadValue<float>() == 0)
            {
                vehicleRB.AddTorque(new Vector3(0, vehicleTorque, 0));
            }

            //BACKWARDS
            else if (controls.Quad.Backward.ReadValue<float>() > 0 && controls.Quad.Left.ReadValue<float>() == 0 && controls.Quad.Right.ReadValue<float>() == 0 && controls.Quad.Forward.ReadValue<float>() == 0)
            {
                if (vehicleRB.velocity.y > -minDriftSpeed / 2 && vehicleRB.velocity.y <= vehicleMaxSpeed / 2)
                    vehicleRB.velocity += transform.TransformDirection(new Vector3(0, 0, -vehicleAcceleration));
                else if (vehicleRB.velocity.y <= vehicleMaxSpeed / 2)
                    vehicleRB.velocity += transform.TransformDirection(new Vector3(0, 0, -vehicleAcceleration / 10));
            }
            else if (controls.Quad.Backward.ReadValue<float>() > 0 && controls.Quad.Left.ReadValue<float>() > 0 && controls.Quad.Right.ReadValue<float>() == 0 && controls.Quad.Forward.ReadValue<float>() == 0)
            {
                if (vehicleRB.velocity.y > -minDriftSpeed / 2 && vehicleRB.velocity.y <= vehicleMaxSpeed / 2)
                    vehicleRB.velocity += transform.TransformDirection(new Vector3(0, 0, -vehicleAcceleration));
                else if (vehicleRB.velocity.y <= vehicleMaxSpeed / 2)
                    vehicleRB.velocity += transform.TransformDirection(new Vector3(0, 0, -vehicleAcceleration / 10));

                vehicleRB.AddTorque(new Vector3(0, vehicleTorque, 0));
            }
            else if (controls.Quad.Backward.ReadValue<float>() > 0 && controls.Quad.Left.ReadValue<float>() == 0 && controls.Quad.Right.ReadValue<float>() > 0 && controls.Quad.Forward.ReadValue<float>() == 0)
            {
                if (vehicleRB.velocity.y > -minDriftSpeed / 2 && vehicleRB.velocity.y <= vehicleMaxSpeed / 2)
                    vehicleRB.velocity += transform.TransformDirection(new Vector3(0, 0, -vehicleAcceleration));
                else if (vehicleRB.velocity.y <= vehicleMaxSpeed / 2)
                    vehicleRB.velocity += transform.TransformDirection(new Vector3(0, 0, -vehicleAcceleration / 10));

                vehicleRB.AddTorque(new Vector3(0, -vehicleTorque, 0));
            }
            //MAIN MOVEMENT KEYS______________________________________________________________________________________________________________________

            //SPEED REGULATION FUNCTION
            SpeedRegulation();

            //DRIFT FUNCTION
            DriftFunction();

            //CHASIS ELEVATION FUNCTION
            ChasisElevationFunction();


            savedVelocity = vehicleRB.velocity;
        }
        else if (vehicleReversed && lifeVehicle > 0)
        {
            //WHEN THE VEHICLE IS REVERSED YOU CAN ROTATE THE QUAD LEFT AND RIGHT UNTIL THE VEHICLE STANDS UP
            VehicleRecoverFunction();
        }
        else
        {
            //FALL FUNCTION
            FallFunction();
        }

        if(reduceSpeed && vehicleMaxSpeed > savedMaxVelocity)
        {
            vehicleMaxSpeed -= Time.deltaTime * 10;
        }
        else if(reduceSpeed && vehicleMaxSpeed <= savedMaxVelocity)
        {
            reduceSpeed = false;
            vehicleMaxSpeed = savedMaxVelocity;
        }
        Debug.Log(vehicleMaxSpeed);

        //VEHICLE SOUND PITCH SYSTEM
        VehicleSoundPitchFunction();
    }

    void VehicleRecoverFunction()
    {
        //LEFT
        if (controls.Quad.Left.ReadValue<float>() > 0 && controls.Quad.Forward.ReadValue<float>() == 0 && controls.Quad.Backward.ReadValue<float>() == 0 && controls.Quad.Right.ReadValue<float>() == 0)
        {
            vehicleRB.AddTorque(new Vector3(0, 0, -vehicleTorque * 10));
        }
        //RIGHT
        else if (controls.Quad.Right.ReadValue<float>() > 0 && controls.Quad.Forward.ReadValue<float>() == 0 && controls.Quad.Backward.ReadValue<float>() == 0 && controls.Quad.Left.ReadValue<float>() == 0)
        {
            vehicleRB.AddTorque(new Vector3(0, 0, vehicleTorque * 10));
        }
    }

    void ChasisElevationFunction()
    {
        Transform chasisTransform = this.transform.GetChild(0);

        if (controls.Quad.ChasisElevation.ReadValue<float>() > 0 && !chasisElevation && chasisTransform.localPosition.y <= 0)
        {
            chasisElevation = true;
            chasisElevationTimer = 2;
        }

        if (chasisElevation)
        {
            if (chasisElevationTimer > 0)
                chasisElevationTimer -= Time.deltaTime;
            else
                chasisElevation = false;

            if (chasisTransform.localPosition.y <= 2)
                chasisTransform.localPosition = new Vector3(chasisTransform.localPosition.x, chasisTransform.localPosition.y + 0.05f, chasisTransform.localPosition.z);
        }
        else
        {
            if (chasisTransform.localPosition.y > 0)
                chasisTransform.localPosition = new Vector3(chasisTransform.localPosition.x, chasisTransform.localPosition.y - 0.05f, chasisTransform.localPosition.z);
        }
    }

    void DriftFunction()
    {
        if (vehicleRB.velocity.magnitude >= minDriftSpeed)
        {
            if (controls.Quad.Left.ReadValue<float>() > 0 && controls.Quad.Drift.ReadValue<float>() > 0)
            {
                vehicleRB.AddTorque(new Vector3(0, -vehicleTorque * 5, 0));
            }
            else if (controls.Quad.Right.ReadValue<float>() > 0 && controls.Quad.Drift.ReadValue<float>() > 0)
            {
                vehicleRB.AddTorque(new Vector3(0, vehicleTorque * 5, 0));
            }
        }
    }

    void VehicleSoundPitchFunction()
    {
        if ((vehicleRB.velocity.magnitude > 1 || vehicleRB.velocity.magnitude < -1) && !this.GetComponent<AudioSource>().enabled && lifeVehicle > 0)
        {
            this.GetComponent<AudioSource>().enabled = true;
        }
        else if ((vehicleRB.velocity.magnitude <= 1 && vehicleRB.velocity.magnitude >= -1) && this.GetComponent<AudioSource>().enabled && lifeVehicle > 0)
            this.GetComponent<AudioSource>().enabled = false;

        if (this.GetComponent<AudioSource>().enabled)
        {
            this.GetComponent<AudioSource>().pitch = (vehicleRB.velocity.magnitude * 1) / vehicleMaxSpeed;
        }
    }

    void FallFunction()
    {
        if (savedVelocity.x > 0)
            savedVelocity -= new Vector3(0.1f + Mathf.Clamp(vehicleRB.velocity.y, 0, 0.2f), 0, 0);
        else if (savedVelocity.x < 0)
            savedVelocity += new Vector3(0.1f + Mathf.Clamp(vehicleRB.velocity.y, 0, 0.2f), 0, 0);
        if (savedVelocity.z > 0)
            savedVelocity -= new Vector3(0, 0, 0.1f + Mathf.Clamp(vehicleRB.velocity.y, 0, 0.2f));
        else if (savedVelocity.z < 0)
            savedVelocity += new Vector3(0, 0, 0.1f + Mathf.Clamp(vehicleRB.velocity.y, 0, 0.2f));

        if (vehicleRB.velocity.y >= 0)
            vehicleRB.velocity = new Vector3(savedVelocity.x, vehicleRB.velocity.y, savedVelocity.z);
        else
            vehicleRB.velocity = new Vector3(savedVelocity.x, vehicleRB.velocity.y - 0.5f, savedVelocity.z);
    }

    void SpeedRegulation()
    {
        if (controls.Quad.Left.ReadValue<float>() == 0 && controls.Quad.Right.ReadValue<float>() == 0)
            vehicleRB.angularVelocity = new Vector3(vehicleRB.angularVelocity.x, 0, vehicleRB.angularVelocity.z);

        if (vehicleRB.angularVelocity.y > vehicleMaxTorque)
        {
            vehicleRB.angularVelocity = new Vector3(vehicleRB.angularVelocity.x, vehicleMaxTorque, vehicleRB.angularVelocity.z);
        }
        else if (vehicleRB.angularVelocity.y < -vehicleMaxTorque)
        {
            vehicleRB.angularVelocity = new Vector3(vehicleRB.angularVelocity.x, -vehicleMaxTorque, vehicleRB.angularVelocity.z);
        }
        if (vehicleRB.velocity.z > vehicleMaxSpeed || vehicleRB.velocity.x > vehicleMaxSpeed)
        {
            if (vehicleRB.velocity.x > vehicleMaxSpeed && vehicleRB.velocity.z < vehicleMaxSpeed)
            {
                vehicleRB.velocity = transform.TransformDirection(new Vector3(0, 0, vehicleMaxSpeed));
                if (controls.Quad.Backward.ReadValue<float>() > 0 && vehicleRB.velocity.x < 0)
                    vehicleRB.velocity = transform.TransformDirection(new Vector3(0, 0, -vehicleMaxSpeed));
            }
            if (vehicleRB.velocity.z > vehicleMaxSpeed)
                vehicleRB.velocity = transform.TransformDirection(new Vector3(0, 0, vehicleMaxSpeed));
            if (controls.Quad.Backward.ReadValue<float>() > 0 && vehicleRB.velocity.z < 0)
                vehicleRB.velocity = transform.TransformDirection(new Vector3(0, 0, -vehicleMaxSpeed));
        }
        else if (vehicleRB.velocity.z < -vehicleMaxSpeed || vehicleRB.velocity.x < -vehicleMaxSpeed)
        {
            if (controls.Quad.Backward.ReadValue<float>() == 0)
                vehicleRB.velocity = transform.TransformDirection(new Vector3(0, 0, vehicleMaxSpeed));
            else
            {
                vehicleRB.velocity = transform.TransformDirection(new Vector3(0, 0, -vehicleMaxSpeed));
                if (vehicleRB.velocity.z < 0)
                    vehicleRB.velocity = transform.TransformDirection(new Vector3(0, 0, -vehicleMaxSpeed));
            }
        }
    }

    IEnumerator WaitEndBoost()
    {
        yield return new WaitForSeconds(boostPadDuration);
        Debug.Log("AAAAAAAAAAAAAAA");
        reduceSpeed = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Boost Pad"))
        {
            float angle = Vector3.Angle(this.transform.forward, other.transform.forward);
            angle *= Mathf.Deg2Rad;
            angle = Mathf.Cos(angle);
            vehicleMaxSpeed = boostPadMultiplier * angle;
            if (vehicleMaxSpeed < savedMaxVelocity)
                vehicleMaxSpeed = savedMaxVelocity;
            //Debug.Log(vehicleMaxSpeed);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //Debug.Log(vehicleMaxSpeed);
        //vehicleMaxSpeed = savedMaxVelocity;
        StartCoroutine(WaitEndBoost());
    }

    void OnCollisionExit(Collision other)
    {
        vehicleReversed = false;
    }
}
