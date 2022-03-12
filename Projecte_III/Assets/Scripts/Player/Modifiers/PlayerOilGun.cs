using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOilGun : MonoBehaviour
{
    OilGunScript oilGun;
    bool hasOilGun = false;

    public bool HasPaintGun { get { return hasOilGun; } }


    // Start is called before the first frame update
    void Start()
    {

    }
    internal void Init(Transform _modifier, bool _active)
    {
        hasOilGun = _active;
        if (_active)
            oilGun = _modifier.GetComponent<OilGunScript>();
        else
            oilGun = null;

    }

    // Update is called once per frame
    void Update()
    {
        // Nope
    }


    public void Activate()
    {
        if (hasOilGun)
            oilGun.Activate();
    }

}
