using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    public GameObject ExplosionParticles;
    ParticleSystem EPS;
    ParticleSystem.MainModule settings;

    // Start is called before the first frame update
    void Start()
    {
        EPS = ExplosionParticles.GetComponent<ParticleSystem>();
        settings = ExplosionParticles.GetComponent<ParticleSystem>().main;

        ExplosionParticles.GetComponent<MeshRenderer>().material.renderQueue = 3003;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerVehicle") /*&& other.name.Equals("Backward")*/)
        {
            ExplosionParticles.transform.position = other.transform.position;
            settings.startColor = new ParticleSystem.MinMaxGradient();
            EPS.Play();
        }
    }
}
