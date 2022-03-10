using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderTexturesManager : MonoBehaviour
{
    [SerializeField] Transform[] renderTextures;

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


    public Transform GetRenderTexture(int _idx)
    {
        if (gameMode == PlayersManager.GameModes.MONO) return renderTextures[0];

        return renderTextures[_idx];
    }

}
