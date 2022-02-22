using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerVehicleScriptP2 : MonoBehaviour
{
    [SerializeField] private int playerNum;

    [SerializeField] Vector3 centerOfMass = new Vector3(0.0f, -0.7f, 0.0f);
    [SerializeField] float boostPadDuration;
    [SerializeField] float alaDeltaDuration;
    [SerializeField] float alaDeltaTimer;
    [SerializeField] float driftTorqueInc = 3.0f;

    Vector3 savedDirection;

    private Material chasisMat;
    private Vector3 savedVelocity;
    private float timerReversed;
    private float savedMaxSpeed;
    private bool reduceSpeed;

    QuadControlSystem controls;

    public Rigidbody vehicleRB;
    public float vehicleAcceleration;
    public float vehicleTorque;
    public float vehicleMaxSpeed;
    public float vehicleMaxTorque;
    public GameObject wheels;
    public int lifeVehicle;
    public bool touchingGround;
    public bool vehicleReversed;
    public float minDriftSpeed;
    public Vector3 respawnPosition, respawnRotation, respawnVelocity;
    public float boostPadMultiplier;
    public float maxClimbingAngle;
    private float chasisElevationTimer;
    [SerializeField] private bool chasisElevation;
    private bool alaDelta;

    [SerializeField] private GameObject wheelsPivot;

    private bool desatascador;
    private float desatascadorCooldown;

    internal bool buildingScene;
    internal List<string> listOfModifiers;
    bool hasFloater = false;
    private float savedAcceleration;
    [SerializeField] private int desatascadorBaseCooldown = 20;
    [SerializeField] private GameObject desatascadorPrefab;
    private GameObject desatascadorInstance;

    // Start is called before the first frame update
    void Start()
    {
        desatascadorBaseCooldown = 10;
        wheelsPivot = transform.GetChild(1).gameObject;
        alaDeltaDuration = 1;
        alaDeltaTimer = 1;
        controls = new QuadControlSystem();

        this.GetComponent<AudioSource>().enabled = false;
        chasisMat = new Material(this.transform.GetChild(0).GetComponent<MeshRenderer>().material);
        this.transform.GetChild(0).GetComponent<MeshRenderer>().material = chasisMat;
        chasisMat.color = Color.red;
        Physics.gravity = new Vector3(0, -9.8f * 2, 0);
        vehicleRB = this.GetComponent<Rigidbody>();
        vehicleRB.centerOfMass = centerOfMass;
        savedMaxSpeed = vehicleMaxSpeed;

        wheels = transform.parent.GetChild(1).GetChild(0).gameObject;
    }

    private void Awake()
    {
        this.transform.name = "Player";
        respawnPosition = new Vector3(0, 0, 0);
        respawnRotation = new Vector3(0, 0, 0);
        respawnVelocity = new Vector3(0, 0, 0);
        savedAcceleration = vehicleAcceleration;
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

        if (!buildingScene)
        {
            Vector3 wheelPosition;
            Quaternion wheelRotation;
            for (int i = 0; i < wheelsPivot.transform.childCount; i++)
            {
                Transform wheel = wheels.transform.GetChild(i);
                WheelCollider wheelCol = wheelsPivot.transform.GetChild(i).GetComponent<WheelCollider>();
                wheelCol.GetWorldPose(out wheelPosition, out wheelRotation);
                if (wheelCol.GetGroundHit(out var touchingGroundV))
                {
                    touchingGround = true;
                }

                wheel.transform.position = wheelPosition;

                if (wheel.tag.Equals("front") && (controls.QuadP2.Right == 1 || controls.QuadP2.Left == 1))
                    wheel.transform.localRotation = Quaternion.Lerp(wheel.transform.localRotation, new Quaternion(0, (controls.QuadP2.Right / 5) - (controls.QuadP2.Left / 5), 0, 1), Time.deltaTime * 3);
                else
                    wheel.transform.rotation = wheelRotation;

                wheels.transform.localPosition = transform.localPosition;
                wheels.transform.localRotation = transform.localRotation;
                //FALLDEATH CHECK
                if (!alaDelta)
                    checkFallDeath();
                //RESETTING ALADELTA VARIABLES
                if (alaDeltaTimer <= alaDeltaDuration - 0.8f)
                {
                    alaDeltaTimer = alaDeltaDuration;
                    alaDelta = false;
                }
            }
        }

        if (touchingGround && vehicleRB.constraints != RigidbodyConstraints.None)

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

    public void Desatascador()
    {
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, 2, transform.TransformDirection(Vector3.forward), out hit, 30))
        {
            if (hit.transform.tag.Contains("Player") && hit.transform != transform)
            {
                savedDirection = (hit.transform.position - transform.position).normalized;
                Debug.Log(savedDirection);
            }
            else
            {
                Debug.Log(hit.transform.tag);
            }
        }

        if (controls.QuadP2.Drift > 0 && !desatascador && desatascadorCooldown <= 0 && desatascadorInstance == null)
        {
            desatascadorInstance = Instantiate(desatascadorPrefab, this.transform.position, this.transform.rotation);
            Physics.IgnoreCollision(desatascadorInstance.transform.GetChild(0).GetComponent<BoxCollider>(), transform.GetChild(0).GetComponent<BoxCollider>());
            desatascadorInstance.GetComponent<plungerInstance>().playerShotPlunger = this.gameObject;
            desatascadorInstance.GetComponent<plungerInstance>().playerNum = playerNum;
            desatascadorInstance.GetComponent<plungerInstance>().normalDir = savedDirection;
            desatascador = true;
            desatascadorCooldown = desatascadorBaseCooldown;
        }

        if (desatascadorCooldown > 0)
            desatascadorCooldown -= Time.deltaTime;

        if (desatascador)
        {
            if (desatascadorCooldown <= desatascadorBaseCooldown / 2 && desatascadorInstance != null)
            {
                savedDirection = Vector3.zero;
                vehicleMaxSpeed = savedMaxSpeed;
                desatascadorInstance.GetComponent<plungerInstance>().destroyPlunger = true;
                desatascadorInstance = null;
                desatascador = false;
            }
            else if (desatascadorInstance == null)
            {
                savedDirection = Vector3.zero;
                vehicleMaxSpeed = savedMaxSpeed;
                desatascador = false;
            }
            if (vehicleMaxSpeed > savedMaxSpeed)
            {
                vehicleMaxSpeed -= 0.5f;
            }
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

        wheels = gameObject.transform.parent.GetChild(1).GetChild(0).gameObject;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        controls.getAllInput(playerNum);
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

        if (touchingGround && (vehicleRB.transform.rotation.x >= maxClimbingAngle || vehicleRB.transform.rotation.y >= maxClimbingAngle || vehicleRB.transform.rotation.z >= maxClimbingAngle))
            touchingGround = false;

        if (touchingGround && lifeVehicle > 0)
        {
            //MAIN MOVEMENT KEYS______________________________________________________________________________________________________________________
            //FORWARD
            if (controls.QuadP2.Forward > 0 && controls.QuadP2.Backward == 0 && transform.rotation.ToEulerAngles().x > -1)
            {
                if (controls.QuadP2.Right == 0 && controls.QuadP2.Left == 0)
                {
                    if (vehicleRB.velocity.y <= vehicleMaxSpeed / 2 && controls.QuadP2.Drift == 0)
                        vehicleRB.velocity += transform.TransformDirection(new Vector3(0, 0, vehicleAcceleration));
                }
                else if (controls.QuadP2.Right == 0 && controls.QuadP2.Left > 0)
                {
                    if (vehicleRB.velocity.y <= vehicleMaxSpeed / 2 && controls.QuadP2.Drift == 0)
                        vehicleRB.velocity += transform.TransformDirection(new Vector3(0, 0, vehicleAcceleration));

                    vehicleRB.AddTorque(new Vector3(0, -vehicleTorque, 0));
                }
                else if (controls.QuadP2.Right > 0 && controls.QuadP2.Left == 0)
                {
                    if (vehicleRB.velocity.y <= vehicleMaxSpeed / 2 && controls.QuadP2.Drift == 0)
                        vehicleRB.velocity += transform.TransformDirection(new Vector3(0, 0, vehicleAcceleration));

                    vehicleRB.AddTorque(new Vector3(0, vehicleTorque, 0));
                }
            }
            //LEFT OR RIGHT
            if (controls.QuadP2.Forward == 0 && controls.QuadP2.Backward == 0)
            {
                //LEFT
                if (controls.QuadP2.Left > 0)
                {
                    vehicleRB.AddTorque(new Vector3(0, -vehicleTorque, 0));
                }
                //RIGHT
                else if (controls.QuadP2.Right > 0)
                {
                    vehicleRB.AddTorque(new Vector3(0, vehicleTorque, 0));
                }
            }

            //BACKWARDS
            if (controls.QuadP2.Backward > 0 && controls.QuadP2.Forward == 0 && transform.rotation.ToEulerAngles().x > -1)
            {
                if (controls.QuadP2.Left == 0 && controls.QuadP2.Right == 0)
                {
                    if (vehicleRB.velocity.y > -minDriftSpeed / 2 && vehicleRB.velocity.y <= vehicleMaxSpeed / 2)
                        vehicleRB.velocity += transform.TransformDirection(new Vector3(0, 0, -vehicleAcceleration));
                    else if (vehicleRB.velocity.y <= vehicleMaxSpeed / 2)
                        vehicleRB.velocity += transform.TransformDirection(new Vector3(0, 0, -vehicleAcceleration / 10));
                }
                else if (controls.QuadP2.Left > 0 && controls.QuadP2.Right == 0)
                {
                    if (vehicleRB.velocity.y > -minDriftSpeed / 2 && vehicleRB.velocity.y <= vehicleMaxSpeed / 2)
                        vehicleRB.velocity += transform.TransformDirection(new Vector3(0, 0, -vehicleAcceleration));
                    else if (vehicleRB.velocity.y <= vehicleMaxSpeed / 2)
                        vehicleRB.velocity += transform.TransformDirection(new Vector3(0, 0, -vehicleAcceleration / 10));

                    vehicleRB.AddTorque(new Vector3(0, vehicleTorque, 0));
                }
                else if (controls.QuadP2.Left == 0 && controls.QuadP2.Right > 0)
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

            //PLUNGER FUNCTION
            Desatascador();


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
        else if (reduceSpeed && vehicleMaxSpeed <= savedMaxSpeed)
        {
            reduceSpeed = false;
            vehicleMaxSpeed = savedMaxSpeed;
            vehicleAcceleration = savedAcceleration;
        }



        //VEHICLE SOUND PITCH SYSTEM
        VehicleSoundPitchFunction();
    }

    void VehicleRecoverFunction()
    {
        timerReversed += Time.deltaTime;
        if (timerReversed >= 1)
        {
            AudioManager.Instance.Play_SFX("Fall_SFX");
            this.transform.position = respawnPosition;
            this.transform.localEulerAngles = respawnRotation;
            this.transform.localEulerAngles += new Vector3(0, 90, 0);
            vehicleRB.velocity = new Vector3(respawnVelocity.x, respawnVelocity.y, respawnVelocity.z);
            timerReversed = 0;
        }
    }

    void ChasisElevationFunction()
    {
        Transform chasisTransform = this.transform.GetChild(0);

        if (controls.QuadP2.ChasisElevation > 0 && !chasisElevation && chasisTransform.localPosition.y <= 0)
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
            if (controls.QuadP2.Left > 0 && controls.QuadP2.Drift > 0)
            {
                vehicleRB.AddTorque(new Vector3(0, -vehicleTorque * driftTorqueInc, 0));
            }
            else if (controls.QuadP2.Right > 0 && controls.QuadP2.Drift > 0)
            {
                vehicleRB.AddTorque(new Vector3(0, vehicleTorque * driftTorqueInc, 0));
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
            this.GetComponent<AudioSource>().pitch = (vehicleRB.velocity.magnitude * 1) / vehicleMaxSpeed / 2;
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
        if (controls.QuadP2.Left == 0 && controls.QuadP2.Right == 0)
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
                if (controls.QuadP2.Backward > 0 && vehicleRB.velocity.x < 0)
                    vehicleRB.velocity = transform.TransformDirection(new Vector3(0, 0, -vehicleMaxSpeed));
            }
            if (vehicleRB.velocity.z > vehicleMaxSpeed)
                vehicleRB.velocity = transform.TransformDirection(new Vector3(0, 0, vehicleMaxSpeed));
            if (controls.QuadP2.Backward > 0 && vehicleRB.velocity.z < 0)
                vehicleRB.velocity = transform.TransformDirection(new Vector3(0, 0, -vehicleMaxSpeed));
        }
        else if (vehicleRB.velocity.z < -vehicleMaxSpeed || vehicleRB.velocity.x < -vehicleMaxSpeed)
        {
            if (controls.QuadP2.Backward == 0)
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
        if (!alaDelta && touchingGround && controls.QuadP2.AlaDelta == 1)
            alaDelta = true;

        if (alaDelta && alaDeltaTimer >= 0)
        {
            alaDeltaTimer -= Time.deltaTime;
            if (alaDeltaTimer >= alaDeltaDuration - 0.6f)
            {
                this.transform.rotation = new Quaternion(0, this.transform.rotation.y, 0, this.transform.rotation.w);
                vehicleRB.velocity += new Vector3(0, 1, 0);
            }
            else
            {
                this.transform.rotation = new Quaternion(0, this.transform.rotation.y, 0, this.transform.rotation.w);
                if (controls.QuadP2.Right == 1)
                    vehicleRB.AddTorque(new Vector3(0, vehicleTorque, 0));
                else if (controls.QuadP2.Left == 1)
                    vehicleRB.AddTorque(new Vector3(0, -vehicleTorque, 0));

                savedVelocity = transform.TransformDirection(new Vector3(0, 0, 25));

                vehicleRB.velocity = new Vector3(savedVelocity.x, -5, savedVelocity.z);
            }
            if (alaDeltaTimer <= 0)
            {
                alaDeltaTimer = alaDeltaDuration;
                alaDelta = false;
            }
        }
    }

    internal void SetCarModifiers()
    {
        for (int i = 0; i < listOfModifiers.Count; i++)
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
        if (vehicleRB.velocity.y <= -22)
        {
            AudioManager.Instance.Play_SFX("Fall_SFX");
            this.transform.position = respawnPosition;
            this.transform.localEulerAngles = respawnRotation;
            this.transform.localEulerAngles += new Vector3(0, 90, 0);
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
            float angle = Vector3.Angle(this.transform.forward, other.transform.forward);
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
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Water") && !hasFloater)
        {
            StartCoroutine(LerpVehicleMaxSpeed(savedMaxSpeed / 3, 3.0f));
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log(vehicleMaxSpeed);
        StartCoroutine(WaitEndBoost());

        if (other.tag.Equals("Water") && !hasFloater)
        {
            StartCoroutine(LerpVehicleMaxSpeed(savedMaxSpeed, 1.5f));
        }

    }

    void OnCollisionExit(Collision other)
    {
        vehicleReversed = false;
        timerReversed = 0;
    }

    IEnumerator LerpVehicleMaxSpeed(float _targetValue, float _lerpTime)
    {
        float lerpTimer = 0;
        while (vehicleMaxSpeed != _targetValue)
        {
            yield return new WaitForEndOfFrame();
            lerpTimer += Time.deltaTime;
            vehicleMaxSpeed = Mathf.Lerp(vehicleMaxSpeed, _targetValue, lerpTimer / _lerpTime);

            //Debug.Log("In Coroutine: Curr->" + vehicleMaxSpeed + ". Target->" + _targetValue);
        }

    }

}

