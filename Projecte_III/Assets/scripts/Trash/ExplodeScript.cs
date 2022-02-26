using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeScript : MonoBehaviour
{
    public bool explode;
    public float speedExplode;
    public Material explodeMat;
    private MeshRenderer mesh;

    private void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        GetComponent<MeshRenderer>().material = new Material(explodeMat);
    }

    // Update is called once per frame
    void Update()
    {
        if (explode && mesh.material.GetFloat("_StartDistance") < 0.5f)
        {
            mesh.material.SetFloat("_StartDistance", mesh.material.GetFloat("_StartDistance") + Time.deltaTime * speedExplode / 10);
        }
        else if (explode)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        GameObject otherGO = other.gameObject;
        if (otherGO.tag.Contains("Player"))
        {
            speedExplode = otherGO.GetComponent<Rigidbody>().velocity.magnitude;
            if(speedExplode >= 0.2f)
            {
                GetComponent<BoxCollider>().isTrigger = true;
                explode = true;
            }
        }
    }
}
