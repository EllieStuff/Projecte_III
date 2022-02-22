using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeScript : MonoBehaviour
{
    public bool explode;
    public float speedExplode;
    public Material explodeMat;

    private void Start()
    {
        GetComponent<MeshRenderer>().material = new Material(explodeMat);
    }

    // Update is called once per frame
    void Update()
    {
        if (explode && GetComponent<MeshRenderer>().material.GetFloat("_StartDistance") < 0.5f)
        {
            GetComponent<MeshRenderer>().material.SetFloat("_StartDistance", GetComponent<MeshRenderer>().material.GetFloat("_StartDistance") + Time.deltaTime * speedExplode / 2);
        }
        else if (explode)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag.Contains("Player"))
        {
            speedExplode = other.gameObject.GetComponent<Rigidbody>().velocity.magnitude;
            if(speedExplode >= 0.2f)
            {
                GetComponent<BoxCollider>().isTrigger = true;
                explode = true;
            }
        }
    }
}
