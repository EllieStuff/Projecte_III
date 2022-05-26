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

    [SerializeField] bool canSpread = true;
    float finalDespawnTime;

    private void OnEnable()
    {
        finalDespawnTime = despawnTime.GetRndValue();
        StartCoroutine(DespawnCoroutine());
    }


    float GetNewTorque(PlayerVehicleScript _player)
    {
        float newTorque = 0;
        if (type == DecalType.OIL) newTorque = _player.savedVehicleTorque * 80000;
        else if (type == DecalType.PAINT) newTorque = _player.savedVehicleTorque / 2;

        return newTorque;
    }
    public void DestroyDecal()
    {
        StopAllCoroutines();
        GetComponent<Collider>().enabled = false;
        Destroy(gameObject, 0.5f);
    }

    IEnumerator DespawnCoroutine()
    {
        yield return new WaitForSeconds(finalDespawnTime);

        despawnSpeed += despawnSpeedDiff.GetRndValue();
        while (transform.localScale.x > 0.1f)
        {
            yield return new WaitForEndOfFrame();
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, Time.timeScale * despawnSpeed);
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.name.Contains("Road"))
        //{
        //    transform.localScale = transform.localScale / 4f;
        //    //DestroyDecal();
        //    //return;
        //}


        if (!canSpread) return;

        if (canSpread && (other.tag.Contains("Player") || other.tag.Equals("vehicleElement")))
        {
            PlayerVehicleScript player = other.GetComponentInParent<PlayerVehicleScript>();
            if (player == null)
            {
                player = other.GetComponent<PlayerVehicleScript>();
                if (player == null)
                {
                    player = other.transform.parent.GetComponentInParent<PlayerVehicleScript>();
                    if (player == null) return;
                }
            }

            player.reinitTorqueTimer = finalDespawnTime;
            player.targetCarTorque = GetNewTorque(player);
            if (transform.parent.tag.Contains("Player") || transform.parent.tag.Equals("vehicleElement"))
            {
                canSpread = false;
            }
        }
        else if (other.name.Contains("Decal"))
        {
            if (transform.parent.tag.Contains("Player") || transform.parent.tag.Equals("vehicleElement"))
                canSpread = false;
        }

    }

}
