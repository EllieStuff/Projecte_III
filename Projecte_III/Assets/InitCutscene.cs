using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitCutscene : MonoBehaviour
{
    [SerializeField] float disableTime = 9.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitForDisable());

    }

    IEnumerator WaitForDisable()
    {
        yield return new WaitForSeconds(disableTime);
        gameObject.SetActive(false);
    }

}
