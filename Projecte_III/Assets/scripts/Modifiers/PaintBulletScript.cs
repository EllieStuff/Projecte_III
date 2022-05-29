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
            // Ho silencio perque el joc peta molt si no

            //GameObject instancedGO = GameObject.Instantiate(decalPrefab, transform.position, decalPrefab.transform.rotation, other.transform);
            //instancedGO.transform.localScale = instancedGO.transform.localScale * transform.localScale.x;
            //instancedGO.tag = "Untagged";
            GameObject instancedGO = GameObject.Instantiate(decalCarPrefab, transform.position, decalCarPrefab.transform.rotation, other.transform);
            instancedGO.transform.localScale = instancedGO.transform.localScale * transform.localScale.x * sizeInc;
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
            player.reinitTorqueTimer = 6f;
            player.targetCarTorque = player.savedVehicleTorque / 2;

            //instancedGO.GetComponent<Collider>().enabled = false;

            //Destroy(gameObject);
        }
        else
        {
            if (!Decals.TagToIgnore(other.tag))
            {
                //Vector3 closesPoint = other.ClosestPoint(transform.position);
                //Vector3 spawnPoint = closesPoint + ((transform.position - closesPoint).normalized * decalPrefab.transform.localScale.x);
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
