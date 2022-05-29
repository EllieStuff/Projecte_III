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


    [HideInInspector] public float finalDespawnTime = 8;
    float timer = 0;

    private void OnEnable()
    {
        finalDespawnTime = despawnTime.GetRndValue();
        despawnSpeed += despawnSpeedDiff.GetRndValue();
    }

    private void Update()
    {
        if (timer > finalDespawnTime)
        {
            float actualSpeed = despawnSpeed * Time.deltaTime;
            transform.localScale =
                new Vector3(transform.localScale.x - actualSpeed, transform.localScale.y - actualSpeed, transform.localScale.z - actualSpeed);

            if (transform.localScale.x < 0.1f)
                Destroy(gameObject);
        }
        else
        {
            timer += Time.deltaTime;
        }

    }


    public float GetNewTorque(PlayerVehicleScript _player)
    {
        float newTorque = 0;
        if (type == DecalType.OIL) newTorque = _player.savedVehicleTorque * 80000;
        else if (type == DecalType.PAINT) newTorque = _player.savedVehicleTorque / 2;

        return newTorque;
    }
    public void DestroyDecal()
    {
        GetComponent<Collider>().enabled = false;
        Destroy(gameObject, 0.5f);
    }

}
