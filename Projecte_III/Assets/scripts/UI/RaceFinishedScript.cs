using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RaceFinishedScript : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI timeTextLocal;

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
