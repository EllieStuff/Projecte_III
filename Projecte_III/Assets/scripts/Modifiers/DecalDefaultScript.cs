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
        _player.vehicleTorque = GetNewTorque(_player);
        if (type == DecalType.OIL) _player.affectedByOil = _affected;
        else if (type == DecalType.PAINT) _player.affectedByPaint = _affected;
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

    IEnumerator WaitTorque()
    {
        float timer = 5f;
        while (timer > 0f)
        {
            yield return new WaitForEndOfFrame();
            if (!affectedByOil) break;
            timer -= Time.deltaTime;
        }
        player.vehicleTorque = player.savedVehicleTorque;
        oilInChecked = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(canSpread && other.tag.Contains("Player"))
        {
            PlayerVehicleScript player = other.GetComponent<PlayerVehicleScript>();
            if (PlayerAlreadyAffected(player)) return;
            
            AffectPlayer(player, true);
            //player.vehicleTorque = player.savedVehicleTorque;
            if (transform.parent.tag.Contains("Player"))
            {
                canSpread = false;

            }
    }


}
