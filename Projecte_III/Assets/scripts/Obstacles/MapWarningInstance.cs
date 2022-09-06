using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapWarningInstance : MonoBehaviour
{
    internal float time = 1;

    private void Start()
    {
        GetComponent<AudioSource>().pitch = Random.Range(0.5f, 1);
    }

    void Update()
    {
        if (time <= 0)
            Destroy(gameObject);

        time -= Time.deltaTime;
    }
}
