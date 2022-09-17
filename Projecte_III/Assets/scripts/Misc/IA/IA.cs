using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IA : MonoBehaviour
{
    private PlayerVehicleScript vehicleScript;
    private PlayerInputs inputs;
    private Transform target;
    private PlayersManager playersManager;
    private RandomModifierGet randomModifierScript;
    private bool forward, backward, left, right;
    private bool enableMovement = false;

    public int ItemThrowProbability;
    private float throwTimer;

    private int playerNum;

    public bool parsecEnabled = true;
    PlayerVehicleScript.InitialTurbo initialTurbo;

    // Start is called before the first frame update
    void Start()
    {
        throwTimer = 5;
        inputs = GetComponent<PlayerInputs>();
        vehicleScript = GetComponent<PlayerVehicleScript>();
        playersManager = GameObject.FindGameObjectWithTag("PlayersManager").GetComponent<PlayersManager>();
        randomModifierScript = GetComponent<RandomModifierGet>();
        playerNum = transform.parent.GetComponent<PlayerData>().id;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playersManager == null)
        {
            playersManager = GameObject.FindGameObjectWithTag("PlayersManager").GetComponent<PlayersManager>();
            //enableMovement = false;
        }

        if (vehicleScript.iaEnabled)
        {
            if(target == null && !SceneManager.GetActiveScene().name.Equals("Current Building Scene"))
                target = GameObject.Find("Target").transform;

            IABrain();

            if(!enableMovement && vehicleScript.timerStartRace <= 0)
            {
                enableMovement = true;
                vehicleScript.SetInitialTurbo(initialTurbo);
            }
            if(enableMovement) IAMovement();
        }
        else if(parsecEnabled)
        {
            ParsecInputsDetect();

            if (vehicleScript.timerStartRace <= 0)
                IAMovement();

        }
    }

    public void ActivateStartTurbo()
    {
        initialTurbo = (PlayerVehicleScript.InitialTurbo)Random.Range(0, (int)PlayerVehicleScript.InitialTurbo.COUNT);
        Debug.Log("IA turbo: " + initialTurbo);
        vehicleScript.bounceScript.Activate(new Vector3(1, 1, 0));
        vehicleScript.smokeBoostParticlesStart.Play();
    }

    void ParsecInputsDetect()
    {
        forward = inputs.parsecP.forward;
        backward = inputs.parsecP.backward;
        left = inputs.parsecP.left;
        right = inputs.parsecP.right;

        randomModifierScript.IADown = inputs.parsecP.downArrow;
        randomModifierScript.IAUp = inputs.parsecP.upArrow;
        randomModifierScript.IALeft = inputs.parsecP.leftArrow;
        randomModifierScript.IARight = inputs.parsecP.rightArrow;
        randomModifierScript.IAItemTrigger = inputs.parsecP.upArrow || inputs.parsecP.downArrow || inputs.parsecP.leftArrow || inputs.parsecP.rightArrow;
    }

    void IABrain()
    {
        forward = false;
        backward = false;
        left = false;
        right = false;

        randomModifierScript.IADown = false;
        randomModifierScript.IAUp = false;
        randomModifierScript.IALeft = false;
        randomModifierScript.IARight = false;
        randomModifierScript.IAItemTrigger = false;

        Vector3 localDir = Vector3.zero;

        for (int i = 0; i < playersManager.numOfPlayers; i++)
        {
            localDir = transform.InverseTransformDirection(playersManager.GetPlayer(i).position - transform.position);

            if(localDir.x > 0 && localDir.x < 7 && localDir.z > -3 && localDir.z < 3)
            {
                left = true;
            }
            else if (localDir.x > -7 && localDir.x < 0 && localDir.z > -3 && localDir.z < 3)
            {
                right = true;
            }
        }

        Vector3 targetDir = Vector3.zero;

        if (target != null)
            targetDir = target.position - transform.position;

        localDir = transform.InverseTransformDirection(targetDir);

        if (localDir.x > 2)
        {
            right = true;
            left = false;
        }
        else if (localDir.x < -2)
        {
            left = true;
            right = false;
        }

        if (localDir.z <= 15)
        {
            forward = true;
            backward = false;
        }
        else
        {
            backward = true;
            forward = false;
        }

        throwTimer -= Time.deltaTime;

        int random = Random.Range(0, 100);

        if(throwTimer <= 0 && random <= ItemThrowProbability && randomModifierScript.canUseModifier) 
        {
            for (int i = 0; i < playersManager.numOfPlayers; i++)
            {
                localDir = transform.InverseTransformDirection(playersManager.GetPlayer(i).position - transform.position);
                
                if(localDir.x > 2) 
                {
                    randomModifierScript.IARight = true;
                    randomModifierScript.IAItemTrigger = true;
                }
                else if (localDir.x < -2)
                {
                    randomModifierScript.IALeft = true;
                    randomModifierScript.IAItemTrigger = true;
                }

                if (localDir.z > 3)
                {
                    randomModifierScript.IADown = true;
                    randomModifierScript.IAItemTrigger = true;
                }
                else if (localDir.z < -3)
                {
                    randomModifierScript.IAUp = true;
                    randomModifierScript.IAItemTrigger = true;
                }

                if (randomModifierScript.IAItemTrigger)
                    break;
            }

            throwTimer = 5;
        }
    }

    void IAMovement()
    {
        if (vehicleScript.dash || vehicleScript.dashCollided) return;
        var locVel = transform.InverseTransformDirection(vehicleScript.vehicleRB.velocity);

        bool disableReverse = (vehicleScript.vehicleMaxSpeed > vehicleScript.savedMaxSpeed);

        if (vehicleScript.speedIncrementEnabled && vehicleScript.savedMaxSpeed < vehicleScript.baseMaxSpeed)
        {
            vehicleScript.savedMaxSpeed += Time.deltaTime * 0.04f;
            if (vehicleScript.refreshMaxSpeed) vehicleScript.vehicleMaxSpeed = vehicleScript.savedMaxSpeed;
        }

        if (vehicleScript.touchingGround)
        {
            //Main Movement Keys______________________________________________________________________________________________________________________
            //Forward
            if (forward && !backward)
            {
                if (vehicleScript.vehicleRB.velocity.y <= vehicleScript.vehicleMaxSpeed / 2)
                    vehicleScript.vehicleRB.velocity += transform.TransformDirection(new Vector3(0, 0, vehicleScript.vehicleAcceleration));

                if (!right && left)
                    vehicleScript.vehicleRB.AddTorque(new Vector3(0, -vehicleScript.vehicleTorque, 0));
                else if (right && !left)
                    vehicleScript.vehicleRB.AddTorque(new Vector3(0, vehicleScript.vehicleTorque, 0));
            }
            //left Or right
            if (!forward && !backward)
            {
                //left
                if (left)
                    vehicleScript.vehicleRB.AddTorque(new Vector3(0, -vehicleScript.vehicleTorque /** left*/, 0));
                //right
                else if (right)
                    vehicleScript.vehicleRB.AddTorque(new Vector3(0, vehicleScript.vehicleTorque /** right*/, 0));
            }

            //Backwards
            if (backward && !forward && !disableReverse)
            {
                if (vehicleScript.vehicleRB.velocity.y > -vehicleScript.minDriftSpeed / 2 && vehicleScript.vehicleRB.velocity.y <= vehicleScript.vehicleMaxSpeed / 2)
                    vehicleScript.vehicleRB.velocity += transform.TransformDirection(new Vector3(0, 0, -vehicleScript.vehicleAcceleration));
                else if (vehicleScript.vehicleRB.velocity.y <= vehicleScript.vehicleMaxSpeed / 2)
                    vehicleScript.vehicleRB.velocity += transform.TransformDirection(new Vector3(0, 0, -vehicleScript.vehicleAcceleration / 10));

                if (left && !right)
                    vehicleScript.vehicleRB.AddTorque(new Vector3(0, vehicleScript.vehicleTorque, 0));
                else if (!left && right)
                    vehicleScript.vehicleRB.AddTorque(new Vector3(0, -vehicleScript.vehicleTorque, 0));
            }
            //Main Movements Keys______________________________________________________________________________________________________________________

            //Speed Regulation Function
            SpeedRegulation(disableReverse);

            vehicleScript.savedVelocity = vehicleScript.vehicleRB.velocity;
        }
        else
        {
            //Fall Function
            vehicleScript.FallFunction();
        }
        //Vehicle sound pitch function
        vehicleScript.VehicleSoundPitchFunction();
    }

    void SpeedRegulation(bool disableReverse)
    {
        if (!left && !right)
            vehicleScript.vehicleRB.angularVelocity = new Vector3(vehicleScript.vehicleRB.angularVelocity.x, 0, vehicleScript.vehicleRB.angularVelocity.z);

        if (vehicleScript.vehicleRB.angularVelocity.y > vehicleScript.vehicleMaxTorque)
        {
            vehicleScript.vehicleRB.angularVelocity = new Vector3(vehicleScript.vehicleRB.angularVelocity.x, vehicleScript.vehicleMaxTorque, vehicleScript.vehicleRB.angularVelocity.z);
        }
        else if (vehicleScript.vehicleRB.angularVelocity.y < -vehicleScript.vehicleMaxTorque)
        {
            vehicleScript.vehicleRB.angularVelocity = new Vector3(vehicleScript.vehicleRB.angularVelocity.x, -vehicleScript.vehicleMaxTorque, vehicleScript.vehicleRB.angularVelocity.z);
        }

        if (vehicleScript.vehicleRB.velocity.z > vehicleScript.vehicleMaxSpeed || vehicleScript.vehicleRB.velocity.x > vehicleScript.vehicleMaxSpeed)
        {
            if (vehicleScript.vehicleRB.velocity.x > vehicleScript.vehicleMaxSpeed && vehicleScript.vehicleRB.velocity.z < vehicleScript.vehicleMaxSpeed)
            {
                vehicleScript.vehicleRB.velocity = transform.TransformDirection(new Vector3(0, 0, vehicleScript.vehicleMaxSpeed));
                if (backward && vehicleScript.vehicleRB.velocity.x < 0)
                    vehicleScript.vehicleRB.velocity = transform.TransformDirection(new Vector3(0, 0, -vehicleScript.vehicleMaxSpeed));
            }
            if (vehicleScript.vehicleRB.velocity.z > vehicleScript.vehicleMaxSpeed)
                vehicleScript.vehicleRB.velocity = transform.TransformDirection(new Vector3(0, 0, vehicleScript.vehicleMaxSpeed));
            if (backward && vehicleScript.vehicleRB.velocity.z < 0)
                vehicleScript.vehicleRB.velocity = transform.TransformDirection(new Vector3(0, 0, -vehicleScript.vehicleMaxSpeed));
        }
        else if (vehicleScript.vehicleRB.velocity.z < -vehicleScript.vehicleMaxSpeed || vehicleScript.vehicleRB.velocity.x < -vehicleScript.vehicleMaxSpeed)
        {
            if (!backward)
                vehicleScript.vehicleRB.velocity = transform.TransformDirection(new Vector3(0, 0, vehicleScript.vehicleMaxSpeed));
            else if (!disableReverse)
            {
                vehicleScript.vehicleRB.velocity = transform.TransformDirection(new Vector3(0, 0, -vehicleScript.vehicleMaxSpeed));
                if (vehicleScript.vehicleRB.velocity.z < 0)
                    vehicleScript.vehicleRB.velocity = transform.TransformDirection(new Vector3(0, 0, -vehicleScript.vehicleMaxSpeed));
            }
        }
    }
}
