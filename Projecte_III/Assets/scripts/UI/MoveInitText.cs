using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInitText : MonoBehaviour
{
    [SerializeField] float baseSpeed = 10.0f;
    [SerializeField] float slowSpeed = 10.0f;
    [SerializeField] float lerpSpeed = 0.3f;

    Rigidbody rb;
    float usedSpeed = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(StartDelay());
        //usedSpeed = slowSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(transform.position + transform.up * usedSpeed * Time.deltaTime);
        //Debug.Log("InitTextSpeed: " + usedSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SlowSpeedCollider"))
        {
            //StartCoroutine(LerpSpeed(slowSpeed));
            usedSpeed = slowSpeed;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SlowSpeedCollider"))
        {
            StartCoroutine(LerpSpeed(baseSpeed, lerpSpeed));
            //usedSpeed = baseSpeed;
        }
    }

    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(1.0f);
        yield return new WaitForEndOfFrame();
        usedSpeed = baseSpeed;
    }
    IEnumerator LerpSpeed(float _speed, float _lerpSpeed = 0.1f)
    {
        Debug.Log("1");
        float initSpeed = usedSpeed;
        float timer = 0.0f, maxTime = _lerpSpeed;
        while (timer < maxTime)
        {
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
            usedSpeed = Mathf.Lerp(initSpeed, _speed, timer / maxTime);
            Debug.Log("2");
        }
        usedSpeed = _speed;
    }
}
