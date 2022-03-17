using UnityEngine;
using UnityEngine.EventSystems;

public class WheelButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject wheelsModel;
    [SerializeField] private GameObject wheelSpot = null;
    [SerializeField] private GameObject currentWheel;

    PlayersManager playersManager;
    PlayerStatsManager playerStats;
    StatsSliderManager stats;
    int playerId;

    Stats.Data sliderData;

    private bool placed = false;

    private void Start()
    {
        playersManager = GameObject.FindGameObjectWithTag("PlayersManager").GetComponent<PlayersManager>();
        playerId = transform.parent.parent.GetComponentInParent<ButtonManager>().playerId;
        playerStats = playersManager.GetPlayer(playerId).GetComponent<PlayerStatsManager>();

        stats = GameObject.FindGameObjectWithTag("StatsManager").GetComponent<StatsManager>().GetPlayerStats(playerId);
    }

    private void Update()
    {
        sliderData = wheelsModel.GetComponent<Stats>().GetStats();

        if (wheelSpot == null)
        {
            wheelSpot = playerStats.transform.parent.GetChild(1).gameObject;
            currentWheel = wheelSpot.transform.GetChild(0).gameObject;
        }
    }

    public void OnPointerEnter(PointerEventData data)
    {
        if (currentWheel == null)
            currentWheel = wheelSpot.transform.GetChild(0).gameObject;

        if (currentWheel.name.Contains(wheelsModel.name)) return;

        if (wheelsModel != null)
        {
            Instantiate(wheelsModel, wheelSpot.transform);

            Stats.Data current = playerStats.transform.GetComponent<Stats>().GetStats() - currentWheel.GetComponent<Stats>().GetStats();
            current += wheelsModel.GetComponent<Stats>().GetStats();

            if (wheelSpot.transform.GetChild(0).childCount > 1 && (currentWheel == null || currentWheel.name != wheelsModel.name))
            {
                if (wheelSpot.transform.childCount > 2)
                    Destroy(wheelSpot.transform.GetChild(1));

                currentWheel = wheelSpot.transform.GetChild(0).gameObject;

                currentWheel.SetActive(false);
            }

            SetNewValues(current);
        }
    }

    public void OnPointerExit(PointerEventData data)
    {
        if (currentWheel == null)
            currentWheel = wheelSpot.transform.GetChild(0).gameObject;

        if (currentWheel.name.Contains(wheelsModel.name)) return;

        SetNewValues(playerStats.transform.GetComponent<Stats>().GetStats(), true);

        if (wheelsModel != null && wheelSpot.transform.childCount > 1)
        {
            Destroy(wheelSpot.transform.GetChild(1).gameObject);
        }
        else if (wheelsModel != null && !placed)
        {
            Destroy(wheelSpot.transform.GetChild(0).gameObject);
        }

        if (currentWheel != null && !currentWheel.activeSelf)
        {
            currentWheel.SetActive(true);
        }
    }

    public void SetWheels()
    {
        if (currentWheel == null)
            currentWheel = wheelSpot.transform.GetChild(0).gameObject;

        if (currentWheel.name.Contains(wheelsModel.name)) return;

        for (int i = 0; i < wheelSpot.transform.childCount; i++)
        {
            Destroy(wheelSpot.transform.GetChild(i).gameObject);
        }

        //SetNewValues();

        Transform instance = Instantiate(wheelsModel, wheelSpot.transform).transform;

        instance.GetComponent<AudioSource>().enabled = true;

        playerStats.SetStats();

        //SetNewValues(playerStats.transform.GetComponent<Stats>().GetStats(), true);

        placed = true;
    }

    private void SetNewValues(Stats.Data _stats, bool placed = false)
    {
        stats.SetSliderValue(_stats, placed);
    }
}
