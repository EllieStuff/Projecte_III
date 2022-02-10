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
    [SerializeField] float alaDeltaDuration;
    [SerializeField] float alaDeltaTimer;
    [SerializeField] float driftTorqueInc = 3.0f;

    private Material chasisMat;
    private Vector3 savedVelocity;
    private float timerReversed;
    private float savedMaxSpeed;
    private bool reduceSpeed;
    
    QuadControls controls;

    public Rigidbody vehicleRB;
    private float vehicleAcceleration;
    public float vehicleTorque;
    public float vehicleMaxSpeed;
    public float vehicleMaxTorque;
    private WheelCollider[] wheelCollider;
    private GameObject wheels;
    public int lifeVehicle;
    public bool touchingGround;
    public bool vehicleReversed;
    public float minDriftSpeed;
    public Vector3 respawnPosition, respawnRotation, respawnVelocity;
    public float boostPadMultiplier;
    private float chasisElevationTimer;
    [SerializeField] private bool chasisElevation;
    private bool alaDelta;

    internal bool buildingScene;
    internal List<string> listOfModifiers;
    bool hasFloater = false;

    // Start is called before the first frame update
    void Start()
    {
        vehicleAcceleration = 2;

        wheelCollider = new WheelCollider[4];

        Transform _wheels = transform.GetChild(1);

        for (int i = 0; i < wheelCollider.Length; i++)
        {
            wheelCollider[i] = _wheels.GetChild(i).GetComponent<WheelCollider>();
        }

        alaDeltaDuration = 1;
        alaDeltaTimer = 1;
        controls = new QuadControls();
        controls.Enable();

        GetComponent<AudioSource>().enabled = false;
        chasisMat = new Material(transform.GetChild(0).GetComponent<MeshRenderer>().material);
        transform.GetChild(0).GetComponent<MeshRenderer>().material = chasisMat;
        chasisMat.color = Color.red;
        Physics.gravity = new Vector3(0, -9.8f * 2, 0);
        vehicleRB = GetComponent<Rigidbody>();
        vehicleRB.centerOfMass = centerOfMass;
        savedMaxSpeed = vehicleMaxSpeed;

        wheels = transform.parent.GetChild(1).gameObject;
    }

    private void Awake()
    {
        transform.name = "Player";
        respawnPosition = new Vector3(0, 0, 0);
        respawnRotation = new Vector3(0, 0, 0);
        respawnVelocity = new Vector3(0, 0, 0);

        buildingScene = SceneManager.GetActiveScene().name == "Building Scene";
    }

    internal void Init()
    {
        if (!hasFloater)
        {
            Physics.IgnoreLayerCollision(3, 4, true);
        }

    }

    void Update()
    {
        touchingGround = false;

        //HERE WE SET THE POSITION AND ROTATION FROM THE WHEELS RENDERERS

        if(!buildingScene)
        {
            for (int i = 0; i < wheelCollider.Length; i++)
            {
                if (wheelCollider[i].GetGroundHit(out var touchingGroundV))
                {
                    touchingGround = true;
                }
                wheels.transform.localPosition = transform.localPosition;
                wheels.transform.localRotation = transform.localRotation;
                //FALLDEATH CHECK
                if(!alaDelta)
                    checkFallDeath();
                //RESETTING ALADELTA VARIABLES
                if(alaDeltaTimer <= alaDeltaDuration - 0.8f)
                {
                    alaDeltaTimer = alaDeltaDuration;
                    alaDelta = false;
                }
            }
        }

        if(touchingGround && vehicleRB.constraints != RigidbodyConstraints.None)

        {

            vehicleRB.constraints = RigidbodyConstraints.None;

        }

        transform.parent.GetChild(2).localPosition = transform.localPosition;
        //_______________________________________________________________

        if (Input.GetKeyDown(KeyCode.F))
        {
            //Debug.LogWarning("quitar en vFinal");
            Physics.IgnoreLayerCollision(3, 4, hasFloater);
            hasFloater = !hasFloater;
        }

    }

    public void HideVoidModifier()

    {

        for (int i = 0; i < transform.parent.GetChild(2).childCount; i++)

        {

            GameObject child = transform.parent.GetChild(2).GetChild(i).gameObject;

            if (child.transform.childCount <= 0)

            {

                child.SetActive(false);

            }

        }

    }

    public void SetWheels()

    {
        wheels = gameObject.transform.parent.GetChild(1).gameObject;
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
            if(controls.Quad.Forward.ReadValue<float>() > 0 && controls.Quad.Backward.ReadValue<float>() == 0 && transform.rotation.ToEulerAngles().x > -1)
            {
                if (controls.Quad.Right.ReadValue<float>() == 0 && controls.Quad.Left.ReadValue<float>() == 0)
                {
                    if (vehicleRB.velocity.y <= vehicleMaxSpeed / 2 && controls.Quad.Drift.ReadValue<float>() == 0)
                        vehicleRB.velocity += transform.TransformDirection(new Vector3(0, 0, vehicleAcceleration));
                }
                else if (controls.Quad.Right.ReadValue<float>() == 0 && controls.Quad.Left.ReadValue<float>() > 0)
                {
                    if (vehicleRB.velocity.y <= vehicleMaxSpeed / 2 && controls.Quad.Drift.ReadValue<float>() == 0)
                        vehicleRB.velocity += transform.TransformDirection(new Vector3(0, 0, vehicleAcceleration));

                    vehicleRB.AddTorque(new Vector3(0, -vehicleTorque, 0));
                }
                else if (controls.Quad.Right.ReadValue<float>() > 0 && controls.Quad.Left.ReadValue<float>() == 0)
                {
                    if (vehicleRB.velocity.y <= vehicleMaxSpeed / 2 && controls.Quad.Drift.ReadValue<float>() == 0)
                        vehicleRB.velocity += transform.TransformDirection(new Vector3(0, 0, vehicleAcceleration));

                    vehicleRB.AddTorque(new Vector3(0, vehicleTorque, 0));
                }
            }
            //LEFT OR RIGHT
            if (controls.Quad.Forward.ReadValue<float>() == 0 && controls.Quad.Backward.ReadValue<float>() == 0)
            {
                //LEFT
                if (controls.Quad.Left.ReadValue<float>() > 0)
                {
                    vehicleRB.AddTorque(new Vector3(0, -vehicleTorque, 0));
                }
                //RIGHT
                else if (controls.Quad.Right.ReadValue<float>() > 0)
                {
                    vehicleRB.AddTorque(new Vector3(0, vehicleTorque, 0));
                }
            }

            //BACKWARDS
            if(controls.Quad.Backward.ReadValue<float>() > 0 && controls.Quad.Forward.ReadValue<float>() == 0 && transform.rotation.ToEulerAngles().x > -1)
            {
                if (controls.Quad.Left.ReadValue<float>() == 0 && controls.Quad.Right.ReadValue<float>() == 0)
                {
                    if (vehicleRB.velocity.y > -minDriftSpeed / 2 && vehicleRB.velocity.y <= vehicleMaxSpeed / 2)
                        vehicleRB.velocity += transform.TransformDirection(new Vector3(0, 0, -vehicleAcceleration));
                    else if (vehicleRB.velocity.y <= vehicleMaxSpeed / 2)
                        vehicleRB.velocity += transform.TransformDirection(new Vector3(0, 0, -vehicleAcceleration / 10));
                }
                else if (controls.Quad.Left.ReadValue<float>() > 0 && controls.Quad.Right.ReadValue<float>() == 0)
                {
                    if (vehicleRB.velocity.y > -minDriftSpeed / 2 && vehicleRB.velocity.y <= vehicleMaxSpeed / 2)
                        vehicleRB.velocity += transform.TransformDirection(new Vector3(0, 0, -vehicleAcceleration));
                    else if (vehicleRB.velocity.y <= vehicleMaxSpeed / 2)
                        vehicleRB.velocity += transform.TransformDirection(new Vector3(0, 0, -vehicleAcceleration / 10));

                    vehicleRB.AddTorque(new Vector3(0, vehicleTorque, 0));
                }
                else if (controls.Quad.Left.ReadValue<float>() == 0 && controls.Quad.Right.ReadValue<float>() > 0)
                {
                    if (vehicleRB.velocity.y > -minDriftSpeed / 2 && vehicleRB.velocity.y <= vehicleMaxSpeed / 2)
                        vehicleRB.velocity += transform.TransformDirection(new Vector3(0, 0, -vehicleAcceleration));
                    else if (vehicleRB.velocity.y <= vehicleMaxSpeed / 2)
                        vehicleRB.velocity += transform.TransformDirection(new Vector3(0, 0, -vehicleAcceleration / 10));

                    vehicleRB.AddTorque(new Vector3(0, -vehicleTorque, 0));
                }
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
            if (!alaDelta)
                FallFunction();
        }

        //ALADELTA FUNCTION
        AlaDeltaFunction();

        if (reduceSpeed && vehicleMaxSpeed > savedMaxSpeed)
        {
            vehicleMaxSpeed -= Time.deltaTime * 10;
        }
        else if(reduceSpeed && vehicleMaxSpeed <= savedMaxSpeed)
        {
            reduceSpeed = false;
            vehicleMaxSpeed = savedMaxSpeed;
        }

        //VEHICLE SOUND PITCH SYSTEM
        VehicleSoundPitchFunction();
    }

    void VehicleRecoverFunction()
    {
        timerReversed += Time.deltaTime;
        if(timerReversed >= 1)
        {
            AudioManager.Instance.Play_SFX("Fall_SFX");
            transform.position = respawnPosition;
            transform.localEulerAngles = respawnRotation;
            transform.localEulerAngles += new Vector3(0, 90, 0);
            vehicleRB.velocity = new Vector3(respawnVelocity.x, respawnVelocity.y, respawnVelocity.z);
            timerReversed = 0;
        }
    }

    void ChasisElevationFunction()
    {
        Transform chasisTransform = transform.GetChild(0);

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
                vehicleRB.AddTorque(new Vector3(0, -vehicleTorque * driftTorqueInc, 0));
            }
            else if (controls.Quad.Right.ReadValue<float>() > 0 && controls.Quad.Drift.ReadValue<float>() > 0)
            {
                vehicleRB.AddTorque(new Vector3(0, vehicleTorque * driftTorqueInc, 0));
            }
        }
    }

    void VehicleSoundPitchFunction()
    {
        if ((vehicleRB.velocity.magnitude > 1 || vehicleRB.velocity.magnitude < -1) && !GetComponent<AudioSource>().enabled && lifeVehicle > 0)
        {
            this.GetComponent<AudioSource>().enabled = true;
        }
        else if ((vehicleRB.velocity.magnitude <= 1 && vehicleRB.velocity.magnitude >= -1) && GetComponent<AudioSource>().enabled && lifeVehicle > 0)
            this.GetComponent<AudioSource>().enabled = false;

        if (this.GetComponent<AudioSource>().enabled)
        {
            this.GetComponent<AudioSource>().pitch = (vehicleRB.velocity.magnitude * 1) / vehicleMaxSpeed/2;
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

    void AlaDeltaFunction() 
    {
        if (!alaDelta && touchingGround && controls.Quad.AlaDelta.ReadValue<float>() == 1)
            alaDelta = true;

        if(alaDelta && alaDeltaTimer >= 0)
        {
            alaDeltaTimer -= Time.deltaTime;
            if (alaDeltaTimer >= alaDeltaDuration - 0.6f)
            {
                this.transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
                vehicleRB.velocity += new Vector3(0, 1, 0);
            }
            else
            {
                this.transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
                if (controls.Quad.Right.ReadValue<float>() == 1)
                    vehicleRB.AddTorque(new Vector3(0, vehicleTorque, 0));
                else if (controls.Quad.Left.ReadValue<float>() == 1)
                    vehicleRB.AddTorque(new Vector3(0, -vehicleTorque, 0));

                savedVelocity = transform.TransformDirection(new Vector3(0, 0, 25));

                vehicleRB.velocity = new Vector3(savedVelocity.x, -5, savedVelocity.z);
            }
            if(alaDeltaTimer <= 0)
            {
                alaDeltaTimer = alaDeltaDuration;
                alaDelta = false;
            }
        }
    }

    internal void SetCarModifiers()
    {
        for(int i = 0; i < listOfModifiers.Count; i++)
        {
            switch (listOfModifiers[i])
            {
                case "Floater":
                    hasFloater = true;

                    break;

                default:
                    break;
            }
        }

    }

    void UpdateCarModifiers()
    {
        for (int i = 0; i < listOfModifiers.Count; i++)
        {
            switch (listOfModifiers[i])
            {
                //case "Floater":
                    

                //    break;

                default:
                    break;
            }
        }
    }

    void checkFallDeath()
    {
        if(vehicleRB.velocity.y <= -22)
        {
            AudioManager.Instance.Play_SFX("Fall_SFX");
            transform.position = respawnPosition;
            transform.localEulerAngles = respawnRotation;
            transform.localEulerAngles += new Vector3(0, 90, 0);
            vehicleRB.velocity = new Vector3(respawnVelocity.x, respawnVelocity.y, respawnVelocity.z);
        }
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
            vehicleMaxSpeed = boostPadMultiplier * angle;
            if (vehicleMaxSpeed < savedMaxSpeed)
                vehicleMaxSpeed = savedMaxSpeed;
        }

        if (other.CompareTag("Water"))
        {
            vehicleRB.AddForce(other.GetComponent<WaterStreamColliderScript>().Stream, ForceMode.Force);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Water") && !hasFloater)
        {
            vehicleMaxSpeed = savedMaxSpeed / 3;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log(vehicleMaxSpeed);
        vehicleMaxSpeed = savedMaxSpeed;
        StartCoroutine(WaitEndBoost());

        if(other.tag.Equals("Water") && !hasFloater)
        {
            vehicleMaxSpeed = savedMaxSpeed;
        }

    }

    void OnCollisionExit(Collision other)
    {
        vehicleReversed = false;
        timerReversed = 0;
    }

}
