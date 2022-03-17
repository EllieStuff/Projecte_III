using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeMaskScript : MonoBehaviour
{
    [SerializeField] Transform playerTransform;

    float distance = 10.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Vector3 camPos = GameObject.FindGameObjectWithTag("MainCamera").transform.position;
        Vector3 camPos = Camera.main.transform.position;
        transform.position = playerTransform.position + (camPos - playerTransform.position).normalized * distance;
    }
}
