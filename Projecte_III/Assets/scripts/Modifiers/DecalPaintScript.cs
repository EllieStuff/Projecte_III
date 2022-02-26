using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecalPaintScript : MonoBehaviour
{
    [SerializeField] Utils.MinMaxFloat despawnTime;
    [SerializeField] float despawnSpeed = 0.003f;
    [SerializeField] Utils.MinMaxFloat despawnSpeedDiff = new Utils.MinMaxFloat(-0.002f, 0.002f);


    private void OnEnable()
    {
        StartCoroutine(DespawnCoroutine());
        GetComponent<MeshRenderer>().material.renderQueue = 3002;
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


}
