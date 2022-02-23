using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[Serializable]
class Scores
{
    public float[] seconds { get; set; }
    public int[] minutes { get; set; }
}

public class RaceFinishedScript : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI timeTextLocal;
    public GameObject newRecord;

    private void Update()
    {
        if(GetComponent<Image>().color.a < 1)
        {
            GetComponent<Image>().color += new Color(0, 0, 0, Time.deltaTime / 2);
        }    
    }

    void Start()
    {
        bool newMaxScore = true;
        try
        {
            timeTextLocal.text = timeText.text;
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(@"Scores.dat", FileMode.Open, FileAccess.Read);
            Scores scores = (Scores)formatter.Deserialize(stream);

            for (int i = 0; i < scores.minutes.Length; i++)
            {
                if (scores.seconds[i] != 0 && scores.minutes[i] <= timeText.GetComponent<UITimerChrono>().minute && scores.seconds[i] < timeText.GetComponent<UITimerChrono>().second)
                {
                    newMaxScore = false;
                    break;
                }
            }

            for (int i = 0; i < scores.minutes.Length; i++)
            {
                if(scores.minutes[i] == 0 && scores.seconds[i] == 0)
                {
                    scores.minutes[i] = timeText.GetComponent<UITimerChrono>().minute;
                    scores.seconds[i] = timeText.GetComponent<UITimerChrono>().second;
                    Debug.Log(scores.seconds[i]);
                    break;
                }
            }

            stream.Close();

            IFormatter formatter2 = new BinaryFormatter();
            Stream stream2 = new FileStream(@"Scores.dat", FileMode.Open, FileAccess.Write);
            formatter2.Serialize(stream2, scores);

            stream2.Close();

        }
        catch(Exception)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(@"Scores.dat", FileMode.Create, FileAccess.Write);

            Scores scores = new Scores();
            scores.minutes = new int[100];
            scores.seconds = new float[100];

            for (int i = 0; i < scores.minutes.Length; i++)
            {
                if(i == 0)
                {
                    scores.minutes[i] = timeText.GetComponent<UITimerChrono>().minute;
                    scores.seconds[i] = timeText.GetComponent<UITimerChrono>().second;
                }
                else
                {
                    scores.minutes[i] = 0;
                    scores.seconds[i] = 0;
                }
            }

            formatter.Serialize(stream, scores);
            stream.Close();

        }

        timeTextLocal.text = timeText.text;

        if (newMaxScore)
        {
            newRecord.SetActive(true);
        }
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
