using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelScript : MonoBehaviour
{
    public enum BarrelType { EXPLOSIVE, MOBIL, COUNT};

    [SerializeField] BarrelType type;

    [SerializeField] float speed = 0.0f;
    [SerializeField] Vector3 rotSpeed = new Vector3(0.0f, 5.0f, 0.0f);

    Rigidbody rb = null;

    BarrelCollision barrel;

    [SerializeField] bool hasExploded = false;
    BarrelExplosion explosion = null;

    private MapWarningObstacle warningSystem; 

    // Start is called before the first frame update
    void Start()
    {
        warningSystem = GameObject.Find("HUD").transform.Find("MapEventWarning").GetComponent<MapWarningObstacle>();
        
        if(type.Equals(BarrelType.MOBIL))
            warningSystem.InstantiateWarning(new Vector3(Mathf.Clamp(transform.parent.parent.localPosition.x * 50, -400, 400), 480, 0), 1.5f);
        else if(type.Equals(BarrelType.EXPLOSIVE))
            warningSystem.InstantiateWarning(new Vector3(Mathf.Clamp(transform.localPosition.x * 50, -400, 400), 480, 0), 1.5f);

        barrel = transform.GetChild(0).GetComponent<BarrelCollision>();

        rb = gameObject.AddComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        switch (type)
        {
            case BarrelType.EXPLOSIVE:
                explosion = barrel.GetComponentInChildren<BarrelExplosion>();
                explosion.gameObject.SetActive(false);

                break;
            case BarrelType.MOBIL:
                //rb.isKinematic = true;

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
            //Move Barrel forward
            rb.velocity = transform.forward * speed;
            Quaternion currRot = Quaternion.Euler(barrel.transform.localRotation.eulerAngles + rotSpeed);
            barrel.transform.localRotation = currRot;
        }
        else if(type == BarrelType.EXPLOSIVE)
        {
            if (explosion == null) return;

            if(hasExploded && !explosion.gameObject.activeSelf)
            {
                explosion.gameObject.SetActive(true);
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
