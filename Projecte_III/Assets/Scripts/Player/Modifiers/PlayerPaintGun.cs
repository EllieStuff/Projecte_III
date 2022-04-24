using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPaintGun : MonoBehaviour
{
    //PlayerVehicleScript player;
    [SerializeField] PaintGunScript paintGun;
    public bool hasPaintGun = false;


    // Start is called before the first frame update
    void Start()
    {
        paintGun.originTransform = transform.Find("vehicleChasis");
    }
    internal void Init(Transform _modifier, bool _active)
    {
        Debug.Log("Inited paintGun");
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


    public void Activate(Quaternion _gunRot)
    {
        Debug.Log("in paint gun, " + hasPaintGun.ToString());
        if (hasPaintGun)
        {
            paintGun.transform.localRotation = _gunRot;
            paintGun.Activate();
        }
    }

    [ContextMenu("SetPaintGunModifier")]
    public void SetPaintGunModifier()
    {
        RandomModifierGet.ModifierTypes modType = RandomModifierGet.ModifierTypes.PAINT_GUN;
        RandomModifierGet modGetter = GetComponent<RandomModifierGet>();
        modGetter.ResetModifiers();
        modGetter.SetModifier(modType);

        try
        {
            int playerId = GetComponentInParent<PlayerData>().id;
            GameObject.Find("HUD").GetComponentInChildren<PlayersHUDManager>().GetPlayerHUD(playerId).SetModifierImage((int)modType);
        }
        catch { Debug.LogError("PlayersHUD not found"); }
    }

}
