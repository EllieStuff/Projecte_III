using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintBulletScript : MonoBehaviour
{
    [SerializeField] GameObject decalCarPrefab, decalFloorPrefab;
    [SerializeField] SphereCollider col;
    [SerializeField] float sizeInc = 5.0f;

    Transform originTransform = null;

    private void OnEnable()
    {
        StartCoroutine(DespawnCoroutine());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Decals.CollidingWithPlayer(other, originTransform))
        {
            DecalDefaultScript instancedPaint = GameObject.Instantiate(decalCarPrefab, transform.position, decalCarPrefab.transform.rotation, other.transform).GetComponent<DecalDefaultScript>();
            instancedPaint.transform.localScale = instancedPaint.transform.localScale * transform.localScale.x * sizeInc;

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
            player.reinitTorqueTimer = instancedPaint.finalDespawnTime;
            player.targetCarTorque = instancedPaint.GetNewTorque(player);

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


    IEnumerator DespawnCoroutine()
    {
        yield return new WaitForSeconds(10.0f);
        Destroy(gameObject);
    }

}
