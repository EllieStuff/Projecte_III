using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomModifierGet : MonoBehaviour
{
    public enum ModifierTypes { PLUNGER, HANG_GLIDER, UMBRELLA, OIL, PAINT_GUN, COUNT, NONE };

    public GameObject[] modifiers;
    GameObject showModifierInstance;
    float timerModifier = 0;

    PlayerInputs inputs;

    ModifierTypes currentModifier = ModifierTypes.NONE;

    PlayersHUD playerHud = null;

    private void Start()
    {
        inputs = GetComponent<PlayerInputs>();
    }

    //public void GetModifier()
    //{
    //    if(timerRoll <= 0)
    //    {
    //        timerRoll = 5;
    //        getModifier = true;
    //        IEnumerator coroutine = ModifiersRoll();
    //        StartCoroutine(coroutine);

    //        int randomInt = Random.Range(0, modifiers.Length);
    //    }
    //}

    private void FixedUpdate()
    {
        if(showModifierInstance != null)
        {
            showModifierInstance.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        }

        if (currentModifier != ModifierTypes.NONE && (inputs.ShootForward || inputs.ShootBackwards || inputs.ShootLeft || inputs.ShootRight) && timerModifier <= 0) 
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
                    timerModifier = 5;
                    break;
                case ModifierTypes.HANG_GLIDER:
                    PlayerAlaDelta hangGlider = GetComponent<PlayerAlaDelta>();
                    hangGlider.Activate();

                    hangGlider.hasAlaDelta = false;
                    timerModifier = 5;
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

                    break;
                case ModifierTypes.OIL:
                    break;
                case ModifierTypes.PAINT_GUN:
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
            case ModifierTypes.HANG_GLIDER:
                GetComponent<PlayerAlaDelta>().hasAlaDelta = true;
                break;
            case ModifierTypes.UMBRELLA:
                break;
            case ModifierTypes.OIL:
                break;
            case ModifierTypes.PAINT_GUN:
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
            case ModifierTypes.HANG_GLIDER:
                GetComponent<PlayerAlaDelta>().hasAlaDelta = false;
                break;
            case ModifierTypes.UMBRELLA:
                break;
            case ModifierTypes.OIL:
                break;
            case ModifierTypes.PAINT_GUN:
                break;
            case ModifierTypes.COUNT:
            case ModifierTypes.NONE:
            default:
                break;
        }
        currentModifier = ModifierTypes.NONE;
    }

    //IEnumerator ModifiersRoll()
    //{
    //    int randomInt = Random.Range(0, modifiers.Length);
    //    while (getModifier)
    //    {
    //        if (timerRoll > 0)
    //        {
    //            timerRoll -= 0.5f;

    //            if (randomInt < modifiers.Length - 1)
    //                randomInt++;
    //            else
    //                randomInt = 0;

    //            showModifierInstance = Instantiate(modifiers[randomInt], new Vector3(transform.position.x , transform.position.y + 1, transform.position.z), transform.rotation);
    //            showModifierInstance.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
    //            yield return new WaitForSeconds(0.2f);
    //            Destroy(showModifierInstance);
    //        }
    //        else
    //        {
    //            if(showModifierInstance != null)
    //                Destroy(showModifierInstance);
    //            showModifierInstance = Instantiate(modifiers[randomInt], new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), transform.rotation);
    //            showModifierInstance.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
    //            for (int i = 0; i < 3; i++)
    //            {
    //                yield return new WaitForSeconds(0.5f);
    //            }
    //            Debug.Log(randomInt);
    //            GetComponent<PlayerAlaDelta>().hasAlaDelta = false;
    //            GetComponent<PlayerThrowPlunger>().hasPlunger = false;
    //            //put modifier
    //            switch (randomInt)
    //            {
    //                case 0:
    //                    GetComponent<PlayerThrowPlunger>().hasPlunger = true;
    //                    break;
    //                case 1:
    //                    GetComponent<PlayerAlaDelta>().hasAlaDelta = true;
    //                    break;
    //                case 2:
    //                    //Umbrella
    //                    break;
    //                case 3:
    //                    //??
    //                    break;
    //            }
    //            modifierIndex = randomInt;
    //            Destroy(showModifierInstance);
    //            hasModifier = true;
    //            //____________
    //            getModifier = false;
    //        }
    //    }

    //    yield return 0;
    //}
}
