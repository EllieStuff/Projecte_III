using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecalPaintScript : MonoBehaviour
{
    [SerializeField] Utils.MinMaxFloat despawnTime;
    [SerializeField] float despawnSpeed = 2.0f;


    private void OnEnable()
    {
        StartCoroutine(DespawnCoroutine());
    }


    IEnumerator DespawnCoroutine()
    {
        yield return new WaitForSeconds(despawnTime.GetRndValue());
        while(transform.localScale.x > 0.1f)
        {
            yield return new WaitForEndOfFrame();
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, Time.timeScale * despawnSpeed);
        }

        Destroy(gameObject);
    }


}
