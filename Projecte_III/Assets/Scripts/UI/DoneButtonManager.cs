using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoneButtonManager : MonoBehaviour
{
    [SerializeField] string nextScene;
    [SerializeField] internal Color selectedBttnImgColor;
    [SerializeField] Color readyBttnImgColor;

    public int buttonsActive = 0;

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
            PlayersManager playerManager = GameObject.FindGameObjectWithTag("PlayersManager").GetComponent<PlayersManager>();
            for (int i = 0; i < playerManager.numOfPlayers; i++)
            {
                playerManager.GetPlayerModifier(i).GetComponent<ModifierManager>().HideAllModifiersSpots();
            }
            StartCoroutine(ChangeSceneEvent());
        }
    }


    bool AllPlayersReady()
    {
        if (!doneButtonScripts[0].isActive) return false;

        foreach(DoneButtonScript button in doneButtonScripts)
        {
            if (button.isActive && !button.isReady) return false;
        }

        return true;
    }

    public DoneButtonScript GetButton(int _idx)
    {
        return doneButtonScripts[_idx];
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
        GameObject.FindGameObjectWithTag("PlayersManager").GetComponent<PlayersManager>().numOfPlayers = buttonsActive;
        GameObject.FindGameObjectWithTag("SceneManager").GetComponent<LoadSceneManager>().ChangeScene(nextScene);
    }

}
