using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilBulletScript : MonoBehaviour
{
    [SerializeField] GameObject decalPrefab;
    [SerializeField] SphereCollider col;
    [SerializeField] float sizeInc = 5.0f;

    private void OnEnable()
    {
        StartCoroutine(DespawnCoroutine());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerVehicle"))
        {
            // Ho silencio perque el joc peta molt si no

            //GameObject instancedGO = GameObject.Instantiate(decalPrefab, transform.position, decalPrefab.transform.rotation, other.transform);
            //instancedGO.transform.localScale = instancedGO.transform.localScale * transform.localScale.x;
            //instancedGO.tag = "Untagged";

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
            || _tag.Equals("CameraTrigger") || _tag.Equals("Untagged") || _tag.Equals("CameraObjective") || _tag.Equals("CamLimit"));
    }


    IEnumerator DespawnCoroutine()
    {
        yield return new WaitForSeconds(10.0f);
        Destroy(gameObject);
    }
}
