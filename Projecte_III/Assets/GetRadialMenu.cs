using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetRadialMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RadialMenuSetManager radialMenuSetManager = GameObject.FindGameObjectWithTag("RadialMenuManager").GetComponent<RadialMenuSetManager>();
        //radialMenuSetManager.SetModifiersToAllRadialMenus();
        Transform radialMenu = radialMenuSetManager.GetActiveSet();
        radialMenu.SetParent(transform);
    }

}
