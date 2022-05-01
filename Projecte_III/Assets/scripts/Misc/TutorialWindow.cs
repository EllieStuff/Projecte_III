using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialWindow : MonoBehaviour
{
    private float timer = 1;
    private bool closed;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
            closed = true;

        if(timer > 0)
            timer -= Time.deltaTime; 
        else
        {
            if(!closed)
            {
                Time.timeScale = 0.000001f;
            }
            else
            {
                GetComponent<Image>().enabled = false;
                transform.GetChild(0).gameObject.SetActive(true);
                Time.timeScale = 1;
                Destroy(this);
            }
        }
    }
}
