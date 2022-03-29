using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomModifierGet : MonoBehaviour
{
    public GameObject[] modifiers;
    GameObject showModifierInstance;
    bool getModifier;
    float timerRoll = 0;
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
        if(hasModifier && inputs.UseGadget) 
        {
            switch (modifierIndex)
            {
                case 0:
                    GetComponent<PlayerThrowPlunger>().Activate();
                    break;
                case 1:
                    GetComponent<PlayerAlaDelta>().Activate();
                    break;
                case 2:

                    break;
            }
        }
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
                yield return new WaitForSeconds(0.2f);
                if(timerRoll > 0)
                    Destroy(showModifierInstance);
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    showModifierInstance.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
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
