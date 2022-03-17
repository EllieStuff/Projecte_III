using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetRadialMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Transform radialMenu = GameObject.FindGameObjectWithTag("RadialMenuManager").transform;
        radialMenu.SetParent(transform);
    }

}
