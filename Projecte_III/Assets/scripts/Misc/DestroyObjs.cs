using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjs : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(GameObject.Find("DontDestroyOnLoad_UI"));
        Destroy(GameObject.FindGameObjectWithTag("PlayersManager"));

        Destroy(gameObject);
    }
}
