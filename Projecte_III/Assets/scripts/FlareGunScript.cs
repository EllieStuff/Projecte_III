using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlareGunScript : MonoBehaviour
{
    public GameObject parentVehicle;
    public GameObject bulletPrefab;
    public float shootTimerResetNum;
    private float shootTimer;
    public int flareAmmo;
    public int bulletSpeed;

    private void Start()
    {
        parentVehicle = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (shootTimer > 0)
            shootTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && flareAmmo > 0 && shootTimer <= 0)
        {
            GameObject flareInstance = Instantiate(bulletPrefab, this.transform.position, this.transform.rotation);
            Physics.IgnoreCollision(parentVehicle.transform.GetChild(0).GetComponent<BoxCollider>(), flareInstance.GetComponent<SphereCollider>());

            GameObject blocks = parentVehicle.transform.GetChild(2).gameObject;
            for (int i = 0; i < blocks.transform.childCount; i++)
            {
                Physics.IgnoreCollision(blocks.transform.GetChild(i).GetComponent<BoxCollider>(), flareInstance.GetComponent<SphereCollider>());
            }
            var locVel = transform.InverseTransformDirection(flareInstance.GetComponent<Rigidbody>().velocity);
            flareInstance.GetComponent<Rigidbody>().velocity = transform.TransformDirection(new Vector3(0, 1, bulletSpeed));
            flareAmmo--;
            shootTimer = shootTimerResetNum;
        }
    }
}
