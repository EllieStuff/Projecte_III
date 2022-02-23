using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RaceFinishedScript : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI timeTextLocal;

    private void Update()
    {
        if(GetComponent<Image>().color.a < 1)
        {
            GetComponent<Image>().color += new Color(0, 0, 0, Time.deltaTime / 2);
        }    
    }

    void Start()
    {
        timeTextLocal.text = timeText.text;
    }

    public void QuitMenu()
    {
        Destroy(GameObject.Find("Vehicle Set"));
        Destroy(GameObject.Find("[Debug Updater]"));
        SceneManager.LoadScene("Menu");
    }

    public void BuildMenu()
    {
        Destroy(GameObject.Find("Vehicle Set"));
        Destroy(GameObject.Find("[Debug Updater]"));
        SceneManager.LoadScene("Building Scene");
    }
}