using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixPuddlePosition : MonoBehaviour
{
    [SerializeField] ColliderManager upCol, downCol;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("water spawned");
        upCol.tagsToListen.Add("Road");
        downCol.tagsToListen.Add("Road");
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Water: " + upCol.triggered.ToString() + " / " + downCol.triggered.ToString());
        if(upCol.triggered) transform.position = new Vector3(transform.position.x, transform.position.y + 0.01f, transform.position.z);
        if(downCol.triggered) transform.position = new Vector3(transform.position.x, transform.position.y - 0.01f, transform.position.z);
    }

}
