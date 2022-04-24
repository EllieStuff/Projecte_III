using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOilGun : MonoBehaviour
{
    [SerializeField] OilGunScript oilGun;
    public bool hasOilGun = false;

    // Start is called before the first frame update
    void Start()
    {
        oilGun.originTransform = transform.Find("vehicleChasis");
    }
    //internal void Init(Transform _modifier, bool _active)
    //{
    //    hasOilGun = _active;
    //    if (_active)
    //        oilGun = _modifier.GetComponent<OilGunScript>();
    //    else
    //        oilGun = null;

    //}

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


    [ContextMenu("SetOilGunModifier")]
    public void SetOilGunModifier()
    {
        RandomModifierGet.ModifierTypes modType = RandomModifierGet.ModifierTypes.OIL;
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
