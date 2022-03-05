using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPaintGun : MonoBehaviour
{
    //PlayerVehicleScript player;
    PaintGunScript paintGun;
    bool hasPaintGun = false;

    public bool HasPaintGun { get { return hasPaintGun; } }


    // Start is called before the first frame update
    void Start()
    {
        //player = GetComponent<PlayerVehicleScript>();
    }
    internal void Init(Transform _modifier, bool _active)
    {
        hasPaintGun = _active;
        if (_active)
            paintGun = _modifier.GetComponent<PaintGunScript>();
        else
            paintGun = null;

    }

    // Update is called once per frame
    void Update()
    {
        // Nope
    }


    public void Activate()
    {
        if(hasPaintGun)
            paintGun.Activate();
    }

}
