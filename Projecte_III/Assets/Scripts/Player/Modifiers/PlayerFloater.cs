using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFloater : MonoBehaviour
{
    PlayerVehicleScript player;
    bool hasFloater = false;

    public bool HasFloater { get { return hasFloater; } }


    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerVehicleScript>();
    }
    internal void Init(bool _active)
    {
        hasFloater = _active;
        Physics.IgnoreLayerCollision(3, 4, !hasFloater);
    }

    // Update is called once per frame
    void Update()
    {
        // Nope
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Water") && !hasFloater)
        {
            StartCoroutine(player.LerpVehicleMaxSpeed(player.savedMaxSpeed * 2 / 3, 3.0f));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Water") && !hasFloater)
        {
            StartCoroutine(player.LerpVehicleMaxSpeed(player.savedMaxSpeed, 1.5f));
        }
    }


}
