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

    private void Update()
    {
        if (lastTouchedPlayers.Count > 0)
        {
            Debug.Log("Touched Players: " + lastTouchedPlayers.Count);
            foreach(PlayerVehicleScript playerValue in lastTouchedPlayers.Values)
            {
                Debug.Log("Player " + playerValue.playerNum + " has " 
                    + playerValue.oilObstacles.Count + " oils and " + playerValue.paintObstacles.Count + " paints");
                Debug.Log("Player's " + playerValue.playerNum + " torque is " + playerValue.vehicleTorque);
            }
        }
    }


    bool PlayerAffectedByThis(PlayerVehicleScript _player)
    {
        if (type == DecalType.OIL) return AffectedByOil(_player);
        else if (type == DecalType.PAINT) return AffectedByPaint(_player);

        return false;
    }
    void AddToDictionaries(PlayerVehicleScript _player)
    {
        if (!lastTouchedPlayers.ContainsKey(_player.transform))
            lastTouchedPlayers.Add(_player.transform, _player);

        if (type == DecalType.OIL)
        {
            if(!_player.oilObstacles.ContainsKey(transform))
                _player.oilObstacles.Add(transform, this);
        }
        else if (type == DecalType.PAINT)
        {
            if (!_player.paintObstacles.ContainsKey(transform))
                _player.paintObstacles.Add(transform, this);
        }
    }
    void RemoveFromDictionaries(PlayerVehicleScript _player)
    {
        if (lastTouchedPlayers.ContainsKey(_player.transform)) 
            lastTouchedPlayers.Remove(_player.transform);

        if (type == DecalType.OIL)
        {
            if (_player.oilObstacles.ContainsKey(_player.transform))
                _player.oilObstacles.Remove(transform);
        }
        else if (type == DecalType.PAINT)
        {
            if (_player.paintObstacles.ContainsKey(_player.transform))
                _player.paintObstacles.Remove(transform);
        }

        if (!AffectedByOil(_player) && !AffectedByPaint(_player))
        {
            //Debug.Break();
            _player.vehicleTorque = _player.savedVehicleTorque;
        }
    }
    float GetNewTorque(PlayerVehicleScript _player)
    {
        float newTorque = 0;
        if (type == DecalType.OIL) newTorque = _player.savedVehicleTorque * 80000;
        else if (type == DecalType.PAINT) newTorque = _player.savedVehicleTorque / 2;

        return newTorque;
    }
    public void DestroyByPlayerDeath(PlayerVehicleScript _player)
    {
        StopAllCoroutines();
        GetComponent<Collider>().enabled = false;
        _player.vehicleTorque = _player.savedVehicleTorque;
        Destroy(gameObject, 0.5f);
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
            foreach (PlayerVehicleScript playerValue in lastTouchedPlayers.Values)
            {
                if (type == DecalType.OIL && !AffectedByPaint(playerValue))
                {
                    playerValue.oilObstacles.Clear();
                    playerValue.vehicleTorque = playerValue.savedVehicleTorque;
                }
                else if (type == DecalType.PAINT && !AffectedByOil(playerValue))
                {
                    playerValue.paintObstacles.Clear();
                    playerValue.vehicleTorque = playerValue.savedVehicleTorque;
                }
            }
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
        if (other.tag.Contains("Player"))
        {
            PlayerVehicleScript player = other.GetComponentInParent<PlayerVehicleScript>();
            AddToDictionaries(player);
            //if (!canSpread || PlayerAffectedByThis(player)) return;

            //AffectPlayer(player);
            player.vehicleTorque = GetNewTorque(player);
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
            PlayerVehicleScript player = other.GetComponentInParent<PlayerVehicleScript>();
            RemoveFromDictionaries(player);
        }
    }


}
