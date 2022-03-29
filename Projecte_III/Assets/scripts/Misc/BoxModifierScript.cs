using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxModifierScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.Contains("Player")) 
        {
            collision.transform.GetComponent<RandomModifierGet>().GetModifier();
            Destroy(gameObject);
        }
    }
}
