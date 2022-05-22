using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecalDefaultScript : MonoBehaviour
{
    public enum DecalType { OIL, PAINT };
    [SerializeField] DecalType type;
    [SerializeField] Utils.MinMaxFloat despawnTime;
    [SerializeField] float despawnSpeed = 0.003f;
    [SerializeField] Utils.MinMaxFloat despawnSpeedDiff = new Utils.MinMaxFloat(-0.002f, 0.002f);

    bool canSpread = true;
    Dictionary<Transform, PlayerVehicleScript> lastTouchedPlayers = new Dictionary<Transform, PlayerVehicleScript>();

    private void OnEnable()
    {
        StartCoroutine(DespawnCoroutine());
    }


    bool PlayerAlreadyAffected(PlayerVehicleScript _player)
    {
        if (type == DecalType.OIL) return AffectedByOil(_player);
        else if (type == DecalType.PAINT) return AffectedByPaint(_player);

        return false;
    }
    void AffectPlayer(PlayerVehicleScript _player)
    {
        _player.vehicleTorque = GetNewTorque(_player);

        if (type == DecalType.OIL)
        {
            _player.oilObstacles.Push(gameObject);
        }
        else if (type == DecalType.PAINT)
        {
            _player.paintObstacles.Push(gameObject);
        }
    }
    void ReleasePlayer(PlayerVehicleScript _player)
    {
        if (type == DecalType.OIL)
        {
            if(AffectedByOil(_player))
                _player.oilObstacles.Pop();
        }
        else if (type == DecalType.PAINT)
        {
            if (AffectedByPaint(_player))
                _player.paintObstacles.Pop();
        }

        if (!AffectedByOil(_player) && !AffectedByPaint(_player))
            _player.vehicleTorque = _player.savedVehicleTorque;
    }
    float GetNewTorque(PlayerVehicleScript _player)
    {
        float newTorque = 0;
        if (type == DecalType.OIL) newTorque = _player.savedVehicleTorque * 80000;
        else if (type == DecalType.PAINT) newTorque = _player.savedVehicleTorque / 2;

        return newTorque;
    }
    bool AffectedByOil(PlayerVehicleScript _player) => _player.oilObstacles.Count > 0;
    bool AffectedByPaint(PlayerVehicleScript _player) => _player.paintObstacles.Count > 0;


    IEnumerator DespawnCoroutine()
    {
        yield return new WaitForSeconds(despawnTime.GetRndValue());

        despawnSpeed += despawnSpeedDiff.GetRndValue();
        while(transform.localScale.x > 0.1f)
        {
            yield return new WaitForEndOfFrame();
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, Time.timeScale * despawnSpeed);
        }
        GetComponent<Collider>().enabled = false;
        if (lastTouchedPlayers.Count > 0)
        {
            foreach(PlayerVehicleScript playerValue in lastTouchedPlayers.Values)
                ReleasePlayer(playerValue);
        }

        Destroy(gameObject);
    }

    //IEnumerator WaitTorque(PlayerVehicleScript _player)
    //{
    //    float timer = 5f;
    //    while (timer > 0f)
    //    {
    //        yield return new WaitForEndOfFrame();
    //        if (!PlayerAlreadyAffected(_player)) break;
    //        timer -= Time.deltaTime;
    //    }
    //    ReleasePlayer(_player);
    //    //_player.vehicleTorque = _player.savedVehicleTorque;
    //    //oilInChecked = false;
    //}


    private void OnTriggerEnter(Collider other)
    {
        if (canSpread && other.tag.Contains("Player"))
        {
            PlayerVehicleScript player = other.GetComponent<PlayerVehicleScript>();
            if (PlayerAlreadyAffected(player)) return;

            AffectPlayer(player);
            lastTouchedPlayers.Add(player.transform, player);
            //player.vehicleTorque = player.savedVehicleTorque;
            if (transform.parent.tag.Contains("Player"))
            {
                canSpread = false;
                //StartCoroutine(WaitTorque(player));
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Contains("Player"))
        {
            PlayerVehicleScript player = other.GetComponent<PlayerVehicleScript>();
            ReleasePlayer(player);
            lastTouchedPlayers.Remove(player.transform);
        }
    }


}
