using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlareGunBulletScript : MonoBehaviour
{
    public float timerDestroyBullet;

    // Update is called once per frame
    void Update()
    {
        timerDestroyBullet -= Time.deltaTime;

        if (timerDestroyBullet <= 0)
            Destroy(this.gameObject);
    }
}
