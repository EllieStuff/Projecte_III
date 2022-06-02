using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGhostTrigger : MonoBehaviour
{
    VehicleTriggerAndCollisionEvents playerEvents;
    Rigidbody playerRB;
    PlayerVehicleScript playerScript;

    Vector3 currDir = Vector3.zero;

    int playersColliding = 0;

    private void Start()
    {
        playerEvents = transform.parent.parent.GetComponent<VehicleTriggerAndCollisionEvents>();
        playerRB = playerEvents.GetComponent<Rigidbody>();
        playerScript = playerEvents.GetComponent<PlayerVehicleScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (playerEvents.inmunity && other.gameObject.tag.Contains("Player"))
        {
            playersColliding++;
            playerEvents.StopGhost = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (playerEvents.inmunity && other.gameObject.tag.Contains("Player"))
        {
            if (playerEvents.TimerRespawn < 0.5f)
            {
                playerScript.dash = true;
                float _speed = 10;
                currDir += -(transform.position - other.transform.position) * Time.deltaTime;
                currDir = new Vector3(currDir.x, 0.0f, 0.0f).normalized;
                Vector3 dirLocal = transform.InverseTransformDirection(currDir);
                playerRB.velocity = transform.TransformDirection(dirLocal.x * _speed, dirLocal.y * _speed, transform.InverseTransformDirection(playerRB.velocity).z);
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (playerEvents.inmunity && other.gameObject.tag.Contains("Player"))
        {
            playersColliding--;
            if(playersColliding == 0 || playersColliding < 0)
            {
                playersColliding = 0;
                playerScript.dash = false;
                playerEvents.StopGhost = true;
            }
        }
    }
}
