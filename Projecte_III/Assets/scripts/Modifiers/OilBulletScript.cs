using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilBulletScript : MonoBehaviour
{
    [SerializeField] GameObject decalCarPrefab, decalFloorPrefab;
    [SerializeField] SphereCollider col;
    [SerializeField] float sizeInc = 5.0f;

    Transform originTransform = null;

    private void OnEnable()
    {
        Destroy(gameObject, 10.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Decals.CollidingWithPlayer(other, originTransform))
        {
            DecalDefaultScript instancedOil = GameObject.Instantiate(decalCarPrefab, transform.position, decalCarPrefab.transform.rotation, other.transform).GetComponent<DecalDefaultScript>();
            instancedOil.transform.localScale = instancedOil.transform.localScale * transform.localScale.x;

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
            player.reinitTorqueTimer = instancedOil.finalDespawnTime;
            player.targetCarTorque = instancedOil.GetNewTorque(player);

        }
        else
        {
            if (!Decals.TagToIgnore(other.tag))
            {
                GameObject instancedGO = GameObject.Instantiate(decalFloorPrefab, transform.position, decalFloorPrefab.transform.rotation, other.transform);
                instancedGO.transform.localScale = instancedGO.transform.localScale * transform.localScale.x * sizeInc;

                Destroy(gameObject);

            }

        }

    }


    public void SetOriginTransform(Transform _transform)
    {
        originTransform = _transform;
    }

}
