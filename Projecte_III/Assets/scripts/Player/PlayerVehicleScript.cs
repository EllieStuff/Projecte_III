using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerVehicleScript : MonoBehaviour
{
    internal int playerNum;

    [SerializeField] Vector3 centerOfMass = new Vector3(0.0f, -0.7f, 0.0f);

    [SerializeField] internal int lifes;

    private Material chasisMat;

    internal float timerReversed;
    internal float savedMaxSpeed;
    [HideInInspector] public float savedAngularDrag;

    //internal QuadControlSystem controls;
    internal PlayerInputs inputs;

    public Rigidbody vehicleRB;
    internal float vehicleAcceleration;
    public float vehicleTorque;
    internal float savedVehicleTorque;
    public float vehicleMaxSpeed;
    public float vehicleMaxTorque;
    internal WheelCollider[] wheelCollider;
    private GameObject wheelsModels;
    public bool touchingGround;
    public bool vehicleReversed;
    public float minDriftSpeed;
    [SerializeField] internal float sandVelocityMultiplier;
    [SerializeField] internal float sandAccelerationMultiplier;
    //[HideInInspector] public bool affectedByOil, affectedByPaint;
    [HideInInspector] public Dictionary<Transform, DecalDefaultScript> oilObstacles = new Dictionary<Transform, DecalDefaultScript>();
    [HideInInspector] public Dictionary<Transform, DecalDefaultScript> paintObstacles = new Dictionary<Transform, DecalDefaultScript>();

    [SerializeField] private GameObject wheelsPivot;

    internal List<Transform> listOfModifiers;
    internal float savedAcceleration;
    [SerializeField] internal Material particleMat;
    [SerializeField] private ParticleSystemRenderer smokeBoostParticles;
    [SerializeField] private Transform quadChasisShake;
    private float timerShake;
    internal Color defaultColorMat;
    
    //public bool onWater;

    public Vector3 savedVelocity;
    internal float timerStartRace;
    [HideInInspector] public bool raceStarted = false;
    private bool startAcceleration;

    [SerializeField] private AudioClip normalClip;

    private Transform outTransform;
    private Rigidbody outVehicleRB;
    internal float baseMaxSpeed;
    internal bool speedIncrementEnabled;
    [HideInInspector] public float reinitTorqueTimer = -1;

    private VehicleTriggerAndCollisionEvents events;

    internal bool dash = false, dashCollided = false;

    public bool iaEnabled = false;

    private void Awake()
    {
        playerNum = GetComponentInParent<PlayerData>().id;
    }

    void Start()
    {
        Init();

    }
    public void Init()
    {
        StopAllCoroutines();

        events = GetComponent<VehicleTriggerAndCollisionEvents>();

        speedIncrementEnabled = true;

        savedVehicleTorque = vehicleTorque;

        lifes = 3;
        timerStartRace = 7;
        raceStarted = false;

        //controls = new QuadControlSystem();

        inputs = GetComponent<PlayerInputs>();

        //inputs.SetGameMode(gameMode);

        defaultColorMat = Color.white;
        particleMat.color = defaultColorMat;
        Material mat = new Material(particleMat);
        smokeBoostParticles.material = mat;
        particleMat = mat;

        vehicleAcceleration = 0.5f;

        savedAcceleration = vehicleAcceleration;

        wheelCollider = new WheelCollider[4];
        Transform _wheels = transform.Find("Wheels Colliders");
        for (int i = 0; i < wheelCollider.Length; i++)
        {
            wheelCollider[i] = _wheels.GetChild(i).GetComponent<WheelCollider>();
        }
        wheelsPivot = _wheels.gameObject;

        GetComponent<AudioSource>().enabled = false;
        Physics.gravity = new Vector3(0, -9.8f * 2, 0);
        vehicleRB = GetComponent<Rigidbody>();
        vehicleRB.centerOfMass = centerOfMass;
        savedMaxSpeed = vehicleMaxSpeed;
        baseMaxSpeed = vehicleMaxSpeed;
        savedMaxSpeed -= 3;
        savedAngularDrag = vehicleRB.angularDrag;

        wheelsModels = transform.parent.GetChild(1).gameObject;

        StartCoroutine(ReinitTorqueOverTime());
    }

    bool InmunityCheck()
    {
        return events.inmunity;
    }

    void Update()
    {
        if (inputs.ControlData != null)
        {
            touchingGround = InmunityCheck();

            if (timerShake < 10000)
                timerShake += Time.deltaTime;
            else
                timerShake = 0;

            //quadChasisShake.localPosition += new Vector3(0, Mathf.Sin(timerShake * 75) / 400, 0);

            //Here we set the position and rotation from the wheel renderers
            Vector3 wheelPosition;
            Quaternion wheelRotation;

            for (int i = 0; i < wheelsPivot.transform.childCount; i++)
            {
                Transform wheel = wheelsModels.transform.GetChild(0).GetChild(i);
                wheelCollider[i].GetWorldPose(out wheelPosition, out wheelRotation);
                if (wheelCollider[i].GetGroundHit(out var touchingGroundV))
                {
                    touchingGround = true;
                }

                //if (!wheel.name.Contains("Front"))
                    wheel.position = wheelPosition;

                if (wheel.name.Contains("Front") && (inputs.Right || inputs.Left))
                {
                    int left = 0;
                    int right = 0;

                    if (inputs.Left)
                        left = 1;
                    else if (inputs.Right)
                        right = 1;

                    wheel.transform.localRotation = Quaternion.Lerp(wheel.transform.localRotation, new Quaternion(0, Mathf.Clamp(right, -0.2f, 0.2f) - Mathf.Clamp(left, -0.2f, 0.2f), 0, 1), Time.deltaTime * 3);
                }
                else
                    wheel.transform.rotation = wheelRotation;

                wheelsModels.transform.localPosition = transform.localPosition;
                wheelsModels.transform.localRotation = transform.localRotation;
            }

            if (touchingGround && vehicleRB.constraints != RigidbodyConstraints.None)
                vehicleRB.constraints = RigidbodyConstraints.None;

            //transform.parent.GetChild(2).localPosition = transform.localPosition;
        }
    }

    void FixedUpdate()
    {
        //if (controls == null)
        //    controls = new QuadControlSystem();

        //controls.getAllInput(playerNum);

        //------Movement------
        if (raceStarted && !iaEnabled)
        {
            vehicleMovement();
            if (!startAcceleration)
            {
                vehicleRB.velocity = Vector3.zero;
                if (inputs.Forward || inputs.Backward)
                    startAcceleration = true;
            }
        }
        else if (timerStartRace <= 0)
        {
            raceStarted = true;
            //vehicleRB.isKinematic = false;
        }
        else if (touchingGround)
        {
            timerStartRace -= Time.deltaTime;
            vehicleRB.velocity = Vector3.zero;
            //vehicleRB.isKinematic = true;
        }
    }

    void vehicleMovement()
    {
        if (dash || dashCollided) return;
        var locVel = transform.InverseTransformDirection(vehicleRB.velocity);

        bool disableReverse = (vehicleMaxSpeed > savedMaxSpeed);

        if(speedIncrementEnabled && savedMaxSpeed < baseMaxSpeed)
        {
            savedMaxSpeed += Time.deltaTime * 0.04f;
            vehicleMaxSpeed = savedMaxSpeed;
        }

        if (touchingGround)
        {
            //Main Movement Keys______________________________________________________________________________________________________________________
            //Forward
            if(inputs.Forward && !inputs.Backward)
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
            if(inputs.Backward && !inputs.Forward && !disableReverse)
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
            SpeedRegulation(disableReverse);

            savedVelocity = vehicleRB.velocity;
        }
        else
        {
            //Fall Function
            FallFunction();
        }
        //Vehicle sound pitch function
        VehicleSoundPitchFunction();
    }

    internal void VehicleSoundPitchFunction()
    {
        AudioSource audio = GetComponent<AudioSource>();

        if ((vehicleRB.velocity.magnitude > 1 || vehicleRB.velocity.magnitude < -1) && !GetComponent<AudioSource>().enabled)
            audio.enabled = true;
        else if ((vehicleRB.velocity.magnitude <= 1 && vehicleRB.velocity.magnitude >= -1) && GetComponent<AudioSource>().enabled)
            audio.enabled = false;

        if (audio.enabled)
           audio.pitch = (vehicleRB.velocity.magnitude * 1) / vehicleMaxSpeed/2;
    }


    internal void FallFunction()
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
    void SpeedRegulation(bool disableReverse)
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
            else if(!disableReverse)
            {
                vehicleRB.velocity = transform.TransformDirection(new Vector3(0, 0, -vehicleMaxSpeed));
                if (vehicleRB.velocity.z < 0)
                    vehicleRB.velocity = transform.TransformDirection(new Vector3(0, 0, -vehicleMaxSpeed));
            }
        }
    }


    IEnumerator ReinitTorqueOverTime()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            if(reinitTorqueTimer >= 0)
            {
                reinitTorqueTimer -= Time.deltaTime;
                if (reinitTorqueTimer < 0) vehicleTorque = savedVehicleTorque;
            }
        }
    }

}
