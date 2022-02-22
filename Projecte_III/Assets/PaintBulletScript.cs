using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintBulletScript : MonoBehaviour
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

        }
        else
        {
            if (!(other.CompareTag("Decal") || other.CompareTag("Painting")))
            {
                Vector3 closesPoint = other.ClosestPoint(transform.position);
                Vector3 spawnPoint = closesPoint + ((transform.position - closesPoint).normalized * decalPrefab.transform.localScale.x);
                GameObject instancedGO = GameObject.Instantiate(decalPrefab, transform.position, decalPrefab.transform.rotation, other.transform);
                instancedGO.transform.localScale = instancedGO.transform.localScale * transform.localScale.x * sizeInc;

                Destroy(gameObject);

            }

        }

    }


    IEnumerator DespawnCoroutine()
    {
        yield return new WaitForSeconds(10.0f);
        Destroy(gameObject);
    }

}
