﻿using UnityEngine;
using UnityEngine.UI;
using ParsecGaming;

public class GameManager : MonoBehaviour
{
    public PlayerManager[] m_Players;
    public GameObject PanelAuthentication;
    public GameObject PanelParsecControl;
    public InputField VerificationUri;
    public InputField UserCode;
    public InputField ShortLinkUrl;
    public Text StatusField;
    public Toggle IsPublicGame;
    [SerializeField] ParsecStreamFull streamer;
    [SerializeField] Transform changeColorManager;
    [SerializeField] InactiveScreensManager inactiveScreensManager;
    [SerializeField] DoneButtonManager doneManager;
    private ParsecUnity.API.SessionResultDataData authdata;
    PlayersManager playersManager;
    InactiveScreensManager inactiveScreens;
    bool roomCreated = false;
    bool adminAssigned = false;
    public Parsec.ParsecGuest currGuest;

    public bool RoomCreated { get { return roomCreated; } }

    void Awake()
    {
        playersManager = GameObject.Find("PlayersManager").GetComponent<PlayersManager>();
        inactiveScreens = GameObject.Find("UI").transform.Find("InactiveScreens").GetComponent<InactiveScreensManager>();
        //PlayerManager[] parsecPlayers = FindObjectsOfType<PlayerManager>();
        //for (int i = 0; i < parsecPlayers.Length; i++)
        //    parsecPlayers[i].inactiveScreensManager = inactiveScreens;

        bool tmpStreamerEnabled = streamer.enabled;
        streamer.enabled = true;
        currGuest = new Parsec.ParsecGuest();
        SpawnPlayer(1, currGuest);
        if (streamer != null)
        {
            streamer.GuestConnected += Streamer_GuestConnected;
            streamer.GuestDisconnected += Streamer_GuestDisconnected;
        }
        roomCreated = inactiveScreens.parsecInited = tmpStreamerEnabled;
        streamer.enabled = tmpStreamerEnabled;
        
    }

    private void Start()
    {

    }

    int GetFreePlayer()
    {
        if (m_Players == null) return 0;
        Debug.Log(inactiveScreens.PlayersInited);
        for (int i = inactiveScreens.PlayersInited; i < m_Players.Length; i++)
            if (m_Players[i] == null)
                return i + 1;
        return 0;
    }

    private void Streamer_GuestDisconnected(object sender, Parsec.ParsecGuest guest)
    {
        if (m_Players == null) return;
        for (int i = 0; i < m_Players.Length; i++)
        {
            if (m_Players[i] != null && (m_Players[i].m_AssignedGuest.id == guest.id))
            {
                m_Players[i].BreakDown();
                Destroy(m_Players[i]);
                m_Players[i] = null;
            }
        }
    }

    private void Streamer_GuestConnected(object _sender, Parsec.ParsecGuest _guest)
    {
        int iPlayer = GetFreePlayer();
        if (iPlayer == 0) return;
        SpawnPlayer(iPlayer, _guest);
    }

    public void SpawnPlayer(int _playerId, Parsec.ParsecGuest _guest/*, bool _isAdmin, bool _reinitId = true*/)
    {
        if (m_Players == null) return;
        if (_playerId >= 1 && _playerId <= m_Players.Length)
        {
            m_Players[_playerId - 1] = new GameObject("PLAYER").AddComponent<PlayerManager>();
            m_Players[_playerId - 1].changeColorManager = changeColorManager;
            m_Players[_playerId - 1].inactiveScreensManager = inactiveScreensManager;
            m_Players[_playerId - 1].m_PlayerNumber = _playerId;
            m_Players[_playerId - 1].m_AssignedGuest = _guest;
            m_Players[_playerId - 1].gameManager = this;
            m_Players[_playerId - 1].doneManager = doneManager;
            m_Players[_playerId - 1].Setup(!adminAssigned);
            if (!adminAssigned) adminAssigned = true;

            //if (_reinitId)
            //{
            //    if (_isAdmin) PlayerPrefs.SetInt("ParsecPlayerId" + (_playerId - 1).ToString(), 0);
            //    else PlayerPrefs.SetInt("ParsecPlayerId" + (_playerId - 1).ToString(), inactiveScreens.PlayersInited);
            //}
        }
    }

    public void GetAccessCode()
    {
        streamer.enabled = true;
        //Replace the Game ID with your own.
        ParsecUnity.API.SessionData sessionData = streamer.RequestCodeAndPoll();
        if ((sessionData != null) && (sessionData.data != null))
        {
            VerificationUri.text = sessionData.data.verification_uri;
            UserCode.text = sessionData.data.user_code;
            GUIUtility.systemCopyBuffer = sessionData.data.user_code;
            StatusField.text = "Waiting for Server...";
            Application.OpenURL("https://parsec.app/activate/?code=" + sessionData.data.user_code);
        }
    }

    public void AuthenticationPoll(ParsecUnity.API.SessionResultDataData data, ParsecUnity.API.SessionResultEnum status)
    {
        Debug.Log(status);
        switch (status)
        {
            case ParsecUnity.API.SessionResultEnum.PolledTooSoon:
                break;
            case ParsecUnity.API.SessionResultEnum.Pending:
                StatusField.text = "Waiting for Server...";
                break;
            case ParsecUnity.API.SessionResultEnum.CodeApproved:
                StatusField.text = "Code Approved!";
                PanelAuthentication.gameObject.SetActive(false);
                authdata = data;
                PanelParsecControl.gameObject.SetActive(true);
                break;
            case ParsecUnity.API.SessionResultEnum.CodeInvallidExpiredDenied:
                StatusField.text = "Code Expired!";
                break;
            case ParsecUnity.API.SessionResultEnum.Unknown:
                StatusField.text = "Unknown State";
                break;
            default:
                break;
        }
    }

    public void StartParsec()
    {
        if (!roomCreated)
        {
            streamer.enabled = true;
            currGuest = new Parsec.ParsecGuest();
            SpawnPlayer(1, currGuest);
            //if (streamer != null)
            //{
            //    streamer.GuestConnected += Streamer_GuestConnected;
            //    streamer.GuestDisconnected += Streamer_GuestDisconnected;
            //}

            streamer.StartParsec(m_Players.Length, IsPublicGame.isOn, "Motor Brawl", "A crazy car party game!", authdata.id);
            ShortLinkUrl.text = streamer.GetInviteUrl(authdata);
            GUIUtility.systemCopyBuffer = ShortLinkUrl.text;
            PlayerPrefs.SetString("LastShortUrl", ShortLinkUrl.text);
            roomCreated = true;
            inactiveScreens.parsecInited = true;
            PlayerPrefs.SetString("RoomCreated", "true");
        }
    }

    public void StopParsec()
    {
        if (roomCreated)
        {
            streamer.enabled = false;
            streamer.StopParsec();
            PlayerManager[] players = GameObject.FindObjectsOfType<PlayerManager>();
            for (int i = 0; i < players.Length; i++)
                Destroy(players[i].gameObject);
            roomCreated = false;
            PlayerPrefs.SetString("RoomCreated", "false");
            PlayerPrefs.SetString("LastShortUrl", "null");
        }
    }

    public void ReinitSceneWithoutParsec()
    {
        GameObject.FindGameObjectWithTag("SceneManager").GetComponent<LoadSceneManager>().ReloadScene();
    }

}
