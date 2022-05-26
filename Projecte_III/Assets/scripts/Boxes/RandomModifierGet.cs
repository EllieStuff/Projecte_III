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
    [HideInInspector] public bool canUseModifier = true;

    PlayersHUD playerHud = null;

    internal bool IAItemTrigger = false;
    internal bool IALeft, IARight, IAUp, IADown;

    private void Start()
    {
        inputs = GetComponent<PlayerInputs>();
        GetComponent<PlayerThrowPlunger>().SetPlungerModifier();
        ResetModifiers();
    }

    private void FixedUpdate()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Contains("Building")) return;

        if(showModifierInstance != null)
        {
            showModifierInstance.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        }

        if (/*currentModifier != ModifierTypes.NONE && */(inputs.ShootForward || inputs.ShootBackwards || inputs.ShootLeft || inputs.ShootRight || IAItemTrigger) && canUseModifier) 
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
                    if (inputs.ShootForward || IAUp)
                        plunger.Activate(transform.TransformDirection(0, 0, 1));
                    else if (inputs.ShootBackwards || IADown)
                        plunger.Activate(transform.TransformDirection(0, 0, -1));
                    else if (inputs.ShootLeft || IALeft)
                        plunger.Activate(transform.TransformDirection(-1, 0, 0));
                    else if (inputs.ShootRight || IARight)
                        plunger.Activate(transform.TransformDirection(1, 0, 0));

                    //plunger.hasPlunger = false;
                    timerModifier = INIT_TIMER_MODIFIER;
                    break;

                case ModifierTypes.UMBRELLA:
                    Umbrella umbrella = GetComponent<Umbrella>();
                    if (inputs.ShootForward || IAUp)
                        umbrella.ActivateUmbrella(Quaternion.Euler(90, 180, 0), true);
                    else if (inputs.ShootBackwards || IADown)
                        umbrella.ActivateUmbrella(Quaternion.Euler(-90, 180, 0), false);
                    else if (inputs.ShootLeft || IALeft)
                        umbrella.ActivateUmbrella(Quaternion.Euler(90, 180, 90), true);
                    else if (inputs.ShootRight || IARight)
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
                    if (inputs.ShootForward || IAUp)
                        paintGun.Activate(Quaternion.Euler(0, 180, 0));
                    else if (inputs.ShootBackwards || IADown)
                        paintGun.Activate(Quaternion.Euler(0, 0, 0));
                    else if (inputs.ShootLeft || IALeft)
                        paintGun.Activate(Quaternion.Euler(0, 90, 0));
                    else if (inputs.ShootRight || IARight)
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

                    if (inputs.ShootForward || IAUp)
                        boost.Active(transform.forward, 1.0f, 1.0f);
                    else if (inputs.ShootBackwards || IADown)
                        boost.Active(-transform.forward, 0.25f, 0.3f);
                    else if (inputs.ShootLeft || IALeft)
                        boost.Active(-transform.right, 0.5f, 1.0f);
                    else if (inputs.ShootRight || IARight)
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
