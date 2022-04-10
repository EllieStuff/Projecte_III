using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelExplosion : MonoBehaviour
{
    PlayerVehicleScript player;

    public PlayerVehicleScript Player
    {
        get => player;
        set => player = value;
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
