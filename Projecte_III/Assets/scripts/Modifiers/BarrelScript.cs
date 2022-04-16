using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelScript : MonoBehaviour
{
    public enum BarrelType { EXPLOSIVE, MOBIL, COUNT};

    [SerializeField] BarrelType type;

    [SerializeField] Transform player;

    [SerializeField] float speed = 0.0f;

    Rigidbody rb = null;

    BarrelColision barrel;

    [SerializeField] bool hasExploded = false;
    Transform explosion = null;

    // Start is called before the first frame update
    void Start()
    {
        barrel = transform.GetChild(0).GetComponent<BarrelColision>();
        barrel.Player = player;

        rb = barrel.gameObject.AddComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        switch (type)
        {
            case BarrelType.EXPLOSIVE:
                explosion = barrel.transform.GetChild(0);
                explosion.GetComponent<SphereCollider>().enabled = false;

                transform.GetComponentInChildren<BarrelExplosion>().Player = player;

                break;
            case BarrelType.MOBIL:
                
                break;
            case BarrelType.COUNT:

                break;
            default:
                break;
        }
    }

    private void Update()
    {
        if (type == BarrelType.MOBIL)
        {
            Debug.Log("Forward: " + transform.forward);
            //Move Barrel forward
            rb.velocity = transform.forward * speed;
        }
        else if(type == BarrelType.EXPLOSIVE)
        {
            if(hasExploded)
            {
                explosion.GetComponent<SphereCollider>().enabled = true;
            }
        }
    }

    public BarrelType GetType()
    {
        return type;
    }

    public void Explode()
    {
        hasExploded = true;
    }
}
