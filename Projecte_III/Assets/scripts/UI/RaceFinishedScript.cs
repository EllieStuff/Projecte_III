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
    public GameObject textInstance;
    public Transform scoresPivot;
    public GameObject scoreGameObject;
    private Image bgEndGame;

    private void Update()
    {
        if(bgEndGame.color.a < 1)
        {
            bgEndGame.color += new Color(0, 0, 0, Time.deltaTime / 2);
        }    
    }

    void Start()
    {
        bgEndGame = GetComponent<Image>();
        bool newMaxScore = true;
        try
        {
            timeTextLocal.text = timeText.text;
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(@"Scores.dat", FileMode.Open, FileAccess.Read);
            Scores scores = (Scores)formatter.Deserialize(stream);

            UITimerChrono timerChronoUI = timeText.GetComponent<UITimerChrono>();

            for (int i = 0; i < scores.minutes.Length; i++)
            {
                if (scores.seconds[i] != 0 && scores.minutes[i] <= timerChronoUI.minute && scores.seconds[i] < timerChronoUI.second)
                {
                    newMaxScore = false;
                    break;
                }
            }

            for (int i = 0; i < scores.minutes.Length; i++)
            {
                if(scores.minutes[i] == 0 && scores.seconds[i] == 0)
                {
                    scores.minutes[i] = timerChronoUI.minute;
                    scores.seconds[i] = timerChronoUI.second;
                    break;
                }
            }

            stream.Close();

            IFormatter formatter2 = new BinaryFormatter();
            Stream stream2 = new FileStream(@"Scores.dat", FileMode.Open, FileAccess.Write);
            formatter2.Serialize(stream2, scores);

            stream2.Close();

            Array.Sort(scores.seconds);
            Array.Sort(scores.minutes);

            int index = 0;

            for (int i = 0; i < 100; i++)
            {
                if (index >= 8)
                    break;

                if(scores.seconds[i] != 0)
                {
                    GameObject instance = Instantiate(textInstance, scoresPivot);
                    TextMeshProUGUI text = instance.GetComponent<TextMeshProUGUI>();

                    if (scores.minutes[i] < 10 && scores.seconds[i] < 10)
                        text.text = "0" + scores.minutes[i] + ":0" + Mathf.Round(scores.seconds[i] * 100) * 0.01f;
                    else if (scores.minutes[i] < 10 && scores.seconds[i] >= 10)
                        text.text = "0" + scores.minutes[i] + ":" + Mathf.Round(scores.seconds[i] * 100) * 0.01f;
                    else if (scores.minutes[i] >= 10 && scores.seconds[i] < 10)
                        text.text = "0" + scores.minutes[i] + Mathf.Round(scores.seconds[i] * 100) * 0.01f;
                    else if (scores.minutes[i] >= 10 && scores.seconds[i] >= 10)
                        text.text = scores.minutes[i] + ":" + Mathf.Round(scores.seconds[i] * 100) * 0.01f;

                    instance.SetActive(true);

                    index++;
                }
            }

        }
        catch(Exception)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(@"Scores.dat", FileMode.Create, FileAccess.Write);

            Scores scores = new Scores();
            scores.minutes = new int[100];
            scores.seconds = new float[100];

            UITimerChrono timerChronoUI = timeText.GetComponent<UITimerChrono>();

            for (int i = 0; i < scores.minutes.Length; i++)
            {
                if(i == 0)
                {
                    scores.minutes[i] = timerChronoUI.minute;
                    scores.seconds[i] = timerChronoUI.second;
                }
                else
                {
                    scores.minutes[i] = 0;
                    scores.seconds[i] = 0;
                }
            }

            formatter.Serialize(stream, scores);
            stream.Close();

            GameObject instance = Instantiate(textInstance, scoresPivot);
            TextMeshProUGUI text = instance.GetComponent<TextMeshProUGUI>();

            if (scores.minutes[0] < 10 && scores.seconds[0] < 10)
                text.text = "0" + scores.minutes[0] + ":0" + Mathf.Round(scores.seconds[0] * 100) * 0.01f;
            else if (scores.minutes[0] < 10 && scores.seconds[0] >= 10)
                text.text = "0" + scores.minutes[0] + ":" + Mathf.Round(scores.seconds[0] * 100) * 0.01f;
            else if (scores.minutes[0] >= 10 && scores.seconds[0] < 10)
                text.text = "0" + scores.minutes[0] + Mathf.Round(scores.seconds[0] * 100) * 0.01f;
            else if (scores.minutes[0] >= 10 && scores.seconds[0] >= 10)
                text.text = scores.minutes[0] + ":" + Mathf.Round(scores.seconds[0] * 100) * 0.01f;

            instance.SetActive(true);
        }

        timeTextLocal.text = timeText.text;

        if (newMaxScore)
        {
            newRecord.SetActive(true);
        }
    }

    public void openScoreMenu()
    {
        if(!scoreGameObject.active)
            scoreGameObject.SetActive(true);
        else
            scoreGameObject.SetActive(false);
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
