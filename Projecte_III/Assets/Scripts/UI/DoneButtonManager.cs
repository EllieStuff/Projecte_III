using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoneButtonManager : MonoBehaviour
{
    [SerializeField] internal Color selectedBttnImgColor;
    [SerializeField] Color readyBttnImgColor;

    public int buttonsActive = 0;
    [SerializeField] string goToScene;
    [SerializeField] bool activateWith1Player = false;

    [SerializeField] ColorsAndAISelector changeColorManager;
    PlayersManager playersManager;

    bool loadingLevel = false;

    DoneButtonScript[] doneButtonScripts;

    // Start is called before the first frame update
    void Start()
    {
        playersManager = GameObject.FindGameObjectWithTag("PlayersManager").GetComponent<PlayersManager>();
        doneButtonScripts = new DoneButtonScript[transform.childCount];
        for(int i = 0; i < doneButtonScripts.Length; i++)
        {
            doneButtonScripts[i] = transform.GetChild(i).GetComponent<DoneButtonScript>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (AllPlayersReady() && !loadingLevel)
        {
            loadingLevel = true;
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
        yield return new WaitForSeconds(0.2f);
        if(!activateWith1Player && buttonsActive == 1)
        {
            doneButtonScripts[0].SetReady();
            loadingLevel = false;
            foreach (DoneButtonScript button in doneButtonScripts)
            {
                StartCoroutine(button.LerpBttnImgColor(Color.white));
            }
            yield break;
        }
        Debug.Log("Changing Scene");

        if (ColorsAndAISelector.GetAi_Active()) ActivateAIs();
        else
        {
            Transform[] _playersCopy = new Transform[buttonsActive];

            for (int i = 0; i < playersManager.numOfPlayers; i++)
            {
                if (i < buttonsActive) _playersCopy[i] = playersManager.players[i];
                else Destroy(playersManager.players[i].parent.gameObject);
            }

            if(buttonsActive != playersManager.numOfPlayers)
            {
                playersManager.players = _playersCopy;
                playersManager.numOfPlayers = buttonsActive;
            }
        }

        LoadSceneManager sceneManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<LoadSceneManager>();
        sceneManager.ChangeScene(goToScene);
    }

    private void ActivateAIs()
    {
        
        for (int i = buttonsActive; i < playersManager.players.Length; i++)
        {
            changeColorManager.transform.GetChild(i).GetComponent<ChangeColor>().enabled = true;
            Transform playerTrans = playersManager.GetPlayer(i);
            if (playerTrans == null)
            {
                playersManager = GameObject.FindGameObjectWithTag("PlayersManager").GetComponent<PlayersManager>();
                playersManager.GetPlayer(i).GetComponent<PlayerVehicleScript>().iaEnabled = true;
            }
            else
            {
                playersManager.GetPlayer(i).GetComponent<PlayerVehicleScript>().iaEnabled = true;
            }
        }
    }
}
