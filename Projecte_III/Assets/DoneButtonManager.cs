using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoneButtonManager : MonoBehaviour
{
    [SerializeField] string nextScene;
    [SerializeField] internal Color selectedBttnImgColor;
    [SerializeField] Color readyBttnImgColor;

    DoneButtonScript[] doneButtonScripts;

    // Start is called before the first frame update
    void Start()
    {
        doneButtonScripts = new DoneButtonScript[transform.childCount];
        for(int i = 0; i < doneButtonScripts.Length; i++)
        {
            doneButtonScripts[i] = transform.GetChild(i).GetComponent<DoneButtonScript>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (AllPlayersReady())
        {
            StartCoroutine(ChangeSceneEvent());
        }
    }


    bool AllPlayersReady()
    {
        foreach(DoneButtonScript button in doneButtonScripts)
        {
            if (!button.isReady) return false;
        }

        return true;
    }

    IEnumerator ChangeSceneEvent()
    {
        yield return new WaitForSeconds(0.5f);
        foreach (DoneButtonScript button in doneButtonScripts)
        {
            StartCoroutine(button.LerpBttnImgColor(readyBttnImgColor));
        }
        yield return new WaitForSeconds(1.5f);
        Debug.Log("Changing Scene");
        GameObject.FindGameObjectWithTag("SceneManager").GetComponent<LoadSceneManager>().ChangeScene(nextScene);
    }

}
