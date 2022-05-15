using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ParticleController : MonoBehaviour
{
    public GameObject Player;
    QuadControls controls;
    public float numOfDustParticleSystems;
    public float numOfWaterParticleSystems;
    private ParticleSystem DustParticleSys;
    private ParticleSystem SmokeParticleSys;
    private ParticleSystem WaterParticleSys;

    // Start is called before the first frame update
    void Start()
    {
        controls = new QuadControls();
        controls.Enable();
        DustParticleSys = this.transform.Find("WheelParticles").GetComponentInChildren<ParticleSystem>();
        SmokeParticleSys = this.transform.Find("SmokeParticles").GetComponentInChildren<ParticleSystem>();
        WaterParticleSys = this.transform.Find("WaterParticles").GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        //Ground Particles
        for (int i = 0; i < numOfDustParticleSystems; i++)
        {
            DustParticleSys = transform.Find("WheelParticles").GetChild(i).GetComponent<ParticleSystem>();

            if (Player.GetComponent<PlayerVehicleScript>().touchingGround && !DustParticleSys.isPlaying)
                DustParticleSys.Play();

            else if (DustParticleSys.isEmitting &&(!Player.GetComponent<PlayerVehicleScript>().touchingGround))
                DustParticleSys.Stop();
        }       

        //Smoke Particles
        if(controls.Quad.Forward.ReadValue<float>() > 0 && !SmokeParticleSys.isEmitting)
            SmokeParticleSys.Play();
        else if (controls.Quad.Forward.ReadValue<float>() == 0 && SmokeParticleSys.isEmitting)
            SmokeParticleSys.Stop();

        //Water Particles
        /*for (int i = 0; i < numOfWaterParticleSystems; i++)
        {
            WaterParticleSys = this.transform.Find("WaterParticles").GetChild(i).GetComponent<ParticleSystem>();

            if (Player.GetComponent<PlayerVehicleScript>().onWater && Player.GetComponent<PlayerFloater>().HasFloater && !WaterParticleSys.isPlaying)
                WaterParticleSys.Play();

            else if (!Player.GetComponent<PlayerVehicleScript>().onWater && WaterParticleSys.isEmitting)
                WaterParticleSys.Stop();
        }*/
    }
}
