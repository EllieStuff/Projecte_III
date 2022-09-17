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

    bool isActive = true;
    bool bounceFlag = true;

    // Start is called before the first frame update
    void Start()
    {
        Activate();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive) return;

        transform.position = new Vector3(transform.position.x, transform.position.y + bounceSpeed * Time.deltaTime, transform.position.z);

    }

    public void Activate()
    {
        isActive = true;
        StartCoroutine(CorrectCoroutine());
        StartCoroutine(VibrateCoroutine());
    }
    public void Deactivate()
    {
        isActive = false;
        transform.position = new Vector3(origin.position.x, origin.position.y + bounceMargin, origin.position.z);
    }


    IEnumerator VibrateCoroutine()
    {
        while (isActive)
        {
            yield return new WaitForSecondsRealtime(bounceFreq);
            bounceSpeed *= -1;
        }
    }

    IEnumerator CorrectCoroutine()
    {
        transform.position = new Vector3(origin.position.x, origin.position.y + bounceMargin, origin.position.z);
        while (isActive)
        {
            yield return new WaitForSeconds(5);
            transform.position = new Vector3(origin.position.x, origin.position.y + bounceMargin + 0.2f, origin.position.z);
        }
    }

}
