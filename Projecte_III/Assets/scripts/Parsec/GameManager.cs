using UnityEngine;
using UnityEngine.UI;
using ParsecGaming;

public class GameManager : MonoBehaviour
{
    public PlayerManager[] m_Players;
    public GameObject PanelAuthentication;
    public GameObject PanelParsecControl;
    public InputField VerificationUri;
    public InputField UserCode;
    public InputField ShortLinkUri;
    public Text StatusField;
    public Toggle IsPublicGame;
    [SerializeField] ParsecStreamFull streamer;
    [SerializeField] Transform changeColorManager;
    [SerializeField] InactiveScreensManager inactiveScreensManager;
    [SerializeField] DoneButtonManager doneManager;
    private ParsecUnity.API.SessionResultDataData authdata;
    PlayersManager playersManager;

    void Awake()
    {
        SpawnPlayer(1, new Parsec.ParsecGuest());
        if (streamer != null)
        {
            streamer.GuestConnected += Streamer_GuestConnected;
            streamer.GuestDisconnected += Streamer_GuestDisconnected;
        }
    }

    private void Start()
    {
        playersManager = GameObject.Find("PlayersManager").GetComponent<PlayersManager>();
    }

    int GetFreePlayer()
    {
        if (m_Players == null) return 0;
        for (int i = 1; i < m_Players.Length; i++)
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

    private void Streamer_GuestConnected(object sender, Parsec.ParsecGuest guest)
    {
        int iPlayer = GetFreePlayer();
        if (iPlayer == 0) return;
        SpawnPlayer(iPlayer, guest);
    }

    public void SpawnPlayer(int player, Parsec.ParsecGuest guest)
    {
        if (m_Players == null) return;
        if (player >= 1 && player <= m_Players.Length)
        {
            m_Players[player - 1] = new GameObject("PLAYER").AddComponent<PlayerManager>();
            m_Players[player - 1].changeColorManager = changeColorManager;
            m_Players[player - 1].inactiveScreensManager = inactiveScreensManager;
            m_Players[player - 1].m_PlayerNumber = player;
            m_Players[player - 1].m_AssignedGuest = guest;
            m_Players[player - 1].doneManager = doneManager;
            m_Players[player - 1].Setup();
        }
    }

    public void GetAccessCode()
    {
        //Replace the Game ID with your own.
        ParsecUnity.API.SessionData sessionData = streamer.RequestCodeAndPoll();
        if ((sessionData != null) && (sessionData.data != null))
        {
            VerificationUri.text = sessionData.data.verification_uri;
            UserCode.text = sessionData.data.user_code;
            StatusField.text = "Waiting for User";
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
                StatusField.text = "Waiting for User";
                break;
            case ParsecUnity.API.SessionResultEnum.CodeApproved:
                StatusField.text = "Code Approved";
                PanelAuthentication.gameObject.SetActive(false);
                authdata = data;
                PanelParsecControl.gameObject.SetActive(true);
                break;
            case ParsecUnity.API.SessionResultEnum.CodeInvallidExpiredDenied:
                StatusField.text = "Code Expired";
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
        streamer.StartParsec(m_Players.Length, IsPublicGame.isOn, "Unity Test", "An Example Unity Project", authdata.id);
        ShortLinkUri.text = streamer.GetInviteUrl(authdata);
    }

    public void StopParsec()
    {
        streamer.StopParsec();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
