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
    [SerializeField] Transform changeColorManager;

    bool loadingLevel = false;

    PlayersManager playersManager;
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

        playersManager.numOfPlayers = buttonsActive;

        for (int i = playersManager.numOfPlayers; i < playersManager.players.Length; i++)
        {
            changeColorManager.GetChild(i).GetComponent<ChangeColor>().enabled = true;
            playersManager.GetPlayer(i).GetComponent<PlayerVehicleScript>().iaEnabled = true;
            TextMeshPro tmPro = playersManager.GetPlayer(i).Find("vehicleChasis").Find("PlayerNumText").GetComponent<TextMeshPro>();
            tmPro.text = "CPU";
            tmPro.fontSize = 22;
            tmPro.transform.localPosition = 
                new Vector3(tmPro.transform.localPosition.x, tmPro.transform.localPosition.y + 0.1f, tmPro.transform.localPosition.z - 0.1f);
        }
        
        playersManager.numOfPlayers = 4;
        LoadSceneManager sceneManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<LoadSceneManager>();
        sceneManager.ChangeScene(goToScene);
    }
}
