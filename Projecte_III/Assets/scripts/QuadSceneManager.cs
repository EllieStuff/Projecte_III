using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadSceneManager : MonoBehaviour
{
    //private GameObject quad;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
