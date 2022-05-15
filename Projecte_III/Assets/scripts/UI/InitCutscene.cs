using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitCutscene : MonoBehaviour
{
    [SerializeField] float disableTime = 9.0f;

    GlobalMenuInputs inputSystem;

    // Start is called before the first frame update
    void Start()
    {
        inputSystem = GameObject.Find("MenuCam").transform.GetChild(0).GetComponent<GlobalMenuInputs>();
        StartCoroutine(WaitForDisable());
    }

    private void Update()
    {
        if(inputSystem.StartBttnReleased)
        {
            gameObject.SetActive(false);
        }
    }

    IEnumerator WaitForDisable()
    {
        yield return new WaitForSeconds(disableTime);
        gameObject.SetActive(false);
    }

}
