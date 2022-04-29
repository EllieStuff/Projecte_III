using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaltoBomba : MonoBehaviour
{
    private PlayerVehicleScript player;
    private PlayersManager playersManager;
    private PlayerInputs inputs;
    private BoxCollider collider;
    internal bool hasSalto;
    private bool saltoEnabled;
    private bool explosionDone;
    [SerializeField] private float saltoDuration = 5;
    [SerializeField] private float explosionRange = 5;
    [SerializeField] private float saltoTimer;
    Quaternion savedRot;
    public void Init(bool _active)
    {
        hasSalto = _active;
    }

    public void Activate()
    {
        if (hasSalto && player.touchingGround)
        {
            savedRot = transform.rotation;
            explosionDone = false;
            saltoEnabled = true;
            player.vehicleRB.velocity = new Vector3(player.vehicleRB.velocity.x, 20, player.vehicleRB.velocity.z);
        }
    }

    private void Start()
    {
        collider = transform.GetChild(0).GetComponent<BoxCollider>();
        playersManager = GameObject.Find("PlayersManager").GetComponent<PlayersManager>();
        player = GetComponent<PlayerVehicleScript>();
        inputs = GetComponent<PlayerInputs>();
        saltoTimer = saltoDuration;
    }

    private void FixedUpdate()
    {
        if (hasSalto)
        {
            SaltoUpdate();
        }
    }

    private void SaltoUpdate()
    {
        if(saltoEnabled)
        {
            transform.rotation = new Quaternion(savedRot.x, transform.rotation.y, savedRot.z, transform.rotation.w);
            saltoTimer -= Time.deltaTime;
            if(saltoTimer <= 0)
            {
                saltoTimer = saltoDuration;
                saltoEnabled = false;
            }
            else if(!explosionDone && saltoTimer <= saltoDuration - 0.5f && player.touchingGround)
            {
                for (int i = 0; i < playersManager.players.Length; i++)
                {
                    Transform otherPlayer = playersManager.GetPlayer(i);
                    float explosionDistance = Vector3.Distance(transform.position, otherPlayer.transform.position);
                    Debug.Log(explosionDistance);
                    if (transform != otherPlayer && explosionDistance <= explosionRange)
                    {
                        otherPlayer.GetComponent<PlayerVehicleScript>().vehicleRB.velocity = new Vector3(0, (explosionRange - explosionDistance) * 5, 0);
                    }
                }

                saltoTimer = saltoDuration;
                explosionDone = true;
            }
            else if(!player.touchingGround)
            {
                transform.position += transform.TransformDirection(Vector3.forward * Time.deltaTime * 10);
            }
        }
    }
}
