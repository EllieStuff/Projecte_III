using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    public GameObject ExplosionParticles;
    ParticleSystem EPS;
    ParticleSystem.MainModule settings;

    [SerializeField] RandomModifierGet player;
    public int id;
    Color _color;

    // Start is called before the first frame update
    void Start()
    {
        EPS = ExplosionParticles.GetComponent<ParticleSystem>();
        settings = ExplosionParticles.GetComponent<ParticleSystem>().main;

        //ExplosionParticles.GetComponent<MeshRenderer>().material.renderQueue = 3003;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerVehicle"))
        {

            id = other.GetComponentInParent<PlayerVehicleScript>().playerNum;
            player = GameObject.Find("PlayersManager").GetComponent<PlayersManager>().GetPlayer(id).GetComponentInChildren<RandomModifierGet>();
            _color = ColorsAndAISelector.GetColor(player.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material.name);

            ExplosionParticles.transform.position = other.transform.position;
            settings.startColor = new ParticleSystem.MinMaxGradient(_color);
            EPS.Play();
        }
    }
}
