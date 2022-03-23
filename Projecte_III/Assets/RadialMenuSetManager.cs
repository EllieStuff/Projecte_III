using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RadialMenuSetManager : MonoBehaviour
{
    [SerializeField] GameObject[] radialMenuSets;
    [SerializeField] bool inBuild = false;
    [SerializeField] GameObject[] radialMenuSetsBuild;
    PlayersManager playersManager;
    int prevSetupNum = 0;
    string sceneName = "";
    bool sceneChecked = false;

    // Start is called before the first frame update
    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        if (inBuild) radialMenuSets = radialMenuSetsBuild;
    }
    private void Start()
    {
        playersManager = GameObject.FindGameObjectWithTag("PlayersManager").GetComponent<PlayersManager>();
        SetUpRadialMenu(playersManager.numOfPlayers - 1);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        sceneName = scene.name;
        if (scene.name.Contains("Building Scene"))
        {
            // Do nothing
        }
        else if(!sceneChecked)
        {
            SetUpRadialMenu(playersManager.numOfPlayers - 1);
            sceneChecked = true;
        }

    }


    void SetUpRadialMenu(int _setupNum)
    {
        for (int i = 0; i < radialMenuSets.Length; i++)
        {
            if (i == _setupNum)
                radialMenuSets[i].SetActive(true);
            else
                radialMenuSets[i].SetActive(false);
        }
        prevSetupNum = _setupNum;
    }
    //public void SetModifiersToAllRadialMenus()
    //{
    //    for (int i = 0; i < radialMenuSets.Length; i++)
    //    {
    //        radialMenuSets[i].SetActive(true);
    //        if (i != prevSetupNum)
    //        {
    //            Transform prevRMSet = radialMenuSets[prevSetupNum].transform;
    //            for (int j = 0; j < radialMenuSets[i].transform.childCount; j++)
    //            {
    //                if (j >= prevRMSet.childCount)
    //                {
    //                    Debug.LogError("Previous RadialMenuSet amount shouldn't be bigger than current's");
    //                    return;
    //                }
    //                Transform prevRM = prevRMSet.GetChild(j);
    //                Transform currRM = radialMenuSets[i].transform.GetChild(j);

    //                Transform prevBuildingRM = prevRM.GetChild(0);
    //                Transform buildingRM = currRM.GetChild(0);
    //                for (int k = 0; k < prevBuildingRM.childCount; k++)
    //                {
    //                    GameObject.Instantiate(prevBuildingRM.GetChild(k), buildingRM);
    //                }
    //            }
    //        }
    //    }
    //}
    public void SetModifiersToChosenRMSet(int _setupNum)
    {
        if (playersManager.gameMode == PlayersManager.GameModes.MONO || prevSetupNum == _setupNum) 
            return;

        Transform prevRMSet = radialMenuSets[prevSetupNum].transform;
        Transform currRMSet = radialMenuSets[_setupNum].transform;
        currRMSet.gameObject.SetActive(true);
        for(int i = 0; i < currRMSet.childCount; i++)
        {
            if(i >= prevRMSet.childCount)
            {
                Debug.LogError("Previous RadialMenuSet amount shouldn't be bigger than current's");
                return;
            }

            Transform prevBuildingRM = prevRMSet.GetChild(i).GetChild(0);
            Transform currBuildingRM = currRMSet.GetChild(i).GetChild(0);
            for(int j = 0; j < prevBuildingRM.childCount; j++)
            {
                GameObject.Instantiate(prevBuildingRM.GetChild(j), currBuildingRM);
            }
        }
    }


    public Transform GetActiveSet()
    {
        return radialMenuSets[playersManager.numOfPlayers - 1].transform;
    }

}
