using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjs : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject UI = GameObject.Find("DontDestroyOnLoad_UI");
        if(UI != null) Destroy(UI);

        GameObject playersManager = GameObject.FindGameObjectWithTag("PlayersManager");
        if(playersManager != null) Destroy(playersManager);

        Destroy(gameObject);
    }
}
