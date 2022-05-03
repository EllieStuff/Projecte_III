using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostModifierScript : MonoBehaviour
{
    [SerializeField] VehicleTriggerAndCollisionEvents player;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<VehicleTriggerAndCollisionEvents>();
    }

    public void Active()
    {

    }
}
