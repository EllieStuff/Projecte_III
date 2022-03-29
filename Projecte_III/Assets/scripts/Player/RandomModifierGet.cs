using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomModifierGet : MonoBehaviour
{
    public GameObject[] modifiers;
    GameObject showModifierInstance;
    bool getModifier;
    float timerRoll = 0;

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

    IEnumerator ModifiersRoll()
    {
        while(getModifier)
        {
            if (timerRoll > 0)
            {
                timerRoll -= 0.5f;
                int randomInt = Random.Range(0, modifiers.Length);
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
                //put modifier

                Destroy(showModifierInstance);
                //____________
                getModifier = false;
            }
        }

        yield return 0;
    }
}
