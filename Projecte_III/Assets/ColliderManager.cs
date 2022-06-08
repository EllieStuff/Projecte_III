using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderManager : MonoBehaviour
{
    public List<string> tagsToListen = new List<string>();
    public bool triggered = false;


    private void OnTriggerStay(Collider other)
    {
        if (tagsToListen.Contains(other.tag))
        {
            triggered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (tagsToListen.Contains(other.tag))
        {
            triggered = false;
        }
    }

}
