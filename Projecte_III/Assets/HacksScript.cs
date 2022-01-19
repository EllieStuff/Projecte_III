using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HacksScript : MonoBehaviour
{
    [SerializeField] Transform[] tpTransforms;
    [SerializeField] LoadSceneManager sceneLoader;

    PlayerVehicleScript playerScript;
    Quaternion rotMargin;

    // Start is called before the first frame update
    void Start()
    {
        sceneLoader = GetComponent<LoadSceneManager>();

        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerVehicleScript>();
        rotMargin = Quaternion.Euler(0, 90, 0);
    }

    // Update is called once per frame
    void Update()
    {
        TPHack();

    }


    private void TPHack()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            VechicleTP(0);
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            VechicleTP(1);
        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            VechicleTP(2);
        }
        else if (Input.GetKeyDown(KeyCode.F4))
        {
            VechicleTP(3);
        }
        else if (Input.GetKeyDown(KeyCode.F5))
        {
            VechicleTP(4);
        }
        else if (Input.GetKeyDown(KeyCode.F6))
        {
            VechicleTP(5);
        }
        else if (Input.GetKeyDown(KeyCode.F7))
        {
            VechicleTP(6);
        }
        else if (Input.GetKeyDown(KeyCode.F8))
        {
            VechicleTP(7);
        }
        else if (Input.GetKeyDown(KeyCode.F9))
        {
            VechicleTP(8);
        }
        else if (Input.GetKeyDown(KeyCode.F10))
        {
            VechicleTP(9);
        }
        else if (Input.GetKeyDown(KeyCode.F11))
        {
            VechicleTP(10);
        }
        else if (Input.GetKeyDown(KeyCode.F12))
        {
            VechicleTP(tpTransforms.Length - 1);
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            sceneLoader.ChangeScene("Menu");
        }

    }

    private void VechicleTP(int _idx)
    {
        if (_idx < tpTransforms.Length)
        {
            playerScript.transform.position = tpTransforms[_idx].position;
            playerScript.transform.rotation = tpTransforms[_idx].rotation * rotMargin;
            playerScript.vehicleRB.velocity = Vector3.zero;
        }
    }

}
