using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetRadialMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Transform radialMenu = GameObject.Find("RadialMenus").transform;
        radialMenu.SetParent(transform);
    }

}
