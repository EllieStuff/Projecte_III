using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartRace : MonoBehaviour
{
    [SerializeField] PlayerInputs inputs;


    // Update is called once per frame
    void Update()
    {
        if (inputs.Start)
        {
            Button bttn = GetComponent<Button>();
            bttn.Select();
            bttn.onClick.Invoke();
        }
    }

}
