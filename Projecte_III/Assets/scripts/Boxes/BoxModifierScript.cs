using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxModifierScript : MonoBehaviour
{
    bool destroy;
    float timer = 2;

    public PlayersHUDManager hudManager;


    private void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.Contains("Player") && !destroy) 
        {
            //collision.transform.GetComponent<RandomModifierGet>().GetModifier();
            destroy = true;

            PlayersHUD currHud = hudManager.GetPlayerHUD(collision.transform.parent.GetComponent<PlayerData>().id);
            currHud.RollModifiers();
        }
    }

    private void Update()
    {
        if(destroy)
        {
            timer -= Time.deltaTime;
            if (timer <= 0 || transform.localScale.magnitude <= 0.2f)
                Destroy(gameObject);
            else
                transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
        }
    }
}
