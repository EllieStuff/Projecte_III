using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerVehicleScript : MonoBehaviour
{
    internal int playerNum;

    [SerializeField] Vector3 centerOfMass = new Vector3(0.0f, -0.7f, 0.0f);
    [SerializeField] float boostPadDuration;
    [SerializeField] float driftTorqueInc = 3.0f;

    private Material chasisMat;

    private float timerReversed;
    public float savedMaxSpeed;
    private bool reduceSpeed;
    private float savedAngularDrag;

    internal QuadControlSystem controls;
    PlayerInputs inputs;

    public Rigidbody vehicleRB;
    private float vehicleAcceleration;
    public float vehicleTorque;
    public float vehicleMaxSpeed;
    public float vehicleMaxTorque;
    private WheelCollider[] wheelCollider;
    private GameObject wheels;
    public bool touchingGround;
    public bool vehicleReversed;
    public float minDriftSpeed;
    public Vector3 respawnPosition, respawnRotation, respawnVelocity;
    public float boostPadMultiplier;
    public float maxClimbingAngle;
    [SerializeField] private float sandVelocityMultiplier;
    [SerializeField] private float sandAccelerationMultiplier;

    [SerializeField] private GameObject wheelsPivot;

    internal bool buildingScene;
    internal List<Transform> listOfModifiers;
    private float savedAcceleration;
    [SerializeField] private Material particleMat;
    [SerializeField] private Transform quadChasisShake;
    private float timerShake;
    private Color defaultColorMat;
    bool paintingChecked = false, oilChecked = false;

    public bool finishedRace;
    public Vector3 savedVelocity;

    [SerializeField] private AudioClip driftClip;
    [SerializeField] private AudioClip normalClip;
    [SerializeField] private AudioClip boostClip;

    private PlayerAlaDelta alaDelta;

    private Transform outTransform;
    private Rigidbody outVehicleRB;

    void Start()
    {
        alaDelta = GetComponent<PlayerAlaDelta>();

        controls = new QuadControlSystem();

        inputs = GetComponent<PlayerInputs>();
        //inputs.SetGameMode(gameMode);

        defaultColorMat = Color.white;
        particleMat.color = defaultColorMat;
        
        vehicleAcceleration = 2;

        savedAcceleration = vehicleAcceleration;

        wheelCollider = new WheelCollider[4];

        Transform _wheels = transform.GetChild(1);

        for (int i = 0; i < wheelCollider.Length; i++)
        {
            wheelCollider[i] = _wheels.GetChild(i).GetComponent<WheelCollider>();
        }

        wheelsPivot = transform.GetChild(1).gameObject;

        GetComponent<AudioSource>().enabled = false;
        Physics.gravity = new Vector3(0, -9.8f * 2, 0);
        vehicleRB = GetComponent<Rigidbody>();
        vehicleRB.centerOfMass = centerOfMass;
        savedMaxSpeed = vehicleMaxSpeed;
        savedAngularDrag = vehicleRB.angularDrag;

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

    void Update()
    {
        if (inputs.ControlData != null)
        {
            touchingGround = false;
            paintingChecked = oilChecked = false;

            if (timerShake < 10000)
                timerShake += Time.deltaTime;
            else
                timerShake = 0;

            try
            {
                quadChasisShake.localPosition += new Vector3(0, Mathf.Sin(timerShake * 75) / 400, 0);
            }
            catch (Exception ex)
            {
                quadChasisShake = transform.GetChild(0).GetChild(0);
            }

            //Here we set the position and rotation from the wheel renderers

            if (!buildingScene)
            {
                Vector3 wheelPosition;
                Quaternion wheelRotation;
                for (int i = 0; i < wheelsPivot.transform.childCount; i++)
                {
                    Transform wheel = wheels.transform.GetChild(0).GetChild(i);
                    wheelCollider[i].GetWorldPose(out wheelPosition, out wheelRotation);
                    if (wheelCollider[i].GetGroundHit(out var touchingGroundV))
                    {
                        touchingGround = true;
                    }

                    wheel.position = Vector3.Lerp(wheel.position, wheelPosition, Time.deltaTime * 2);

                    if (wheel.tag.Equals("front") && (inputs.Right || inputs.Left))
                        wheel.transform.localRotation = Quaternion.Lerp(wheel.transform.localRotation, new Quaternion(0, (inputs.RightFloat / 5) - (inputs.LeftFloat / 5), 0, 1), Time.deltaTime * 3);
                    else
                        wheel.transform.rotation = wheelRotation;

                    wheels.transform.localPosition = transform.localPosition;
                    wheels.transform.localRotation = transform.localRotation;

                    //Falldeath Check

                    if (DeathScript.DeathByFalling(alaDelta.usingAlaDelta, transform, vehicleRB, respawnPosition, respawnRotation, respawnVelocity, out outTransform, out outVehicleRB))

                    {

                        transform.position = outTransform.position;

                        transform.rotation = outTransform.rotation;

                        vehicleRB.velocity = outVehicleRB.velocity;

                    }
                }

                if (touchingGround && vehicleRB.constraints != RigidbodyConstraints.None)
                {
                    vehicleRB.constraints = RigidbodyConstraints.None;
                }

                transform.parent.GetChild(2).localPosition = transform.localPosition;
            }

        }

    }

    public void SetWheels()
    {
        wheels = gameObject.transform.parent.GetChild(1).gameObject;
    }

    void FixedUpdate()
    {
        if(!SceneManager.GetActiveScene().name.Equals("Building Scene Multiplayer"))
        {
            if (controls == null)
                controls = new QuadControlSystem();

            controls.getAllInput(playerNum);

            //------Movement------

            if (!finishedRace)
                vehicleMovement();
            else
            {
                if (transform.InverseTransformDirection(vehicleRB.velocity).z > 1)
                    vehicleRB.velocity -= transform.TransformDirection(new Vector3(0, 0, vehicleAcceleration));
                else if(transform.InverseTransformDirection(vehicleRB.velocity).z < -1)
                    vehicleRB.velocity += transform.TransformDirection(new Vector3(0, 0, vehicleAcceleration));
                else
                    vehicleRB.velocity = Vector3.zero;
            }
            //------------------------
        }
    }

    void OnCollisionStay(Collision other)
    {
        //------Player Death------
        vehicleReversed = (other.gameObject.tag.Equals("ground"));
        //------------------------
    }

    void vehicleMovement()
    {
        var locVel = transform.InverseTransformDirection(vehicleRB.velocity);

        if(touchingGround && (vehicleRB.transform.rotation.x >= maxClimbingAngle || vehicleRB.transform.rotation.y >= maxClimbingAngle || vehicleRB.transform.rotation.z >= maxClimbingAngle))
            touchingGround = false;


        if (touchingGround)
        {
            //Main Movement Keys______________________________________________________________________________________________________________________
            //Forward
            if( inputs.Forward && !inputs.Backward && transform.rotation.ToEulerAngles().x > -1)
            {
                if (vehicleRB.velocity.y <= vehicleMaxSpeed / 2)
                    vehicleRB.velocity += transform.TransformDirection(new Vector3(0, 0, vehicleAcceleration));

                if (!inputs.Right && inputs.Left)
                    vehicleRB.AddTorque(new Vector3(0, -vehicleTorque * inputs.LeftFloat, 0));
                else if (inputs.Right && !inputs.Left)
                    vehicleRB.AddTorque(new Vector3(0, vehicleTorque * inputs.RightFloat, 0));
            }
            //Left Or Right
            if (!inputs.Forward && !inputs.Backward)
            {
                //Left
                if (inputs.Left)
                    vehicleRB.AddTorque(new Vector3(0, -vehicleTorque /** inputs.LeftFloat*/, 0));
                //Right
                else if (inputs.Right)
                    vehicleRB.AddTorque(new Vector3(0, vehicleTorque /** inputs.RightFloat*/, 0));
            }

            //Backwards
            if(inputs.Backward && !inputs.Forward && transform.rotation.ToEulerAngles().x > -1)
            {
                if (vehicleRB.velocity.y > -minDriftSpeed / 2 && vehicleRB.velocity.y <= vehicleMaxSpeed / 2)
                    vehicleRB.velocity += transform.TransformDirection(new Vector3(0, 0, -vehicleAcceleration));
                else if (vehicleRB.velocity.y <= vehicleMaxSpeed / 2)
                    vehicleRB.velocity += transform.TransformDirection(new Vector3(0, 0, -vehicleAcceleration / 10));

                if (inputs.Left && !inputs.Right)
                    vehicleRB.AddTorque(new Vector3(0, vehicleTorque * inputs.LeftFloat, 0));
                else if (!inputs.Left && inputs.Right)
                    vehicleRB.AddTorque(new Vector3(0, -vehicleTorque * inputs.RightFloat, 0));
            }
            //Main Movements Keys______________________________________________________________________________________________________________________

            //Speed Regulation Function
            SpeedRegulation();

            //Drift Function
            DriftFunction();

            savedVelocity = vehicleRB.velocity;
        }
        else
        {
            //Fall Function
            if (!alaDelta.usingAlaDelta)
                FallFunction();
        }

        if (vehicleReversed)
        {

            //Vehicle recover zone

            timerReversed += Time.deltaTime;
            if(DeathScript.DeathByFlipping(timerReversed, transform, vehicleRB, respawnPosition, respawnRotation, respawnVelocity, out outTransform, out outVehicleRB))

            {

                transform.position = outTransform.position;

                transform.rotation = outTransform.rotation;

                vehicleRB.velocity = outVehicleRB.velocity;

            }

            //_________________________________________________________________________________________________________________________________________________________________

        }

        if (reduceSpeed && vehicleMaxSpeed > savedMaxSpeed)
        {
            vehicleMaxSpeed -= Time.deltaTime * 10;
        }
        else if(reduceSpeed && vehicleMaxSpeed <= savedMaxSpeed)
        {
            reduceSpeed = false;
            vehicleMaxSpeed = savedMaxSpeed;
            vehicleAcceleration = savedAcceleration;
        }

        //Vehicle sound pitch function
        VehicleSoundPitchFunction();
    }

    float driftTimer = 0;
    bool driftLeft;
    bool driftRight;
    Vector3 savedDir;
    Quaternion savedRot;
    Quaternion driftRot;

    void DriftFunction()
    {
        if (vehicleRB.velocity.magnitude >= minDriftSpeed && inputs.Forward && !inputs.Backward)
        {
            if (inputs.Left && inputs.Drift)
            {
                if(!driftLeft)
                {
                    vehicleRB.AddTorque(0, -vehicleTorque * driftTorqueInc, 0);
                    savedDir = vehicleRB.velocity;
                    vehicleRB.velocity += new Vector3(0, 5, 0);
                    driftRot = vehicleRB.rotation * new Quaternion(0, -0.5f * inputs.LeftFloat, 0, 1).normalized;
                    savedRot = vehicleRB.rotation;
                }
                else if (driftRot.y <= savedRot.y)
                    savedRot *= new Quaternion(0, -0.002f * inputs.RightFloat, 0, 1);

                driftLeft = true;

                savedDir += transform.TransformDirection(-0.4f * inputs.LeftFloat, 0, 0);
                savedRot *= new Quaternion(0, -0.015f * inputs.LeftFloat, 0, 1).normalized;

                vehicleRB.velocity = new Vector3(savedDir.x, vehicleRB.velocity.y, savedDir.z);
                vehicleRB.rotation = savedRot;

                if (driftRight)
                {
                    driftTimer = 1;
                    driftRight = false;
                }
                if (driftTimer > 0)
                {
                    driftTimer -= Time.deltaTime;
                    particleMat.color = Color.yellow;
                }
                else
                    particleMat.color = Color.red;
            }
            else if (inputs.Right && inputs.Drift)
            {
                if (!driftRight)
                {
                    vehicleRB.AddTorque(0, vehicleTorque * driftTorqueInc, 0);
                    savedDir = vehicleRB.velocity;
                    vehicleRB.velocity += new Vector3(0, 5, 0);
                    driftRot = vehicleRB.rotation * new Quaternion(0, 0.5f * inputs.RightFloat, 0, 1).normalized;
                    savedRot = vehicleRB.rotation;
                }
                else if (driftRot.y >= savedRot.y)
                    savedRot *= new Quaternion(0, 0.002f * inputs.RightFloat, 0, 1);

                driftRight = true;

                savedDir += transform.TransformDirection(0.4f * inputs.RightFloat, 0, 0);
                savedRot *= new Quaternion(0, 0.015f * inputs.RightFloat, 0, 1).normalized;

                vehicleRB.velocity = new Vector3(savedDir.x, vehicleRB.velocity.y, savedDir.z);
                vehicleRB.rotation = savedRot;

                if (driftLeft)
                {
                    driftTimer = 1;
                    driftLeft = false;
                }

                if (driftTimer > 0)
                {
                    driftTimer -= Time.deltaTime;
                    particleMat.color = Color.yellow;
                }
                else
                    particleMat.color = Color.red;
            }
            else if (driftTimer <= 0)
            {
                particleMat.color = defaultColorMat;
                vehicleAcceleration = 2;
                vehicleMaxSpeed = 28.5f;
                driftTimer = 1;
                StartCoroutine(WaitEndBoost());
            }
            else
            {
                particleMat.color = defaultColorMat;
                if (driftTimer != 1)
                    driftTimer = 1;
                driftLeft = false;
                driftRight = false;
            }
        }
        else if (driftTimer <= 0)
        {
            particleMat.color = defaultColorMat;
            vehicleAcceleration = 2;
            vehicleMaxSpeed = 28.5f;
            driftTimer = 1;
            StartCoroutine(WaitEndBoost());
        }
        else
        {
            particleMat.color = defaultColorMat;
            if(driftTimer != 1)
                driftTimer = 1;
            driftLeft = false;
            driftRight = false;
        }
    }

    float timerStart = 2;

    void VehicleSoundPitchFunction()
    {
        AudioSource audio = GetComponent<AudioSource>();

        if ((vehicleRB.velocity.magnitude > 1 || vehicleRB.velocity.magnitude < -1) && !GetComponent<AudioSource>().enabled)
            audio.enabled = true;
        else if ((vehicleRB.velocity.magnitude <= 1 && vehicleRB.velocity.magnitude >= -1) && GetComponent<AudioSource>().enabled)
            audio.enabled = false;

        if (audio.enabled)
           audio.pitch = (vehicleRB.velocity.magnitude * 1) / vehicleMaxSpeed/2;

        if (inputs.Drift && (inputs.Left || inputs.Right) && vehicleMaxSpeed <= savedMaxSpeed && vehicleRB.velocity.magnitude > 0.5f)
        {
            audio.pitch = 1;
            if (audio.clip != driftClip)
            {
                audio.loop = true;
                audio.volume = 0.05f;
                audio.clip = driftClip;
                audio.enabled = false;
                audio.enabled = true;
            }
        }
        else if (vehicleMaxSpeed <= savedMaxSpeed)
        {
            if (audio.clip != normalClip)
            {
                audio.loop = true;
                audio.volume = 0.5f;
                audio.clip = normalClip;
                audio.enabled = false;
                audio.enabled = true;
            }
        }
        else if (timerStart <= 0)
        {
            audio.pitch = 1;
            if (audio.clip != boostClip && vehicleMaxSpeed > savedMaxSpeed + 5)
            {
                audio.volume = 0.2f;
                audio.clip = boostClip;
                audio.enabled = false;
                audio.enabled = true;
                audio.loop = false;
            }
        }
        else
            timerStart -= Time.deltaTime;
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
        if (!inputs.Left && !inputs.Right)
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
                if (inputs.Backward && vehicleRB.velocity.x < 0)
                    vehicleRB.velocity = transform.TransformDirection(new Vector3(0, 0, -vehicleMaxSpeed));
            }
            if (vehicleRB.velocity.z > vehicleMaxSpeed)
                vehicleRB.velocity = transform.TransformDirection(new Vector3(0, 0, vehicleMaxSpeed));
            if (inputs.Backward && vehicleRB.velocity.z < 0)
                vehicleRB.velocity = transform.TransformDirection(new Vector3(0, 0, -vehicleMaxSpeed));
        }
        else if (vehicleRB.velocity.z < -vehicleMaxSpeed || vehicleRB.velocity.x < -vehicleMaxSpeed)
        {
            if (!inputs.Backward)
                vehicleRB.velocity = transform.TransformDirection(new Vector3(0, 0, vehicleMaxSpeed));
            else
            {
                vehicleRB.velocity = transform.TransformDirection(new Vector3(0, 0, -vehicleMaxSpeed));
                if (vehicleRB.velocity.z < 0)
                    vehicleRB.velocity = transform.TransformDirection(new Vector3(0, 0, -vehicleMaxSpeed));
            }
        }
    }

    void checkFallDeath()
    {
        if(vehicleRB.velocity.y <= -100)
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
            vehicleAcceleration = 2;
            vehicleMaxSpeed = boostPadMultiplier * angle;
            if (vehicleMaxSpeed < savedMaxSpeed)
                vehicleMaxSpeed = savedMaxSpeed;
        }

        if (other.CompareTag("Water"))
        {
            vehicleRB.AddForce(other.GetComponent<WaterStreamColliderScript>().Stream, ForceMode.Force);
        }

        if (!paintingChecked && other.CompareTag("Painting"))
        {
            paintingChecked = true;
            if (vehicleRB.velocity.magnitude > 1.0f)
            {
                AddFriction(1000, 2.0f);
            }
        }
        if (!oilChecked && other.CompareTag("Oil"))
        {
            oilChecked = true;
            if (vehicleRB.velocity.magnitude > 1.0f)
            {
                AddFriction(-1200, 0.7f);
            }
        }

        //Terrain
        if (other.CompareTag("Sand") && touchingGround)
            OnSand(other);

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Respawn") && !other.GetComponent<DeathfallAndCheckpointsSystem>().activated)
        {
            GameObject.Find("UI").transform.Find("SliderPosition").GetComponent<UIPosition>().actualCheckpoint++;
            other.GetComponent<DeathfallAndCheckpointsSystem>().activated = true;
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if(!other.CompareTag("Sand"))
            StartCoroutine(WaitEndBoost());

        if (other.CompareTag("Painting") || other.CompareTag("Oil"))
        {
            ResetFriction();
        }

        //Terrain
        if (other.CompareTag("Sand") || !touchingGround)
        {
            //if (vehicleMaxSpeed == savedMaxSpeed / sandVelocityMultiplier)
            //{
                vehicleMaxSpeed = savedMaxSpeed;
                vehicleAcceleration = savedAcceleration;
            //}
        }
    }

    void OnCollisionExit(Collision other)
    {
        vehicleReversed = false;
        timerReversed = 0;
    }
    void ResetFriction()
    {
        vehicleRB.angularDrag = savedAngularDrag;
    }
    void AddFriction(float _frictionForce, float _dragInc)
    {
        Vector3 velFrictionVec = -vehicleRB.velocity.normalized * _frictionForce * vehicleRB.velocity.magnitude;
        vehicleRB.AddForce(velFrictionVec, ForceMode.Force);
        vehicleRB.angularDrag = savedAngularDrag * _dragInc;
    }

    void OnSand(Collider other)
    {
        if(vehicleMaxSpeed <= savedMaxSpeed && vehicleMaxSpeed > savedMaxSpeed / sandVelocityMultiplier)
        {
            vehicleMaxSpeed = savedMaxSpeed / sandVelocityMultiplier;
            vehicleAcceleration = savedAcceleration / sandAccelerationMultiplier;
        }
    }


    internal IEnumerator LerpVehicleMaxSpeed(float _targetValue, float _lerpTime)
    {
        float lerpTimer = 0;
        while (vehicleMaxSpeed != _targetValue)
        {
            yield return new WaitForEndOfFrame();
            lerpTimer += Time.deltaTime;
            vehicleMaxSpeed = Mathf.Lerp(vehicleMaxSpeed, _targetValue, lerpTimer / _lerpTime);
        }
    }

}
