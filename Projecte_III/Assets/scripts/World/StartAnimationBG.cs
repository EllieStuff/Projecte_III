using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAnimationBG : MonoBehaviour
{
    private float timer = 2.5f;

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
        {
            GetComponent<Animator>().enabled = false;
            Destroy(this);
        }
        else
            timer -= Time.deltaTime;
    }
}
