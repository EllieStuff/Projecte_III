using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitCutscene : MonoBehaviour
{
    [SerializeField] float disableTime = 9.0f;

    // Start is called before the first frame update
    void Start()
    {
        string watchInitCutscene = PlayerPrefs.GetString("InitCutsceneEnabled", "true");
        if (watchInitCutscene == "false")
        {
            PlayerPrefs.SetString("InitCutsceneEnabled", "true");
            gameObject.SetActive(false);
        }
        else if(watchInitCutscene == "true")
        {
            StartCoroutine(WaitForDisable());
        }

    }

    IEnumerator WaitForDisable()
    {
        yield return new WaitForSeconds(disableTime);
        gameObject.SetActive(false);
    }

}
