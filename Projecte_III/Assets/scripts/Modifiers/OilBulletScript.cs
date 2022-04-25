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
        Destroy(gameObject, 10.0f);
        //StartCoroutine(DespawnCoroutine());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Decals.CollidingWithPlayer(other, originTransform))
        {
            // Ho silencio perque el joc peta molt si no

            GameObject instancedGO = GameObject.Instantiate(decalPrefab, transform.position, decalPrefab.transform.rotation, other.transform);
            instancedGO.transform.localScale = instancedGO.transform.localScale * transform.localScale.x;
            //instancedGO.tag = "Untagged";
            other.GetComponentInParent<Rigidbody>().AddExplosionForce(1000, transform.position, col.radius * transform.localScale.x);

        }
        else
        {
            if (!Decals.TagToIgnore(other.tag))
            {
                //Vector3 closesPoint = other.ClosestPoint(transform.position);
                //Vector3 spawnPoint = closesPoint + ((transform.position - closesPoint).normalized * decalPrefab.transform.localScale.x);
                GameObject instancedGO = GameObject.Instantiate(decalPrefab, transform.position, decalPrefab.transform.rotation, other.transform);
                instancedGO.transform.localScale = instancedGO.transform.localScale * transform.localScale.x * sizeInc;

                Destroy(gameObject);

            }

        }

    }



    public void SetOriginTransform(Transform _transform)
    {
        originTransform = _transform;
    }


    //IEnumerator DespawnCoroutine()
    //{
    //    yield return new WaitForSeconds(10.0f);
    //    Destroy(gameObject);
    //}
}
