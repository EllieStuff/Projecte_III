using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Umbrella : MonoBehaviour
{
    bool umbrellaActivated;
    float umbrellaTimer = 10;
    [SerializeField] GameObject umbrellaGameObject;

    // Start is called before the first frame update

    private void Start()
    {
        umbrellaGameObject.SetActive(false);
    }

    private void Update()
    {
        if(umbrellaActivated)
        {
            umbrellaGameObject.SetActive(true);
            umbrellaTimer -= Time.deltaTime;

            if(umbrellaTimer <= 0)
            {
                umbrellaTimer = 10;
                umbrellaActivated = false;
                umbrellaGameObject.SetActive(false);
            }
        }
    }

    public void ActivateUmbrella()
    {
        umbrellaActivated = true;
    }
}
