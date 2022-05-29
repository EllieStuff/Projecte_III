using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class VehicleTriggerAndCollisionEvents : MonoBehaviour
{
    private PlayerVehicleScript player;
    [SerializeField] private Transform centerRespawn;
    public Vector3 respawnPosition, respawnRotation, respawnVelocity;
    bool exitCamera;
    [SerializeField] float boostPadDuration;
    private bool reduceSpeed;
    public float boostPadMultiplier;
    private Transform outTransform;
    private Rigidbody outVehicleRB;
    private PlayersManager playersManager;
    private PlayersHUDManager playersHUDManager;

    PlayersHUD playerHud = null;
    [SerializeField] private float timerRespawn = 3;
    public float TimerRespawn
    {
        get { return timerRespawn; }
    }

    private BoxCollider[] collisionBox;
    [SerializeField] private Material ghostMat;

    [SerializeField] private Material defaultMat;

    bool stopGhost = true;
    public bool StopGhost{
        set { stopGhost = value; } 
    }

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
        carRender = transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>();
        carRender.material = defaultMat;
        if (Physics.GetIgnoreLayerCollision(3, 9))
            Physics.IgnoreLayerCollision(3, 9);
        //Init();
    }
    internal void Init()
    {
        timerRespawn = 10;
        centerRespawn = GameObject.FindGameObjectWithTag("CameraObjective").transform;

        playersManager = GameObject.FindGameObjectWithTag("PlayersManager").GetComponent<PlayersManager>();
        collisionBox = transform.GetChild(0).GetComponents<BoxCollider>();
        player = GetComponent<PlayerVehicleScript>();
        respawnPosition = new Vector3(0, 0, 0);
        respawnRotation = new Vector3(0, 0, 0);
        respawnVelocity = new Vector3(0, 0, 0);
    }

    void SetLayerRecursively(Transform _obj, int layer)
    {
        _obj.gameObject.layer = layer;
        Transform[] _children = _obj.GetComponentsInChildren<Transform>(true);
        foreach (var item in _children)
        {
            if (item.gameObject.layer == 3 || item.gameObject.layer == 9)
                item.gameObject.layer = layer;
        }
    }

    private void Update()
    {

        if (!ghostTextureEnabled && carRender.material != defaultMat)
            carRender.material = defaultMat;

        if (centerRespawn == null) return;

        if (playersHUDManager == null)
            playersHUDManager = GameObject.Find("HUD").transform.GetComponentInChildren<PlayersHUDManager>();

        if (timerRespawn > 0 && inmunity)
        {
           // player.vehicleRB.velocity = new Vector3(player.vehicleRB.velocity.x, 0, player.vehicleRB.velocity.z);

            timerRespawn -= Time.deltaTime;

            if (ghostTextureEnabled)
                carRender.material = ghostMat;

            ghostTextureEnabled = !ghostTextureEnabled;
        }
        else if(inmunity && stopGhost)
        {
            carRender.material = defaultMat;

            SetLayerRecursively(transform, 3);

            //Debug.Log("Stopped inmunity");
            inmunity = false;
        }

        respawnPosition = centerRespawn.position;
        respawnRotation = centerRespawn.rotation.eulerAngles;

        if (player.vehicleReversed)
        {
            if (playerHud == null)
            {
                playerHud = playersHUDManager.GetPlayerHUD(transform.parent.GetComponent<PlayerData>().id);
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
                playerHud = playersHUDManager.GetPlayerHUD(transform.parent.GetComponent<PlayerData>().id);
            }

            //Erase decals
            Debug.Log(player.playerNum);
            Transform chasis = player.transform.Find("vehicleChasis");
            for(int i = 0; i < chasis.childCount; i++)
            {
                DecalDefaultScript child = chasis.GetChild(i).GetComponent<DecalDefaultScript>();
                if (child != null)
                    child.DestroyDecal();
            }
            Transform wheelsFather = player.transform.Find("Wheels Colliders");
            for(int i = 0; i < wheelsFather.childCount; i++)
            {
                Transform currWheel = wheelsFather.GetChild(i);
                for(int j = 0; j < currWheel.childCount; j++)
                {
                    DecalDefaultScript child = currWheel.GetChild(j).GetComponent<DecalDefaultScript>();
                    if (child != null)
                        child.DestroyDecal();
                }
            }
            player.targetFloorTorque = player.targetCarTorque = player.reinitTorqueTimer = -1;
            player.votesForMaintingFloorTorque = 0;
            //player.vehicleTorque = player.savedVehicleTorque;
            //player.oilObstacles.Clear();
            //player.paintObstacles.Clear();


            transform.position = outTransform.position;
            transform.rotation = outTransform.rotation;
            player.vehicleRB.velocity = outVehicleRB.velocity;

            //Resetting variables
            player.vehicleTorque = player.savedVehicleTorque;

            player.vehicleMaxSpeed = player.savedMaxSpeed;
            player.speedIncrementEnabled = true;
            //_______________________________________________
            if (!inmunity && !infiniteLifes) 
                player.lifes--;

            playerHud.UpdateLifes(player.lifes);

            if(player.lifes > 0)
                AudioManager.Instance.Play_SFX("Respawn_SFX");

            if (player.lifes <= 0)
            {
                AudioManager.Instance.Play_SFX("PlayerEliminated_SFX");

                playersHUDManager.GetPlayerHUD(player.playerNum).transform.Find("CurrentModifier").GetComponent<Image>().color -= new Color(0, 0, 0, 0.3f);

                transform.parent.gameObject.SetActive(false);
            }
            else
            {
                RespawnParticles.Play();
                //GetComponent<ParticleSystem>().Play();
                //parent.GetComponent<AudioSource>().Play();
                if(!inmunity)
                {
                    SetLayerRecursively(transform, 9);

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

        //ResetTorqueAfterDecal();


        ///End of Update
    }

    public void ApplyForce(float forceValue, float _seconds)
    {

        player.speedIncrementEnabled = false;

        player.vehicleMaxSpeed = forceValue;

        StartCoroutine(ResetVelocity(_seconds));

    }

    public IEnumerator ResetVelocity(float _seconds)

    {

        yield return new WaitForSeconds(_seconds);

        player.vehicleMaxSpeed = player.savedMaxSpeed;

        player.speedIncrementEnabled = true;

        Debug.Log("Velocity reseted");

    }

    //void ResetTorqueAfterDecal()
    //{
    //    Debug.Log("Player 1 paints; " + player.paintObstacles.Count);
    //    if(player.vehicleTorque != player.savedVehicleTorque 
    //        && player.paintObstacles.Count == 0 && player.oilObstacles.Count == 0)
    //    {
    //        player.vehicleTorque = player.savedVehicleTorque;
    //    }
    //}


    void OnCollisionStay(Collision other)
    {
        //------Player Death------
        player.vehicleReversed = (other.gameObject.tag.Equals("ground"));
        //------------------------
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
        if (other.CompareTag("TopCollider"))
            player.vehicleRB.velocity += transform.TransformDirection(Vector3.back * Time.deltaTime * 5);


        //Terrain
        if (other.CompareTag("Sand") && player.touchingGround)
            OnSand(other);

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

    //void ResetFriction()
    //{
    //    //player.vehicleRB.angularDrag = player.savedAngularDrag;
    //    player.vehicleTorque = player.savedVehicleTorque;
    //}

    //void AddFriction(float _torque, string _type)
    //{
    //    //Vector3 velFrictionVec = -player.vehicleRB.velocity.normalized * _frictionForce * player.vehicleRB.velocity.magnitude;
    //    //player.vehicleRB.AddForce(velFrictionVec, ForceMode.Force);
    //    //player.vehicleRB.angularDrag = player.savedAngularDrag * _dragInc;
    //    player.vehicleTorque = _torque;
    //    if(_type == "Oil")
    //        StartCoroutine(WaitTorqueOil());
    //    else if(_type == "Paint")
    //        StartCoroutine(WaitTorquePaint());
    //}

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
