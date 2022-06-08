using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaltoBomba : MonoBehaviour
{
    private PlayerVehicleScript player;
    private PlayersManager playersManager;
    private PlayerInputs inputs;
    private BoxCollider collider;
    internal bool hasJumped;
    private bool saltoEnabled;
    private bool explosionDone;
    private bool fallSoundPlayed;
    [SerializeField] private float saltoDuration = 5;
    [SerializeField] private float explosionRange = 5;
    [SerializeField] private float saltoTimer;

    [SerializeField] GameObject explosionRadiusPrefab;
    RadiusBlink radius;

    [SerializeField] ParticleSystem SaltoBombaPS;

    Quaternion savedRot;
    public void Init(bool _active)
    {
        hasJumped = _active;
    }

    public void Activate()
    {
        if (hasJumped && player.touchingGround)
        {
            AudioManager.Instance.Play_SFX("JumpSpring_SFX");

            savedRot = transform.rotation;
            explosionDone = false;
            saltoEnabled = true;
            fallSoundPlayed = false;
            player.vehicleRB.velocity = new Vector3(player.vehicleRB.velocity.x, 20, player.vehicleRB.velocity.z);

            if (radius == null)
            {
                radius = Instantiate(explosionRadiusPrefab).GetComponent<RadiusBlink>();
                radius.transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
                radius.SetBlink(true, saltoDuration);
            }
        }
    }

    [ContextMenu("SetSaltoBombaModifier")]
    public void SetSaltoBombaModifier()
    {
        RandomModifierGet.ModifierTypes modType = RandomModifierGet.ModifierTypes.SALTO_BOMBA;
        RandomModifierGet modGetter = GetComponent<RandomModifierGet>();
        modGetter.ResetModifiers();
        modGetter.SetModifier(modType);

        try
        {
            int playerId = GetComponentInParent<PlayerData>().id;
            GameObject.Find("HUD").GetComponentInChildren<PlayersHUDManager>().GetPlayerHUD(playerId).SetModifierImage((int)modType);
        }
        catch { Debug.LogError("PlayersHUD not found"); }
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
        if (saltoEnabled)
        {
            SaltoUpdate();

            if(radius != null) radius.transform.position = new Vector3(transform.position.x, 0.16f, transform.position.z);
        }
    }

    private void SaltoUpdate()
    {
        transform.rotation = new Quaternion(savedRot.x, transform.rotation.y, savedRot.z, transform.rotation.w);
        saltoTimer -= Time.deltaTime;
        if (saltoTimer <= 0)
        {
            if (radius != null)
                Destroy(radius.gameObject);

            saltoTimer = saltoDuration;
            saltoEnabled = false;
        }
        else if (!explosionDone && saltoTimer <= saltoDuration - 1.2f && player.touchingGround)
        {
            SaltoBombaPS.Play();
            for (int i = 0; i < playersManager.players.Length; i++)
            {
                Transform otherPlayer = playersManager.GetPlayer(i);
                float explosionDistance = Vector3.Distance(transform.position, otherPlayer.transform.position);
                //Debug.Log(explosionDistance);
                if (transform != otherPlayer && explosionDistance <= explosionRange)
                {
                    otherPlayer.GetComponent<PlayerVehicleScript>().vehicleRB.velocity = new Vector3(0, (explosionRange - explosionDistance) * 8, 0);
                }
            }

            if (radius != null)
                Destroy(radius.gameObject);

            saltoTimer = saltoDuration;
            explosionDone = true;
        }
        else if (!player.touchingGround)
        {
            transform.position += transform.TransformDirection(Vector3.forward * Time.deltaTime * 10);
        }

        if (player.vehicleRB.velocity.y <= 1.2f && !fallSoundPlayed)
        {
            AudioManager.Instance.Play_SFX("JumpFall_SFX");
            fallSoundPlayed = true;
        }
    }
}
