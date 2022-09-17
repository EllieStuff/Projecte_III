using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceScript : MonoBehaviour
{
    const float BOUNCE_MARGIN = 0.04f;
    
    [SerializeField] Transform origin;
    [SerializeField] float bounceMargin = -0.05f;
    [SerializeField] float bounceSpeed = -1;
    [SerializeField] float bounceFreq = 0.06f;

    internal Vector3 affectedAxis = new Vector3(0, 1, 0);

    bool isActive = false;
    bool bounceFlag = true;
    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        //Activate();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive) return;

        timer += Time.deltaTime;
        if(timer > bounceFreq)
        {
            timer = 0;
            bounceSpeed *= -1;
        }

        transform.position = new Vector3(
            transform.position.x + bounceSpeed * Time.deltaTime * affectedAxis.x, 
            transform.position.y + bounceSpeed * Time.deltaTime * affectedAxis.y, 
            transform.position.z + bounceSpeed * Time.deltaTime * affectedAxis.z
        );

    }

    public void Activate(Vector3 _affectedAxis, float _affectedTime = -1)
    {
        isActive = true;
        affectedAxis = _affectedAxis;
        StartCoroutine(CorrectCoroutine());
        if(_affectedTime >= 0)
            StartCoroutine(DeactivateCoroutine(_affectedTime));
    }
    public void Deactivate()
    {
        isActive = false;
        transform.position = new Vector3(origin.position.x, origin.position.y + bounceMargin, origin.position.z);
    }

    IEnumerator CorrectCoroutine()
    {
        //transform.position = new Vector3(origin.position.x, origin.position.y + bounceMargin, origin.position.z);
        yield return new WaitForSeconds(0.1f);
        while (isActive)
        {
            transform.position = new Vector3(origin.position.x, origin.position.y + bounceMargin, origin.position.z);
            yield return new WaitForSeconds(3);
        }
    }
    IEnumerator DeactivateCoroutine(float _time)
    {
        yield return new WaitForSeconds(_time);
        Deactivate();
    }

}
