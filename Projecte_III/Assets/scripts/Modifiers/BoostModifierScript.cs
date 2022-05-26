using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostModifierScript : MonoBehaviour
{
    Rigidbody playerRB;
    PlayerVehicleScript player;

    [SerializeField] GameObject BoostPS1;
    [SerializeField] GameObject BoostPS2;

    float boostSpeed = 20.0f;
    float boostTime = 1.0f;
    float initialDrag;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        player = GetComponent<PlayerVehicleScript>();
    }

    [ContextMenu("SetBoostModifier")]
    public void SetBoostModifier()
    {
        RandomModifierGet.ModifierTypes modType = RandomModifierGet.ModifierTypes.BOOST;
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

    public void Active(Vector3 _direction, float _multiplier, float _speedMultiplier)
    {
        player.dash = true;
        float _speed = boostSpeed * _speedMultiplier;

        Vector3 dirLocal = transform.InverseTransformDirection(_direction);

        if (dirLocal == Vector3.left || dirLocal == Vector3.right)
            playerRB.velocity = transform.TransformDirection(dirLocal.x * _speed, dirLocal.y * _speed, transform.InverseTransformDirection(playerRB.velocity).z);
        else
            playerRB.velocity = _direction * _speed;

        initialDrag = playerRB.drag;
        playerRB.drag = 0;

        BoostPS1.transform.LookAt(transform.localPosition + _direction);
        BoostPS1.GetComponent<ParticleSystem>().Play();
        BoostPS2.transform.LookAt(transform.localPosition + _direction);
        BoostPS2.GetComponent<ParticleSystem>().Play();

        Debug.Log("PS rotation: " + BoostPS1.transform.localRotation);

        StartCoroutine(StopBoost(boostTime * _multiplier));
    }

    IEnumerator StopBoost(float _time)
    {
        yield return new WaitForSeconds(_time);
        
        playerRB.drag = initialDrag;

        player.dash = false;
    }
}
