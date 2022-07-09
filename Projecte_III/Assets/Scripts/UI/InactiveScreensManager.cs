using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InactiveScreensManager : MonoBehaviour
{
    PlayersManager playersManager;
    PlayerInputs currPlayerInputs;
    DoneButtonManager doneBttnManager;
    [SerializeField] int playersInited = 0;
    [SerializeField] Transform changeColorManager;
    public int PlayersInited { get { return playersInited; } }

    public bool spawnParsecCar;
    public bool parsecInited = false;

    PlayerManager[] parsecPlayersToInit;
    bool canReceiveLocalPlayers = true;


    // Start is called before the first frame update
    void Start()
    {
        playersManager = GameObject.FindGameObjectWithTag("PlayersManager").GetComponent<PlayersManager>();
        //ToDo: que faci aixo al reiniciar l'escena
        currPlayerInputs = playersManager.GetPlayer(playersInited).GetComponent<PlayerInputs>();
        doneBttnManager = GameObject.FindGameObjectWithTag("DoneBttnManager").GetComponent<DoneButtonManager>();
        parsecPlayersToInit = GameObject.FindObjectsOfType<PlayerManager>();

        if (playersManager.gameMode == PlayersManager.GameModes.MONO)
            doneBttnManager.buttonsActive = playersInited = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (playersInited < playersManager.numOfPlayers && ((currPlayerInputs.Inited() && canReceiveLocalPlayers)/* || spawnParsecCar*/))
        {
            SetNewCar();
        }
        else if(canReceiveLocalPlayers && parsecInited)
        {
            canReceiveLocalPlayers = false;
        }

    }

    public void SetNewCar()
    {
        //Activate Change Color Script
        changeColorManager.GetChild(playersInited).GetComponent<ChangeColor>().enabled = true;
        //
        StartCoroutine(DisappearBlackScreen(playersInited));
        playersInited++;
        Transform playerTrans = playersManager.GetPlayer(playersInited);
        if (playerTrans == null)
        {
            int debug = 0;
        }
        else
        {
            currPlayerInputs = playersManager.GetPlayer(playersInited).GetComponent<PlayerInputs>();

            int debug = 0;
        }

        //spawnParsecCar = false;
    }


    IEnumerator DisappearBlackScreen(int _idx)
    {
        Image blackScreen = transform.GetChild(_idx).GetComponent<Image>();
        TextMeshProUGUI text = blackScreen.GetComponentInChildren<TextMeshProUGUI>();
        Color initScreenColor = blackScreen.color, initTextColor = text.color;

        float timer = 0, lerpTime = 0.1f;
        while (timer < lerpTime)
        {
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
            blackScreen.color = Color.Lerp(initScreenColor, Color.clear, timer / lerpTime);
            text.color = Color.Lerp(initTextColor, Color.clear, timer / lerpTime);
        }
        blackScreen.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.4f);
        doneBttnManager.buttonsActive = playersInited;
        doneBttnManager.GetButton(_idx).isActive = true;
    }
}
