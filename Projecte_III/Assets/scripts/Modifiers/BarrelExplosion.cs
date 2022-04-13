using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelExplosion : MonoBehaviour
{
    Transform player;

    public Transform Player
    {
        get => player;
        set => player = value;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("Player"))
        {
            Debug.Log("Player inside exlosion");
        }
    }
}
