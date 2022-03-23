using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathfallAndCheckpointsSystem : MonoBehaviour
{
    public int checkpointNumber;
    public bool finishRaceCP;
    public bool activated;
    private bool enableMusic;
    [SerializeField] private bool multiplayerMode;
    private GameObject particlesFinish;
    private AudioSource fireworkEffectSound;
    private AudioSource finishAudio;
    [SerializeField] private GameObject particlesPrefab;
    [SerializeField] private Transform nextCheckPoint;
    [SerializeField] private Transform[] finishedQuads;
    PlayerVehicleScript[] vehicleScripts;
    GameObject[] chasises;

    private void Update()
    {
        if(enableMusic)
        {
            if(finishAudio.volume < 0.2f)
                finishAudio.volume += Time.deltaTime / 50;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(finishRaceCP)
        {
            particlesFinish = GameObject.Find("ParticlesFinish");
            finishAudio = particlesFinish.transform.GetChild(0).GetComponent<AudioSource>();
            fireworkEffectSound = particlesFinish.GetComponent<AudioSource>();
        }

        PlayersManager playersManager = GameObject.FindGameObjectWithTag("PlayersManager").GetComponent<PlayersManager>();
        multiplayerMode = (playersManager.gameMode == PlayersManager.GameModes.MULTI_LOCAL);
        if (multiplayerMode)
            finishedQuads = new Transform[4];
        vehicleScripts = new PlayerVehicleScript[playersManager.numOfPlayers];
        chasises = new GameObject[playersManager.numOfPlayers];
        for(int i = 0; i < playersManager.numOfPlayers; i++)
        {
            vehicleScripts[i] = playersManager.GetPlayer(i).GetComponent<PlayerVehicleScript>();
            chasises[i] = vehicleScripts[i].transform.Find("vehicleChasis").gameObject;
        }

        //Set default checkpoint (should be the first of the level)
        if (name.Equals("Checkpoint"))
        {
            for(int i = 0; i < vehicleScripts.Length; i++)
            {
                vehicleScripts[i].respawnPosition = transform.position;
                vehicleScripts[i].respawnRotation = transform.localEulerAngles;
                vehicleScripts[i].respawnVelocity = Vector3.zero;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (finishRaceCP)
        {
            GameObject UI = GameObject.Find("UI");
            bool endRace = true;

            try
            {
                if(multiplayerMode)
                {
                    other.transform.parent.GetComponent<PlayerVehicleScript>().finishedRace = true;

                    for (int i = 0; i < finishedQuads.Length; i++)
                    {
                        if (finishedQuads[i] == null)
                        {
                            finishedQuads[i] = other.transform;
                            break;
                        }
                        else if (finishedQuads[i] == other.transform)
                            break;
                    }


                    PlayersManager pm = GameObject.FindGameObjectWithTag("PlayersManager").GetComponent<PlayersManager>();

                    for (int i = 0; i < pm.numOfPlayers; i++)
                    {
                        if (finishedQuads[i] == null)
                            endRace = false;
                    }
                }

                if (endRace)
                {
                    UI.transform.GetChild(1).GetChild(0).GetComponent<UITimerChrono>().finishedRace = true;
                    UI.transform.GetChild(3).gameObject.SetActive(true);

                    particlesFinish.GetComponent<ParticleSystem>().Play();
                    finishAudio.Play();
                    fireworkEffectSound.Play();

                    particlesFinish.transform.GetChild(0).GetComponent<AudioSource>().Play();
                    AudioManager.Instance.Stop_OST();

                    enableMusic = true;
                    finishRaceCP = false;
                }
            }
            catch (Exception)
            {

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerVehicle"))
        {
            int currPlayerId = other.transform.parent.GetComponentInParent<QuadSceneManager>().playerId;
            
            if (gameObject.tag.Equals("Respawn"))
            {
                vehicleScripts[currPlayerId].respawnPosition = transform.position;
                vehicleScripts[currPlayerId].respawnRotation = transform.localEulerAngles;
                vehicleScripts[currPlayerId].respawnVelocity = Vector3.zero;

                if(multiplayerMode)
                {
                    Transform UIPositionIndex = GameObject.FindGameObjectWithTag("PositionsManager").transform;
                    UIPosition posUI = UIPositionIndex.transform.GetChild(currPlayerId).GetComponent<UIPosition>();
                    PlayerPositions playerPositions = UIPositionIndex.GetComponent<PlayerPositions>();
                    if (playerPositions != null && nextCheckPoint != null)
                        playerPositions.checkpoints[currPlayerId] = nextCheckPoint;
                    if (checkpointNumber > posUI.checkpointNumber)
                        posUI.checkpointNumber++;
                }
            }
            if (gameObject.tag.Equals("Death Zone"))
            {
                AudioManager.Instance.Play_SFX("Fall_SFX");

                // Fa falta el transform.parent??
                PlayerVehicleScript currPlayerScript = other.GetComponentInParent<PlayerVehicleScript>();
                currPlayerScript.transform.position = vehicleScripts[currPlayerId].respawnPosition;
                currPlayerScript.vehicleRB.velocity = vehicleScripts[currPlayerId].respawnVelocity;
                currPlayerScript.vehicleRB.angularVelocity = vehicleScripts[currPlayerId].respawnVelocity;
                currPlayerScript.vehicleRB.constraints = RigidbodyConstraints.FreezePositionX;
                currPlayerScript.vehicleRB.constraints = RigidbodyConstraints.FreezePositionZ;
                currPlayerScript.transform.localEulerAngles = vehicleScripts[currPlayerId].respawnRotation;
                currPlayerScript.transform.localEulerAngles += new Vector3(0, 90, 0);
            }

        }
    }
}
