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


    private void OnEnable()
    {
    }

    bool PlayerAlreadyAffected(PlayerVehicleScript _player)
    {
        if (type == DecalType.OIL) return _player.affectedByOil;
        else if (type == DecalType.PAINT) return _player.affectedByPaint;

        return false;
    }
    void AffectPlayer(PlayerVehicleScript _player, bool _affected)
    {
        if (_affected) _player.vehicleTorque = GetNewTorque(_player);

        if (type == DecalType.OIL)
        {
            _player.affectedByOil = _affected;
            if (!_affected && !_player.affectedByPaint)
                _player.vehicleTorque = _player.savedVehicleTorque;
        }
        else if (type == DecalType.PAINT)
        {
            _player.affectedByPaint = _affected;
            if (!_affected && !_player.affectedByOil)
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


    IEnumerator DespawnCoroutine()
    {
        yield return new WaitForSeconds(despawnTime.GetRndValue());

        despawnSpeed += despawnSpeedDiff.GetRndValue();
        while(transform.localScale.x > 0.1f)
        {
            yield return new WaitForEndOfFrame();
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, Time.timeScale * despawnSpeed);
        }

        Destroy(gameObject);
    }

    IEnumerator WaitTorque(PlayerVehicleScript _player)
    {
        float timer = 5f;
        while (timer > 0f)
        {
            yield return new WaitForEndOfFrame();
            if (!PlayerAlreadyAffected(_player)) break;
            timer -= Time.deltaTime;
        }
        AffectPlayer(_player, false);
        //_player.vehicleTorque = _player.savedVehicleTorque;
        //oilInChecked = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (canSpread && other.tag.Contains("Player"))
        {
            PlayerVehicleScript player = other.GetComponent<PlayerVehicleScript>();
            if (PlayerAlreadyAffected(player)) return;

            AffectPlayer(player, true);
            //player.vehicleTorque = player.savedVehicleTorque;
            if (transform.parent.tag.Contains("Player"))
            {
                canSpread = false;
                StartCoroutine(WaitTorque(player));
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Contains("Player"))
        {
            PlayerVehicleScript player = other.GetComponent<PlayerVehicleScript>();

        }
    }


}
