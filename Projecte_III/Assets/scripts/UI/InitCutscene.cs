using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitCutscene : MonoBehaviour
{
    [SerializeField] float disableTime = 9.0f;
    [SerializeField] Button defaultButton;

    GlobalMenuInputs inputSystem;

    // Start is called before the first frame update
    void Start()
    {
        inputSystem = GameObject.Find("MenuCam").transform.GetChild(0).GetComponent<GlobalMenuInputs>();
        StartCoroutine(WaitForDisable());
    }

    private void Update()
    {
        if(inputSystem.StartBttnReleased || inputSystem.EscapeBttnPressed 
            || inputSystem.AcceptReleased || inputSystem.DeclineReleased
            || Input.GetKeyUp(KeyCode.Mouse0))
        {
            StopAllCoroutines();
            defaultButton.Select();
            gameObject.SetActive(false);
        }
    }

    IEnumerator WaitForDisable()
    {
        yield return new WaitForSeconds(disableTime);
        gameObject.SetActive(false);
    }

}
