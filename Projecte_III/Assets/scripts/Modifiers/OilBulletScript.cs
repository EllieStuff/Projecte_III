using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilBulletScript : MonoBehaviour
{
    [SerializeField] GameObject decalPrefab;
    [SerializeField] SphereCollider col;
    [SerializeField] float sizeInc = 5.0f;

    Transform originTransform = null;

    private void OnEnable()
    {
        StartCoroutine(DespawnCoroutine());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (CollidingWithPlayer(other))
        {
            // Ho silencio perque el joc peta molt si no

            GameObject instancedGO = GameObject.Instantiate(decalPrefab, transform.position, decalPrefab.transform.rotation, other.transform);
            instancedGO.transform.localScale = instancedGO.transform.localScale * transform.localScale.x;
            //instancedGO.tag = "Untagged";
            other.GetComponentInParent<Rigidbody>().AddExplosionForce(1000, transform.position, col.radius * transform.localScale.x);

        }
        else
        {
            if (!TagToIgnore(other.tag))
            {
                //Vector3 closesPoint = other.ClosestPoint(transform.position);
                //Vector3 spawnPoint = closesPoint + ((transform.position - closesPoint).normalized * decalPrefab.transform.localScale.x);
                GameObject instancedGO = GameObject.Instantiate(decalPrefab, transform.position, decalPrefab.transform.rotation, other.transform);
                instancedGO.transform.localScale = instancedGO.transform.localScale * transform.localScale.x * sizeInc;

                Destroy(gameObject);

            }

        }

    }


    bool TagToIgnore(string _tag)
    {
        return (_tag.Equals("Decal") || _tag.Equals("Painting") || _tag.Equals("Oil") || _tag.Equals("Respawn") 
            || _tag.Equals("CameraTrigger") || _tag.Equals("Untagged") || _tag.Equals("CameraObjective") || _tag.Equals("CamLimit")
            || _tag.Equals("PlayerVehicle"));
    }
    bool CollidingWithPlayer(Collider other)
    {
        return other.CompareTag("PlayerVehicle") && originTransform != other.transform;
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
