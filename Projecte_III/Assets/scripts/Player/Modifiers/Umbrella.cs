using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Umbrella : MonoBehaviour
{
    bool umbrellaActivated;
    float umbrellaTimer = 10;
    [SerializeField] GameObject umbrellaGameObject;
    Vector3 originalPos;

    // Start is called before the first frame update

    private void Start()
    {
        umbrellaGameObject.SetActive(false);
        originalPos = umbrellaGameObject.transform.localPosition;
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
                umbrellaGameObject.transform.localPosition = originalPos;
            }
        }
    }

    public void ActivateUmbrella(Quaternion direction, bool moveUmbrellaPivot)
    {
        umbrellaGameObject.transform.localRotation = direction;
        
        if (moveUmbrellaPivot)
            umbrellaGameObject.transform.localPosition -= new Vector3(0, 0, 1);
        
        umbrellaActivated = true;
    }
}
