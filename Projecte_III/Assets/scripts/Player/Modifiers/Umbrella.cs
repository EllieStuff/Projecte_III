using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Umbrella : MonoBehaviour
{
    public bool umbrellaEnabled;
    [SerializeField] GameObject umbrellaGameObject;

    // Start is called before the first frame update

    private void Start()
    {
        umbrellaGameObject.SetActive(false);
    }

    public void ActivateUmbrella()
    {
        umbrellaGameObject.SetActive(true);
    }

    public void StopUmbrella()
    {
        umbrellaGameObject.SetActive(false);
    }
}
