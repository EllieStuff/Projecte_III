using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class VehicleTriggerAndCollisionEvents : MonoBehaviour
{
    private PlayerVehicleScript player;
    private Transform centerRespawn;
    public Vector3 respawnPosition, respawnRotation, respawnVelocity;
    bool paintingChecked = false, oilChecked = false, exitCamera;
    [SerializeField] float boostPadDuration;
    private bool reduceSpeed;
    public float boostPadMultiplier;
    private Transform outTransform;
    private Rigidbody outVehicleRB;
    private PlayersManager playersManager;
    private PlayersHUDManager playersHUDManager;

    PlayersHUD playerHud = null;
    [SerializeField] private float timerRespawn = 3;
    private BoxCollider[] collisionBox;
    [SerializeField] private Material ghostMat;

    [SerializeField] private Material defaultMat;

    public Material DefaultMaterial
    {
        get { return defaultMat; }
        set { defaultMat = value; }
    }

    private MeshRenderer carRender;

    internal bool inmunity;
    internal bool infiniteLifes = false;

    private bool ghostTextureEnabled;



    public bool applyingForce = false;

    public float previousMaxSpeed;

    public ParticleSystem RespawnParticles;



    private void Start()
    {
        Init();
    }
    internal void Init()
    {
        carRender = transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>();
        carRender.material = defaultMat;
        timerRespawn = 10;
        playersManager = GameObject.FindGameObjectWithTag("PlayersManager").GetComponent<PlayersManager>();
        collisionBox = transform.GetChild(0).GetComponents<BoxCollider>();
        centerRespawn = GameObject.Find("Main Camera").transform;
        player = GetComponent<PlayerVehicleScript>();
        respawnPosition = new Vector3(0, 0, 0);
        respawnRotation = new Vector3(0, 0, 0);
        respawnVelocity = new Vector3(0, 0, 0);
        paintingChecked = oilChecked = false;
    }

    private void Update()
    {
        if (centerRespawn == null) Init();

        if (!ghostTextureEnabled && carRender.material != defaultMat)
            carRender.material = defaultMat;

        if (timerRespawn > 0 && inmunity)
        {
            player.vehicleRB.velocity = new Vector3(player.vehicleRB.velocity.x, 0, player.vehicleRB.velocity.z);

            timerRespawn -= Time.deltaTime;

            if (ghostTextureEnabled)
                carRender.material = ghostMat;

            ghostTextureEnabled = !ghostTextureEnabled;
        }
        else if(inmunity)
        {
            carRender.material = defaultMat;
            for (int i = 0; i < 4; i++)
            {
                BoxCollider[] cols = playersManager.GetPlayer(i).GetChild(0).GetComponents<BoxCollider>();

                Physics.IgnoreCollision(collisionBox[0], cols[0], false);
                Physics.IgnoreCollision(collisionBox[0], cols[1], false);
                Physics.IgnoreCollision(collisionBox[1], cols[0], false);
                Physics.IgnoreCollision(collisionBox[1], cols[1], false);

                for (int o = 0; o < player.wheelCollider.Length; o++)
                    player.wheelCollider[o].isTrigger = false;
            }
            //Debug.Log("Stopped inmunity");
            inmunity = false;
        }

        respawnPosition = new Vector3(centerRespawn.position.x, centerRespawn.position.y - 25, centerRespawn.position.z);
        respawnPosition += centerRespawn.TransformDirection(new Vector3(0, 0, 30)) + new Vector3(0, 35, 0);
        respawnRotation = centerRespawn.rotation.eulerAngles;
        if (player.vehicleReversed)
        {
            if (playerHud == null)
            {
                playerHud = GameObject.Find("HUD").transform.GetComponentInChildren<PlayersHUDManager>().GetPlayerHUD(transform.parent.GetComponent<PlayerData>().id);
            }
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
        else if(DeathScript.DeathByFalling(false, transform, player.vehicleRB, respawnPosition, respawnRotation, respawnVelocity, out outTransform, out outVehicleRB)
            || DeathScript.DeathByExitCamera(exitCamera, transform, player.vehicleRB, respawnPosition, respawnRotation, respawnVelocity, out outTransform, out outVehicleRB))
        {
            if (playerHud == null)
            {
                playerHud = GameObject.Find("HUD").transform.GetComponentInChildren<PlayersHUDManager>().GetPlayerHUD(transform.parent.GetComponent<PlayerData>().id);
            }


            transform.position = outTransform.position;
            transform.rotation = outTransform.rotation;
            player.vehicleRB.velocity = outVehicleRB.velocity;

            //Resetting variables
            player.vehicleTorque = player.savedVehicleTorque;
            oilChecked = false;
            paintingChecked = false;

            player.vehicleMaxSpeed = player.savedMaxSpeed;
            player.speedIncrementEnabled = true;
            //_______________________________________________
            if (!inmunity && !infiniteLifes) 
            player.lifes--;

            playerHud.UpdateLifes(player.lifes);

            GameObject parent = transform.parent.gameObject;

            if(player.lifes > 0)
                AudioManager.Instance.Play_SFX("Respawn_SFX");

            if (player.lifes <= 0)
            {
                AudioManager.Instance.Play_SFX("PlayerEliminated_SFX");

                if (playersHUDManager == null)
                    playersHUDManager = GameObject.Find("HUD").transform.Find("Players").GetComponent<PlayersHUDManager>();

                playersHUDManager.GetPlayerHUD(player.playerNum).transform.Find("CurrentModifier").GetComponent<Image>().color -= new Color(0, 0, 0, 0.3f);

                parent.SetActive(false);
            }
            else
            {
                RespawnParticles.Play();
                //GetComponent<ParticleSystem>().Play();
                //parent.GetComponent<AudioSource>().Play();
                if(!inmunity)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        BoxCollider[] cols = playersManager.GetPlayer(i).GetChild(0).GetComponents<BoxCollider>();

                        Physics.IgnoreCollision(collisionBox[0], cols[0], true);
                        Physics.IgnoreCollision(collisionBox[0], cols[1], true);
                        Physics.IgnoreCollision(collisionBox[1], cols[0], true);
                        Physics.IgnoreCollision(collisionBox[1], cols[1], true);

                        for (int o = 0; o < player.wheelCollider.Length; o++)
                            player.wheelCollider[o].isTrigger = true;
                    }

                    carRender.material = ghostMat;
                    ghostTextureEnabled = true;

                    timerRespawn = 3;

                    inmunity = true;
                }
            }

            if (exitCamera)
                exitCamera = false;
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

    public void ApplyForce(float forceValue, float _seconds)
    {

        player.speedIncrementEnabled = false;

        player.vehicleMaxSpeed = forceValue;

        StartCoroutine(ResetVelocity(_seconds));

    }

    private void FixedUpdate()

    {



    }

    public IEnumerator ResetVelocity(float _seconds)

    {

        yield return new WaitForSeconds(_seconds);

        player.vehicleMaxSpeed = player.savedMaxSpeed;

        player.speedIncrementEnabled = true;

        Debug.Log("Velocity reseted");

    }

    void OnCollisionStay(Collision other)
    {
        //------Player Death------
        player.vehicleReversed = (other.gameObject.tag.Equals("ground"));
        //------------------------
    }
    IEnumerator WaitTorque()
    {
        yield return new WaitForSeconds(5);
        player.vehicleTorque = player.savedVehicleTorque;
        oilChecked = false;
        paintingChecked = false;
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

        if (other.CompareTag("CamLimit") && other.name.Equals("Backward"))
        {
            exitCamera = true;
        }
        else if (other.CompareTag("CamLimit"))
        {
            if (player.vehicleRB.velocity.x > 10)
                player.vehicleRB.velocity = new Vector3(10.0f, 0, 0);
            else if (player.vehicleRB.velocity.z > 10)
                player.vehicleRB.velocity = new Vector3(0, 0, 10.0f);
        }
        if (other.CompareTag("TopCollider"))
            player.vehicleRB.velocity += transform.TransformDirection(Vector3.back * Time.deltaTime * 5);

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
                //Debug.Log("enter Paint");
                AddFriction(player.savedVehicleTorque / 2);
            }
        }
        if (!oilChecked && other.CompareTag("Oil"))
        {
            oilChecked = true;
            if (player.vehicleRB.velocity.magnitude > 1.0f)
            {
                //Debug.Log("enter Oil");
                AddFriction(player.savedVehicleTorque*100000);
            }
        }

        //Terrain
        if (other.CompareTag("Sand") && player.touchingGround)
            OnSand(other);

    }

    private void OnTriggerExit(Collider other)
    {
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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag.Contains("Player") && player.dash)
        {
            PlayerVehicleScript otherPlayer = collision.transform.GetComponent<PlayerVehicleScript>();
            otherPlayer.dashCollided = true;
            otherPlayer.vehicleRB.velocity = player.vehicleRB.velocity * 2.0f;
            otherPlayer.GetComponent<VehicleTriggerAndCollisionEvents>().ResetDash(otherPlayer);

        }
    }

    public void ResetDash(PlayerVehicleScript _other)
    {
        StartCoroutine(ResetCollision(_other));
    }

    IEnumerator ResetCollision(PlayerVehicleScript _other)
    {
        yield return new WaitForSeconds(1.0f);
        _other.dashCollided = false;

    }

    void OnCollisionExit(Collision other)
    {
        player.vehicleReversed = false;
        player.timerReversed = 0;
    }

    void ResetFriction()
    {
        //player.vehicleRB.angularDrag = player.savedAngularDrag;
        player.vehicleTorque = player.savedVehicleTorque;
    }

    void AddFriction(float _torque)
    {
        //Vector3 velFrictionVec = -player.vehicleRB.velocity.normalized * _frictionForce * player.vehicleRB.velocity.magnitude;
        //player.vehicleRB.AddForce(velFrictionVec, ForceMode.Force);
        //player.vehicleRB.angularDrag = player.savedAngularDrag * _dragInc;
        player.vehicleTorque = _torque;
        StartCoroutine(WaitTorque());
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
