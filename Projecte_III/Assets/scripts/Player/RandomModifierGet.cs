using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomModifierGet : MonoBehaviour
{
    public GameObject[] modifiers;
    GameObject showModifierInstance;
    bool getModifier;
    float timerRoll = 0;
    float timerModifier = 0;
    int modifierIndex;
    bool hasModifier;
    PlayerInputs inputs;

    private void Start()
    {
        inputs = GetComponent<PlayerInputs>();
    }

    // Update is called once per frame
    public void GetModifier()
    {
        if(timerRoll <= 0)
        {
            timerRoll = 2;
            getModifier = true;
            IEnumerator coroutine = ModifiersRoll();
            StartCoroutine(coroutine);
        }
    }

    private void Update()
    {
        if(showModifierInstance != null)
        {
            showModifierInstance.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        }

        if (hasModifier && inputs.UseGadget && timerModifier <= 0) 
        {
            switch (modifierIndex)
            {
                case 0:
                    GetComponent<PlayerThrowPlunger>().Activate();
                    timerModifier = 5;
                    break;
                case 1:
                    GetComponent<PlayerAlaDelta>().Activate();
                    timerModifier = 5;
                    break;
                case 2:
                    GetComponent<Umbrella>().ActivateUmbrella();
                    break;
            }
        }
        else
        {
                GetComponent<Umbrella>().StopUmbrella();
        }
        if (timerModifier > 0)
            timerModifier -= Time.deltaTime;
    }

    IEnumerator ModifiersRoll()
    {
        int randomInt = 0;
        while (getModifier)
        {
            if (timerRoll > 0)
            {
                timerRoll -= 0.5f;
                randomInt = Random.Range(0, modifiers.Length);
                showModifierInstance = Instantiate(modifiers[randomInt], new Vector3(transform.position.x , transform.position.y + 1, transform.position.z), transform.rotation);
                showModifierInstance.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                yield return new WaitForSeconds(0.2f);
                Destroy(showModifierInstance);
            }
            else
            {
                if(showModifierInstance != null)
                    Destroy(showModifierInstance);
                showModifierInstance = Instantiate(modifiers[randomInt], new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), transform.rotation);
                showModifierInstance.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                for (int i = 0; i < 3; i++)
                {
                    yield return new WaitForSeconds(0.5f);
                }
                Debug.Log(randomInt);
                //put modifier
                switch(randomInt)
                {
                    case 0:
                        GetComponent<PlayerAlaDelta>().hasAlaDelta = false;
                        GetComponent<PlayerThrowPlunger>().hasPlunger = true;
                        break;
                    case 1:
                        GetComponent<PlayerThrowPlunger>().hasPlunger = false;
                        GetComponent<PlayerAlaDelta>().hasAlaDelta = true;
                        break;
                    case 2:
                        //Umbrella
                        break;
                    case 3:
                        //Explosives
                        break;
                }
                modifierIndex = randomInt;
                Destroy(showModifierInstance);
                hasModifier = true;
                //____________
                getModifier = false;
            }
        }

        yield return 0;
    }
}
