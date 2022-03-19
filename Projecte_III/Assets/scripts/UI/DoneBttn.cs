using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoneBttn : MonoBehaviour
{
    Button bttn;

    // Start is called before the first frame update
    void Start()
    {
        bttn = GetComponent<Button>();

        bttn.onClick.AddListener(DoneActive);
    }

    void DoneActive()
    {
        LoadSceneManager.Instance.ChangeScene("Scorching Desert");
    }
}
