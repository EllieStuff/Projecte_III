using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelColision : MonoBehaviour
{
    Transform player;
    CapsuleCollider col;

    BarrelScript barrel;

    public Transform Player
    {
        get => player;
        set => player = value;
    }

    private void Start()
    {
        barrel = transform.parent.GetComponent<BarrelScript>();
        col = GetComponent<CapsuleCollider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Contains("Player"))
        {
            Debug.Log("Collision with player");

            if(barrel.GetType() == BarrelScript.BarrelType.EXPLOSIVE)
                barrel.Explode();
        }
    }
}
