using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSoundInstance : MonoBehaviour
{
    float timerDestroy = 5;

    // Update is called once per frame
    void Update()
    {
        timerDestroy -= Time.deltaTime;
        if (timerDestroy <= 0)
            Destroy(this.gameObject);
    }
}
