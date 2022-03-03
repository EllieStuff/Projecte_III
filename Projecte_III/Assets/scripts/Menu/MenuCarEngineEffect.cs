using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCarEngineEffect : MonoBehaviour
{
    private Transform quadChasisShake;
    private float timerShake = 0;

    // Start is called before the first frame update
    void Start()
    {
        quadChasisShake = transform.GetChild(0).GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        timerShake += Time.deltaTime;

        if (timerShake >= 1000)
            timerShake = 0;

        quadChasisShake.localPosition += new Vector3(0, Mathf.Sin(timerShake * 75) / 400, 0);
    }
}
