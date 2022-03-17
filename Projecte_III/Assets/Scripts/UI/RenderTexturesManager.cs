using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderTexturesManager : MonoBehaviour
{
    [SerializeField] GameObject[] renderSetups;
    
    //Transform[] renderTextures = new Transform[4];
    int currState = 0;
    PlayersManager.GameModes gameMode;

    // Start is called before the first frame update
    void Awake()
    {
        gameMode = GameObject.FindGameObjectWithTag("PlayersManager").GetComponent<PlayersManager>().gameMode;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetRenderSetup(int _numOfPlayers)
    {
        currState = _numOfPlayers - 1;
        for(int i = 0; i < renderSetups.Length; i++)
        {
            renderSetups[i].SetActive(currState == i);
        }
    }
    public Transform GetRenderTexture(int _idx)
    {
        if (gameMode == PlayersManager.GameModes.MONO) return renderSetups[0].transform.GetChild(0);

        return renderSetups[currState].transform.GetChild(_idx);
    }

}
