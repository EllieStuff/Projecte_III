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
        currPlayerInputs = playersManager.GetPlayer(playersInited).GetComponent<PlayerInputs>();
        doneBttnManager = GameObject.FindGameObjectWithTag("DoneBttnManager").GetComponent<DoneButtonManager>();
        parsecPlayersToInit = GameObject.FindObjectsOfType<PlayerManager>();

        if (playersManager.gameMode == PlayersManager.GameModes.MONO)
            doneBttnManager.buttonsActive = playersInited = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (playersInited < playersManager.numOfPlayers && ((currPlayerInputs.Inited() && canReceiveLocalPlayers) || spawnParsecCar))
        {
            //Activate Change Color Script
            changeColorManager.GetChild(playersInited).GetComponent<ChangeColor>().enabled = true;
            //
            StartCoroutine(DisappearBlackScreen(playersInited));
            playersInited++;
            currPlayerInputs = playersManager.GetPlayer(playersInited).GetComponent<PlayerInputs>();
            spawnParsecCar = false;
        }
        else if(canReceiveLocalPlayers && parsecInited)
        {
            canReceiveLocalPlayers = false;
        }
        /*else if (playersInited < playersManager.numOfPlayers && NeedsToInitPrevParsecPlayer())
        {
            //TODO: Wtfff, porque se queda en bucle infinitooo
            int playerId = GetPrevParsecPlayerId();
            PlayerManager parsecPlayer = parsecPlayersToInit[playerId];
            parsecPlayer.InitParsecPlayer(false);

            //int parsecPlayerId = GetPrevParsecPlayerId();
            //GameManager gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            //gameManager.SpawnPlayer(parsecPlayerId, gameManager.currGuest);
            //
            changeColorManager.GetChild(playersInited).GetComponent<ChangeColor>().enabled = true;
            //
            StartCoroutine(DisappearBlackScreen(playersInited));
            playersInited++;

            //PlayerManager parsecPlayer = parsecPlayersToInit[GetPrevParsecPlayerId()];
            //parsecPlayer.InitParsecPlayer(false);

            currPlayerInputs = playersManager.GetPlayer(playersInited).GetComponent<PlayerInputs>();
        }*/

    }


    bool NeedsToInitPrevParsecPlayer()
    {
        for(int i = 0; i < parsecPlayersToInit.Length; i++)
        {
            if (parsecPlayersToInit[i].playerPos == playersInited)
                return true;
        }
        ////TODO: Posar PArsecPlayerInited com a check als playerPrefs per evitar que es confongui amb l'original
        //for (int i = 0; i < 4; i++)
        //{
        //    if (PlayerPrefs.GetInt("ParsecPlayerId" + i, -1) == playersInited && PlayerPrefs.GetInt("ParsecPlayerId" + i, -1) > 0)
        //        return true;
        //}
        return false;
    }
    int GetPrevParsecPlayerId()
    {
        for (int i = 0; i < parsecPlayersToInit.Length; i++)
        {
            if (parsecPlayersToInit[i].playerPos == playersInited)
                return i;
        }
        //for (int i = 0; i < 4; i++)
        //{
        //    if (PlayerPrefs.GetInt("ParsecPlayerId" + i, -1) == playersInited)
        //        return i;
        //}
        return -1;
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
