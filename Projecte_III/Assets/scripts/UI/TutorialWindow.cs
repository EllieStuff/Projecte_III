using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialWindow : MonoBehaviour
{
    [SerializeField] GameObject uiSet;

    private float timer = 1;
    private bool closed;

    void Start()
    {
        GetComponent<Image>().enabled = true;
        uiSet.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
            closed = true;

        if (Time.timeScale > 0.000001f)
            Time.timeScale -= Time.deltaTime * 0.75f;
        else
            Time.timeScale = 0.000001f;

        if (timer > 0) 
        {
            timer -= Time.deltaTime;
        }
        else if(closed)
        {
            GetComponent<Image>().enabled = false;
            transform.GetChild(0).gameObject.SetActive(true);
            Time.timeScale = 1;
            uiSet.SetActive(true);
            Destroy(this);
        }
    }
}
