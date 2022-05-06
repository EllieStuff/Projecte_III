using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomModifierGet : MonoBehaviour
{
    const float INIT_TIMER_MODIFIER = 5.0f;

    public enum ModifierTypes { PLUNGER, UMBRELLA, OIL, PAINT_GUN, SALTO_BOMBA, BOOST, COUNT, NONE };

    public GameObject[] modifiers;
    GameObject showModifierInstance;
    float timerModifier = 0;

    PlayerInputs inputs;

    public ModifierTypes currentModifier = ModifierTypes.NONE;

    PlayersHUD playerHud = null;

    private void Start()
    {
        inputs = GetComponent<PlayerInputs>();
        GetComponent<PlayerThrowPlunger>().SetPlungerModifier();
        ResetModifiers();
    }

    private void FixedUpdate()
    {
        if(showModifierInstance != null)
        {
            showModifierInstance.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        }

        if (/*currentModifier != ModifierTypes.NONE && */(inputs.ShootForward || inputs.ShootBackwards || inputs.ShootLeft || inputs.ShootRight) && timerModifier <= 0) 
        {
            if(playerHud == null)
            {
                playerHud = GameObject.Find("HUD").transform.GetComponentInChildren<PlayersHUDManager>().GetPlayerHUD(transform.parent.GetComponent<PlayerData>().id);
            }

            playerHud.ClearModifiers();

            switch (currentModifier)
            {
                case ModifierTypes.PLUNGER:
                    PlayerThrowPlunger plunger = GetComponent<PlayerThrowPlunger>();
                    if (inputs.ShootForward)
                        plunger.Activate(transform.TransformDirection(0, 0, 1));
                    else if (inputs.ShootBackwards)
                        plunger.Activate(transform.TransformDirection(0, 0, -1));
                    else if (inputs.ShootLeft)
                        plunger.Activate(transform.TransformDirection(-1, 0, 0));
                    else if (inputs.ShootRight)
                        plunger.Activate(transform.TransformDirection(1, 0, 0));

                    //plunger.hasPlunger = false;
                    timerModifier = INIT_TIMER_MODIFIER;
                    break;

                case ModifierTypes.UMBRELLA:
                    Umbrella umbrella = GetComponent<Umbrella>();
                    if (inputs.ShootForward)
                        umbrella.ActivateUmbrella(Quaternion.Euler(90, 180, 0), true);
                    else if (inputs.ShootBackwards)
                        umbrella.ActivateUmbrella(Quaternion.Euler(-90, 180, 0), false);
                    else if (inputs.ShootLeft)
                        umbrella.ActivateUmbrella(Quaternion.Euler(90, 180, 90), true);
                    else if (inputs.ShootRight)
                        umbrella.ActivateUmbrella(Quaternion.Euler(90, 180, -90), true);

                    timerModifier = INIT_TIMER_MODIFIER;
                    break;

                case ModifierTypes.OIL:
                    PlayerOilGun oilGun = GetComponent<PlayerOilGun>();
                    oilGun.Activate();

                    timerModifier = INIT_TIMER_MODIFIER;
                    break;

                case ModifierTypes.PAINT_GUN:
                    PlayerPaintGun paintGun = GetComponent<PlayerPaintGun>();
                    if (inputs.ShootForward)
                        paintGun.Activate(Quaternion.Euler(0, 180, 0));
                    else if (inputs.ShootBackwards)
                        paintGun.Activate(Quaternion.Euler(0, 0, 0));
                    else if (inputs.ShootLeft)
                        paintGun.Activate(Quaternion.Euler(0, 90, 0));
                    else if (inputs.ShootRight)
                        paintGun.Activate(Quaternion.Euler(0, -90, 0));

                    timerModifier = INIT_TIMER_MODIFIER;
                    break;

                case ModifierTypes.SALTO_BOMBA:
                    SaltoBomba salto = GetComponent<SaltoBomba>();
                    salto.hasJumped = true;
                    salto.Activate();
                    
                    timerModifier = INIT_TIMER_MODIFIER;
                    break;

                case ModifierTypes.BOOST:
                    BoostModifierScript boost = GetComponent<BoostModifierScript>();

                    if (inputs.ShootForward)
                        boost.Active(transform.forward, 1.0f, 1.0f);
                    else if (inputs.ShootBackwards)
                        boost.Active(-transform.forward, 0.25f, 0.3f);
                    else if (inputs.ShootLeft)
                        boost.Active(-transform.right, 0.5f, 1.0f);
                    else if (inputs.ShootRight)
                        boost.Active(transform.right, 0.5f, 1.0f);

                    timerModifier = INIT_TIMER_MODIFIER;
                    break;
                default:
                    break;
            }
            currentModifier = ModifierTypes.NONE;
        }
        if (timerModifier > 0)
            timerModifier -= Time.deltaTime;
    }

    public void SetModifier(ModifierTypes _type)
    {
        currentModifier = _type;
        switch (currentModifier)
        {
            case ModifierTypes.PLUNGER:
                GetComponent<PlayerThrowPlunger>().hasPlunger = true;
                break;
            case ModifierTypes.UMBRELLA:
                break;
            case ModifierTypes.OIL:
                GetComponent<PlayerOilGun>().hasOilGun = true;
                break;
            case ModifierTypes.PAINT_GUN:
                GetComponent<PlayerPaintGun>().hasPaintGun = true;
                break;
        }
    }

    public void ResetModifiers()
    {
        switch (currentModifier)
        {
            case ModifierTypes.PLUNGER:
                GetComponent<PlayerThrowPlunger>().hasPlunger = false;
                break;
            case ModifierTypes.UMBRELLA:
                break;
            case ModifierTypes.OIL:
                GetComponent<PlayerOilGun>().hasOilGun = false;
                break;
            case ModifierTypes.PAINT_GUN:
                GetComponent<PlayerPaintGun>().hasPaintGun = false;
                break;
            case ModifierTypes.COUNT:
            case ModifierTypes.NONE:
            default:
                break;
        }
        currentModifier = ModifierTypes.NONE;
    }

}
