using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxModifierScript : MonoBehaviour
{
    bool destroy;
    float timer = 2;

    public PlayersHUDManager hudManager;

    private Light cubeLight;
    private bool alphaUpOrDown;


    private void Start()
    {
        alphaUpOrDown = true;
        hudManager = GameObject.Find("HUD").transform.GetComponentInChildren<PlayersHUDManager>();
        cubeLight = GetComponent<Light>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.Contains("Player") && !destroy) 
        {
            //collision.transform.GetComponent<RandomModifierGet>().GetModifier();
            destroy = true;
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
            PlayersHUD currHud = hudManager.GetPlayerHUD(collision.transform.parent.GetComponent<PlayerData>().id);
            currHud.RollModifiers();
        }
    }

    private void Update()
    {
        if (cubeLight.range >= 1 || cubeLight.range <= 0.7)
            alphaUpOrDown = !alphaUpOrDown;

        if (cubeLight.range > 0.7 && !alphaUpOrDown)
            cubeLight.range -= Time.deltaTime * 0.2f;
        else if(cubeLight.range < 1 && alphaUpOrDown)
            cubeLight.range += Time.deltaTime * 0.2f;

        if (destroy)
        {
            timer -= Time.deltaTime;
            if (timer <= 0 || transform.localScale.magnitude <= 0.2f)
                Destroy(gameObject);
            else
                transform.localScale -= new Vector3(5.0f, 5.0f, 5.0f) * Time.deltaTime;
        }
    }
}
