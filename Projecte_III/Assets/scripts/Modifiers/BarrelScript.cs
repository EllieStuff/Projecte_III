using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelScript : MonoBehaviour
{
    enum BarrelType { EXPLOSIVE, MOBIL, COUNT};

    [SerializeField] BarrelType type;

    [SerializeField] Transform player;

    [SerializeField] float speed = 0.0f;

    Rigidbody rb = null;

    Transform explosion = null;

    // Start is called before the first frame update
    void Start()
    {
        switch (type)
        {
            case BarrelType.EXPLOSIVE:
                explosion = transform.GetChild(0);
                //transform.GetComponentInChildren<BarrelExplosion>().Player = player;

                break;
            case BarrelType.MOBIL:
                rb = gameObject.AddComponent<Rigidbody>();
                
                break;
            case BarrelType.COUNT:

                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(type == BarrelType.MOBIL)
        {
            //Move Barrel forward
            rb.velocity = transform.forward * speed;
        }

        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag(player.tag))
        {
            Debug.Log("Collision with player");
        }
    }
}
