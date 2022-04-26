using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilGunScript : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] int dropsAmount = 30;
    [SerializeField] float disableTime = 5.0f;
    [SerializeField] Utils.MinMaxFloat force = new Utils.MinMaxFloat(4, 6);
    [SerializeField] Utils.MinMaxFloat size = new Utils.MinMaxFloat(0.01f, 0.1f);
    [SerializeField] Utils.MinMaxVec3 dirDiff = new Utils.MinMaxVec3(-Vector3.one, Vector3.one);

    internal Transform originTransform;

    //bool gunUsable = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Backspace) && gunUsable)
        //    Activate();
    }


    public void Activate()
    {
        for (int i = 0; i < dropsAmount; i++)
        {
            GameObject currBullet = GameObject.Instantiate(prefab, transform.position, prefab.transform.rotation);
            currBullet.GetComponent<OilBulletScript>().SetOriginTransform(originTransform);
            float newScale = size.GetRndValue();
            currBullet.transform.localScale = new Vector3(newScale, newScale, newScale);
            currBullet.GetComponent<Rigidbody>().AddForce((transform.forward + dirDiff.GetRndValue().normalized) * force.GetRndValue(), ForceMode.Impulse);

        }

    }


    //IEnumerator RechargeGun()
    //{
    //    float currTime = 0.0f;
    //    while (currTime < disableTime)
    //    {
    //        yield return new WaitForEndOfFrame();
    //        currTime += Time.deltaTime;
    //    }


    //    gunUsable = true;
    //    Debug.Log("Out");
    //}
}
