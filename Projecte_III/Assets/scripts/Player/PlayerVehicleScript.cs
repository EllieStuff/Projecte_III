using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerVehicleScript : MonoBehaviour
{
    [SerializeField] private int playerNum;

    [SerializeField] Vector3 centerOfMass = new Vector3(0.0f, -0.7f, 0.0f);
    [SerializeField] float boostPadDuration;
    [SerializeField] float alaDeltaDuration;
    [SerializeField] float alaDeltaTimer;
    [SerializeField] float driftTorqueInc = 3.0f;

    Vector3 savedDirection;

    private Material chasisMat;
    //Vehicle Stats
    Stats stats;

    private Vector3 savedVelocity;
    private float timerReversed;
    private float savedMaxSpeed;
    private bool reduceSpeed;
    private float savedAngularDrag;

    QuadControlSystem controls;

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
    bool paintingChecked = false, oilChecked = false;

    public bool finishedRace;

    // Start is called before the first frame update
    void Start()
    {
        stats = gameObject.AddComponent<Stats>();

        vehicleAcceleration = 2;

                savedAcceleration = vehicleAcceleration;

        wheelCollider = new WheelCollider[4];

        Transform _wheels = transform.GetChild(1);

        for (int i = 0; i < wheelCollider.Length; i++)
        {
            wheelCollider[i] = _wheels.GetChild(i).GetComponent<WheelCollider>();
        }

        desatascadorBaseCooldown = 10;
        wheelsPivot = transform.GetChild(1).gameObject;
        alaDeltaDuration = 1;
        alaDeltaTimer = 1;
        controls = new QuadControlSystem();

        GetComponent<AudioSource>().enabled = false;
        Physics.gravity = new Vector3(0, -9.8f * 2, 0);
        vehicleRB = GetComponent<Rigidbody>();
        vehicleRB.centerOfMass = centerOfMass;
        savedMaxSpeed = vehicleMaxSpeed;
        savedAngularDrag = vehicleRB.angularDrag;

        wheels = transform.parent.GetChild(1).gameObject;

        SetStats();
    }

    public void SetStats()
    {
        stats.SetStats(new Stats.Data());

        //Wheels stats
        stats.SetStats(stats + wheels.GetComponentInChildren<Stats>());

        //Quad stats
        stats.SetStats(stats + GameObject.FindGameObjectWithTag("PlayerVehicle").transform.GetComponentInChildren<Stats>());

        //Modifier Stats
        Transform modfs = GameObject.FindGameObjectWithTag("ModifierSpots").transform;
        
        for (int i = 0; i < modfs.childCount; i++)
        {
            if(modfs.GetChild(i).childCount > 0)
                stats.SetStats(stats + modfs.GetChild(i).GetComponentInChildren<Stats>());
        }

        GameObject.FindGameObjectWithTag("StatsManager").GetComponentInChildren<StatsListUI>().UpdateStatsUI(stats.GetStats());
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
        paintingChecked = oilChecked = false;

        //HERE WE SET THE POSITION AND ROTATION FROM THE WHEELS RENDERERS

        //------Movement------

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

                wheel.position = wheelPosition;

                if (wheel.tag.Equals("front") && (controls.Quad.Right >= 0.2f || controls.Quad.Left >= 0.2f))
                    wheel.transform.localRotation = Quaternion.Lerp(wheel.transform.localRotation, new Quaternion(0, (controls.Quad.Right / 5) - (controls.Quad.Left / 5), 0, 1), Time.deltaTime * 3);
                else
                    wheel.transform.rotation = wheelRotation;

                wheels.transform.localPosition = transform.localPosition;
                wheels.transform.localRotation = transform.localRotation;

        //------------------

                //-----Modifiers-----

                //FALLDEATH CHECK
                if (!alaDelta)
                    checkFallDeath();
                //RESETTING ALADELTA VARIABLES
                if (alaDeltaTimer <= alaDeltaDuration - 0.8f)
                {
                    alaDeltaTimer = alaDeltaDuration;
                    alaDelta = false;
                }

                //--------------------
            }
        }

        if(touchingGround && vehicleRB.constraints != RigidbodyConstraints.None)
        {
            vehicleRB.constraints = RigidbodyConstraints.None;
        }

        transform.parent.GetChild(2).localPosition = transform.localPosition;
        //_______________________________________________________________

        //-----Temporal-----

        if (Input.GetKeyDown(KeyCode.F))
        {
            Physics.IgnoreLayerCollision(3, 4, hasFloater);
            hasFloater = !hasFloater;
        }

        //--------------------

    }

    Transform localTransform;
    Vector3 VectorLerp;
    float timerPoint;
    bool createMaterial;

    public void Desatascador()
    {
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, 10, transform.TransformDirection(Vector3.forward), out hit, 10))
        {
            if ((hit.transform.tag.Contains("Player") || hit.transform.tag.Contains("Tree") || hit.transform.tag.Contains("Valla")) && (hit.transform.position - transform.position).magnitude > 5 && hit.transform != transform)
                localTransform = hit.transform;
        }

        if(localTransform != null)
            savedDirection = (localTransform.position - transform.position).normalized;

        LineRenderer line = GetComponent<LineRenderer>();

        line.SetPosition(0, Vector3.zero);
        line.SetPosition(1, Vector3.zero);

        bool isCorrect = transform.InverseTransformDirection(savedDirection).z > 0.75;

        if (savedDirection != Vector3.zero && desatascadorCooldown <= 0)
        {
            Vector3 sum = (transform.position + savedDirection * 3);

            if (isCorrect)
            {
                line.material.color = Color.green;
                timerPoint = 2;
                line.SetPosition(0, transform.position);
                line.SetPosition(1, sum);
            }
            else if(timerPoint > 0)
            {
                line.material.color = Color.red;
                line.SetPosition(0, transform.position);
                line.SetPosition(1, sum);
                timerPoint -= Time.deltaTime;
            }
            else
            {
                savedDirection = Vector3.zero;
            }
        }

        if (controls.Quad.plunger && !desatascador && desatascadorCooldown <= 0 && desatascadorInstance == null)
        {
            if (!createMaterial)
            {
                line.material = new Material(line.material);
                createMaterial = true;
            }

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

        if(desatascador)
        {
            if(desatascadorCooldown <= desatascadorBaseCooldown/ 2 && desatascadorInstance != null)
            {
                savedDirection = Vector3.zero;
                vehicleMaxSpeed = savedMaxSpeed;
                desatascadorInstance.GetComponent<plungerInstance>().destroyPlunger = true;
                desatascadorInstance = null;
                desatascador = false;
            }
            else if(desatascadorInstance == null)
            {
                savedDirection = Vector3.zero;
                vehicleMaxSpeed = savedMaxSpeed;
                desatascador = false;
            }
            if(vehicleMaxSpeed > savedMaxSpeed)
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

    //------Wheels------

    public void SetWheels()
    {
        wheels = gameObject.transform.parent.GetChild(1).gameObject;
    }

    //------------------

    // Update is called once per frame
    void FixedUpdate()
    {
        if(playerNum == 2 && !SceneManager.GetActiveScene().name.Equals("Building Scene Multiplayer"))
        {
            GetComponent<PlayerVehicleScriptP2>().enabled = true;
            GetComponent<PlayerVehicleScript>().enabled = false;
        }

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

    void OnCollisionStay(Collision other)
    {
        //------Player Death------

        if (other.gameObject.tag.Equals("ground"))
            vehicleReversed = true;

        //------------------------
    }

    void vehicleMovement()
    {
        var locVel = transform.InverseTransformDirection(vehicleRB.velocity);

        if(touchingGround && (vehicleRB.transform.rotation.x >= maxClimbingAngle || vehicleRB.transform.rotation.y >= maxClimbingAngle || vehicleRB.transform.rotation.z >= maxClimbingAngle))
            touchingGround = false;

        if (touchingGround && lifeVehicle > 0)
        {
            //MAIN MOVEMENT KEYS______________________________________________________________________________________________________________________
            //FORWARD
            if(controls.Quad.Forward && !controls.Quad.Backward && transform.rotation.ToEulerAngles().x > -1)
            {
                if (controls.Quad.Right == 0 && controls.Quad.Left == 0)
                {
                    if (vehicleRB.velocity.y <= vehicleMaxSpeed / 2 && !controls.Quad.Drift)
                        vehicleRB.velocity += transform.TransformDirection(new Vector3(0, 0, vehicleAcceleration));
                }
                else if (controls.Quad.Right == 0 && controls.Quad.Left > 0)
                {
                    if (vehicleRB.velocity.y <= vehicleMaxSpeed / 2 && !controls.Quad.Drift)
                        vehicleRB.velocity += transform.TransformDirection(new Vector3(0, 0, vehicleAcceleration));

                    vehicleRB.AddTorque(new Vector3(0, -vehicleTorque * controls.Quad.Left, 0));
                }
                else if (controls.Quad.Right > 0 && controls.Quad.Left == 0)
                {
                    if (vehicleRB.velocity.y <= vehicleMaxSpeed / 2 && !controls.Quad.Drift)
                        vehicleRB.velocity += transform.TransformDirection(new Vector3(0, 0, vehicleAcceleration));

                    vehicleRB.AddTorque(new Vector3(0, vehicleTorque * controls.Quad.Right, 0));
                }
            }
            //LEFT OR RIGHT
            if (!controls.Quad.Forward && !controls.Quad.Backward)
            {
                //LEFT
                if (controls.Quad.Left > 0)
                {
                    vehicleRB.AddTorque(new Vector3(0, -vehicleTorque * controls.Quad.Left, 0));
                }
                //RIGHT
                else if (controls.Quad.Right > 0)
                {
                    vehicleRB.AddTorque(new Vector3(0, vehicleTorque * controls.Quad.Right, 0));
                }
            }

            //BACKWARDS
            if(controls.Quad.Backward && !controls.Quad.Forward && transform.rotation.ToEulerAngles().x > -1)
            {
                if (controls.Quad.Left == 0 && controls.Quad.Right == 0)
                {
                    if (vehicleRB.velocity.y > -minDriftSpeed / 2 && vehicleRB.velocity.y <= vehicleMaxSpeed / 2)
                        vehicleRB.velocity += transform.TransformDirection(new Vector3(0, 0, -vehicleAcceleration));
                    else if (vehicleRB.velocity.y <= vehicleMaxSpeed / 2)
                        vehicleRB.velocity += transform.TransformDirection(new Vector3(0, 0, -vehicleAcceleration / 10));
                }
                else if (controls.Quad.Left > 0 && controls.Quad.Right == 0)
                {
                    if (vehicleRB.velocity.y > -minDriftSpeed / 2 && vehicleRB.velocity.y <= vehicleMaxSpeed / 2)
                        vehicleRB.velocity += transform.TransformDirection(new Vector3(0, 0, -vehicleAcceleration));
                    else if (vehicleRB.velocity.y <= vehicleMaxSpeed / 2)
                        vehicleRB.velocity += transform.TransformDirection(new Vector3(0, 0, -vehicleAcceleration / 10));

                    vehicleRB.AddTorque(new Vector3(0, vehicleTorque * controls.Quad.Left, 0));
                }
                else if (controls.Quad.Left == 0 && controls.Quad.Right > 0)
                {
                    if (vehicleRB.velocity.y > -minDriftSpeed / 2 && vehicleRB.velocity.y <= vehicleMaxSpeed / 2)
                        vehicleRB.velocity += transform.TransformDirection(new Vector3(0, 0, -vehicleAcceleration));
                    else if (vehicleRB.velocity.y <= vehicleMaxSpeed / 2)
                        vehicleRB.velocity += transform.TransformDirection(new Vector3(0, 0, -vehicleAcceleration / 10));

                    vehicleRB.AddTorque(new Vector3(0, -vehicleTorque * controls.Quad.Right, 0));
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
        else if(reduceSpeed && vehicleMaxSpeed <= savedMaxSpeed)
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

        if (controls.Quad.ChasisElevation && !chasisElevation && chasisTransform.localPosition.y <= 0)
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
            if (controls.Quad.Left > 0 && controls.Quad.Drift)
            {
                vehicleRB.AddTorque(new Vector3(0, -vehicleTorque * driftTorqueInc, 0));
            }
            else if (controls.Quad.Right > 0 && controls.Quad.Drift)
            {
                vehicleRB.AddTorque(new Vector3(0, vehicleTorque * driftTorqueInc, 0));
            }
        }
    }

    void VehicleSoundPitchFunction()
    {
        if ((vehicleRB.velocity.magnitude > 1 || vehicleRB.velocity.magnitude < -1) && !GetComponent<AudioSource>().enabled && lifeVehicle > 0)
            GetComponent<AudioSource>().enabled = true;
        else if ((vehicleRB.velocity.magnitude <= 1 && vehicleRB.velocity.magnitude >= -1) && GetComponent<AudioSource>().enabled && lifeVehicle > 0)
            GetComponent<AudioSource>().enabled = false;

        if (GetComponent<AudioSource>().enabled)
            GetComponent<AudioSource>().pitch = (vehicleRB.velocity.magnitude * 1) / vehicleMaxSpeed/2;
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
        if (controls.Quad.Left == 0 && controls.Quad.Right == 0)
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
                if (controls.Quad.Backward && vehicleRB.velocity.x < 0)
                    vehicleRB.velocity = transform.TransformDirection(new Vector3(0, 0, -vehicleMaxSpeed));
            }
            if (vehicleRB.velocity.z > vehicleMaxSpeed)
                vehicleRB.velocity = transform.TransformDirection(new Vector3(0, 0, vehicleMaxSpeed));
            if (controls.Quad.Backward && vehicleRB.velocity.z < 0)
                vehicleRB.velocity = transform.TransformDirection(new Vector3(0, 0, -vehicleMaxSpeed));
        }
        else if (vehicleRB.velocity.z < -vehicleMaxSpeed || vehicleRB.velocity.x < -vehicleMaxSpeed)
        {
            if (!controls.Quad.Backward)
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
        if (!alaDelta && touchingGround && controls.Quad.AlaDelta)
            alaDelta = true;

        if(alaDelta && alaDeltaTimer >= 0)
        {
            alaDeltaTimer -= Time.deltaTime;
            if (alaDeltaTimer >= alaDeltaDuration - 0.6f)
            {
                transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
                vehicleRB.velocity += new Vector3(0, 1, 0);
            }
            else
            {
                transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
                if (controls.Quad.Right == 1)
                    vehicleRB.AddTorque(new Vector3(0, vehicleTorque, 0));
                else if (controls.Quad.Left == 1)
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

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Water") && !hasFloater)
        {
            StartCoroutine(LerpVehicleMaxSpeed(savedMaxSpeed * 2 / 3, 3.0f));
        }
        if(other.tag.Equals("Respawn") && !other.GetComponent<DeathfallAndCheckpointsSystem>().activated)
        {
            GameObject.Find("UI").transform.GetChild(1).GetComponent<UIPosition>().actualCheckpoint++;
            other.GetComponent<DeathfallAndCheckpointsSystem>().activated = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        StartCoroutine(WaitEndBoost());

        if(other.tag.Equals("Water") && !hasFloater)
        {
            StartCoroutine(LerpVehicleMaxSpeed(savedMaxSpeed, 1.5f));
        }

        if (other.CompareTag("Painting") || other.CompareTag("Oil"))
        {
            ResetFriction();
        }

    }

    void OnCollisionExit(Collision other)
    {
        vehicleReversed = false;
        timerReversed = 0;
    }

    //void ChangeFriction(float _dragInc, float _speedDec)
    //{
    //    vehicleRB.angularDrag = savedAngularDrag * _dragInc;
    //    vehicleMaxSpeed = savedMaxSpeed / _speedDec;
    //    if (_speedDec < 1.0f) vehicleAcceleration /= (_speedDec + (1-_speedDec)*2.0f);
    //}
    void ResetFriction()
    {
        vehicleRB.angularDrag = savedAngularDrag;
        //vehicleMaxSpeed = savedMaxSpeed;
        //vehicleAcceleration = savedAcceleration;
    }
    void AddFriction(float _frictionForce, float _dragInc)
    {
        //Vector3 frictionVec = transform.up * _frictionForce;
        Vector3 velFrictionVec = -vehicleRB.velocity.normalized * _frictionForce * vehicleRB.velocity.magnitude;
        vehicleRB.AddForce(velFrictionVec, ForceMode.Force);
        //Vector3 angularFrictionVec = -vehicleRB.angularVelocity.normalized * _frictionForce;
        //vehicleRB.AddForce(angularFrictionVec, ForceMode.Force);
        vehicleRB.angularDrag = savedAngularDrag * _dragInc;

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