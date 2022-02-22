using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintGunScript : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    //[SerializeField] Vector3 dir;
    [SerializeField] float timeActive = 5;
    [SerializeField] Utils.MinMaxFloat force = new Utils.MinMaxFloat(4, 6);
    [SerializeField] Utils.MinMaxFloat timeDiff = new Utils.MinMaxFloat(0.01f, 0.2f);
    [SerializeField] Utils.MinMaxFloat size = new Utils.MinMaxFloat(0.01f, 0.1f);
    [SerializeField] Utils.MinMaxVec3 dirDiff = new Utils.MinMaxVec3(-Vector3.one, Vector3.one);

    bool gunActive = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !gunActive)
            Activate();
    }


    public void Activate()
    {
        Debug.Log("In");
        gunActive = true;
        StartCoroutine(ShootGun());
    }


    IEnumerator ShootGun()
    {
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


        gunActive = false;
        Debug.Log("Out");
    }


}
