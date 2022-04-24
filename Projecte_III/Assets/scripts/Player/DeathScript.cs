using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DeathScript
{
    public static bool CheckIfDeathByFlipping(bool VehicleIsFlipped, Collision other)
    {
        if (other.gameObject.tag.Equals("ground"))
            return true;
        return false;
    }

    public static bool DeathByFlipping(float timeInFlippedState, Transform playerTransform, Rigidbody playerRB, Vector3 respawnPosition, Vector3 respawnRotation, Vector3 respawnVelocity, out Transform outPlayerTransform, out Rigidbody outPlayerRB)
    {
        outPlayerRB = playerRB;
        outPlayerTransform = playerTransform;
        if (timeInFlippedState >= 1)
        {
            //AudioManager.Instance.Play_SFX("Fall_SFX");
            outPlayerTransform.position = respawnPosition;
            outPlayerTransform.localEulerAngles = respawnRotation;
            outPlayerTransform.localEulerAngles += new Vector3(0, 90, 0);
            outPlayerRB.velocity = new Vector3(respawnVelocity.x, respawnVelocity.y, respawnVelocity.z);
            timeInFlippedState = 0;
            return true;
        }
        return false;
    }

    public static bool DeathByFalling(bool gliderActive, Transform playerTransform, Rigidbody playerRB, Vector3 respawnPosition, Vector3 respawnRotation, Vector3 respawnVelocity, out Transform outPlayerTransform, out Rigidbody outPlayerRB)
    {
        outPlayerRB = playerRB;
        outPlayerTransform = playerTransform;
        if (!gliderActive && playerRB.velocity.y <= -30)
        {
            //AudioManager.Instance.Play_SFX("Fall_SFX");
            outPlayerTransform.position = respawnPosition;
            outPlayerTransform.localEulerAngles = respawnRotation;
            outPlayerTransform.localEulerAngles += new Vector3(0, 180, 0);
            outPlayerTransform.localEulerAngles = new Vector3(0, outPlayerTransform.localEulerAngles.y, outPlayerTransform.localEulerAngles.z);
            outPlayerRB.velocity = new Vector3(respawnVelocity.x, respawnVelocity.y, respawnVelocity.z);
            return true;
        }
        return false;
    }

    public static bool DeathByExitCamera(bool touchedTrigger, Transform playerTransform, Rigidbody playerRB, Vector3 respawnPosition, Vector3 respawnRotation, Vector3 respawnVelocity, out Transform outPlayerTransform, out Rigidbody outPlayerRB)
    {
        outPlayerRB = playerRB;
        outPlayerTransform = playerTransform;
        if (touchedTrigger)
        {
            //AudioManager.Instance.Play_SFX("Fall_SFX");
            outPlayerTransform.position = respawnPosition;
            outPlayerTransform.localEulerAngles = respawnRotation;
            outPlayerTransform.localEulerAngles += new Vector3(0, 180, 0);
            outPlayerTransform.localEulerAngles = new Vector3(0, outPlayerTransform.localEulerAngles.y, outPlayerTransform.localEulerAngles.z);
            outPlayerRB.velocity = new Vector3(respawnVelocity.x, respawnVelocity.y, respawnVelocity.z);
            return true;
        }
        return false;
    }
}


