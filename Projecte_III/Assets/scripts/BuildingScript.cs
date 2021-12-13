using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject blockPrefab;

    private GameObject block;

    private GameObject playerBlocks;

    private bool editorMode;

    public Vector3 depthVector;

    void Start()
    {
        editorMode = true;

        playerBlocks = GameObject.Find("Player").transform.GetChild(2).gameObject;

        InstantiateBlock();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Backspace))
            ChangeMode();

        if(editorMode)
        {
            EditorUpdate();
        }
    }

    void EditorUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && block.GetComponent<BlockScript>().touchingAnother)
        {
            block.GetComponent<BlockScript>().BlockPlaced(true);

            InstantiateBlock();
        }
    }

    public void ChangeMaterial(GameObject mat)
    {
        blockPrefab = mat;

        Destroy(block);

        InstantiateBlock();
    }

    void InstantiateBlock()
    {
        block = Instantiate(blockPrefab, Input.mousePosition, Quaternion.identity, playerBlocks.transform);
        block.GetComponent<BlockScript>().playerMode = true;
        block.GetComponent<BlockScript>().depthVector = depthVector;
        block.GetComponent<BlockScript>().buildingScriptGameObject = this.gameObject;
        block.GetComponent<ParticleSystem>().Stop();
    }

    void ChangeMode()
    {
        editorMode = !editorMode;
        if(editorMode)
        {
            GameObject player = GameObject.Find("Player").gameObject;
            player.GetComponent<PlayerVehicleScript>().EditorStart();

            for (int i = 0; i < player.transform.childCount; i++)
            {
                if (player.transform.GetChild(i).GetComponent<BlockScript>() != null)
                    player.transform.GetChild(i).GetComponent<BlockScript>().EditorStart();
            }

            GameObject.Find("Enemy").GetComponent<EnemyVehicleScript>().EditorStart();
            InstantiateBlock();
        }
        else
        {
            GameObject player = GameObject.Find("Player").gameObject;
            player.GetComponent<PlayerVehicleScript>().BattleStart();

            for (int i = 0; i < player.transform.childCount; i++)
            {
                if(player.transform.GetChild(i).GetComponent<BlockScript>() != null)
                player.transform.GetChild(i).GetComponent<BlockScript>().BattleStart();
            }

            GameObject.Find("Enemy").GetComponent<EnemyVehicleScript>().BattleStart();

            //Comprovar si el block esta colocat, si no ho esta destruir-ho
            if(block != null)
            {
                block.GetComponent<BlockScript>().DestroyBlock();
                block = null;
            }
        }

        Camera.main.transform.parent.GetComponent<CameraScript>().ChangeMode();
    }

    public bool EditorModeActive()
    {
        return editorMode;
    }
}
