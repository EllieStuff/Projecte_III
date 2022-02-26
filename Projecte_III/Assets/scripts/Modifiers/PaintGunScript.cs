using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintGunScript : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    //[SerializeField] Vector3 dir;
    [SerializeField] float timeActive = 5;
    [SerializeField] float rechargeTime = 5;
    [SerializeField] Utils.MinMaxFloat force = new Utils.MinMaxFloat(4, 6);
    [SerializeField] Utils.MinMaxFloat timeDiff = new Utils.MinMaxFloat(0.01f, 0.2f);
    [SerializeField] Utils.MinMaxFloat size = new Utils.MinMaxFloat(0.01f, 0.1f);
    [SerializeField] Utils.MinMaxVec3 dirDiff = new Utils.MinMaxVec3(-Vector3.one, Vector3.one);

    GameObject model;
    bool gunUsable = true;
    float gunSizeIncSpeed = 30.0f;
    Vector3 originalModelSize;

    // Start is called before the first frame update
    void Start()
    {
        model = transform.GetChild(0).gameObject;
        originalModelSize = model.transform.localScale;
        model.transform.localScale = Vector3.zero;
        model.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && gunUsable)
            Activate();
    }


    public void Activate()
    {
        Debug.Log("In");
        gunUsable = false;
        StartCoroutine(ShootGun());
    }


    IEnumerator ShootGun()
    {
        model.SetActive(true);
        model.transform.localScale = Vector3.zero;
        while (model.transform.localScale.x < originalModelSize.x - 0.1f)
        {
            yield return new WaitForEndOfFrame();
            model.transform.localScale = Vector3.Lerp(model.transform.localScale, originalModelSize, Time.deltaTime * gunSizeIncSpeed);
        }
        model.transform.localScale = originalModelSize;

        float currTime = 0.0f;
        while(currTime < timeActive)
        {
            GameObject currBullet = GameObject.Instantiate(prefab, transform.position, prefab.transform.rotation);
            float newScale = size.GetRndValue();
            currBullet.transform.localScale = new Vector3(newScale, newScale, newScale);
            currBullet.GetComponent<Rigidbody>().AddForce((transform.forward + dirDiff.GetRndValue().normalized/10.0f) * force.GetRndValue(), ForceMode.Impulse);

            float timeInc = timeDiff.GetRndValue();
            currTime += timeInc;
            yield return new WaitForSeconds(timeInc);
        }


        while (model.transform.localScale.x > 0.1f)
        {
            yield return new WaitForEndOfFrame();
            model.transform.localScale = Vector3.Lerp(model.transform.localScale, Vector3.zero, Time.deltaTime * gunSizeIncSpeed);
        }


        model.SetActive(false);
        StartCoroutine(RechargeTime());
    }

    // Nota: esta fet aixi perque es pugui mostrar el temps que falta amb un slider facilment
    IEnumerator RechargeTime()
    {
        float currTime = 0.0f;
        while (currTime < rechargeTime)
        {
            yield return new WaitForEndOfFrame();
            currTime += Time.deltaTime;
        }

        gunUsable = true;
    }


}
