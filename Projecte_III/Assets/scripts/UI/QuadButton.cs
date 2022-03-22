using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuadButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject quadModel;
    [SerializeField] private GameObject quadSpot;
    StatsSliderManager stats;
    Stats.Data sliderData;
    int playerId;

    PlayersManager playersManager;
    PlayerStatsManager playerStats;
    PlayerInputs playerInputs;

    [SerializeField] private GameObject currentQuad;
    private bool placed = false;

    private void Start()
    {
        playerId = transform.parent.parent.GetComponentInParent<ButtonManager>().playerId;

        playersManager = GameObject.FindGameObjectWithTag("PlayersManager").GetComponent<PlayersManager>();
        playerStats = playersManager.GetPlayer(playerId).GetComponent<PlayerStatsManager>();
        playerInputs = playersManager.GetPlayer(playerId).GetComponent<PlayerInputs>();

        stats = GameObject.FindGameObjectWithTag("StatsManager").GetComponent<StatsManager>().GetPlayerStats(playerId);
    }

    private void Update()
    {
        sliderData = quadModel.GetComponent<Stats>().GetStats();

        if (quadSpot == null)
        {
            quadSpot = playerStats.transform.GetChild(0).gameObject;
            currentQuad = quadSpot.transform.GetChild(0).gameObject;
        }
    }

    public void OnPointerEnter(PointerEventData data)
    {
        if (!playerInputs.UsesKeyboard()) return;

        if(currentQuad == null)
            currentQuad = quadSpot.transform.GetChild(0).gameObject;

        if (currentQuad.name.Contains(quadModel.name)) return;

        if (quadModel != null)
        {
            Instantiate(quadModel, quadSpot.transform);

            Stats.Data current = playerStats.transform.GetComponent<Stats>().GetStats() - currentQuad.GetComponent<Stats>().GetStats();
            current += quadModel.GetComponent<Stats>().GetStats();

            if (quadSpot.transform.childCount > 1)
            {
                if (quadSpot.transform.childCount > 2)
                    Destroy(quadSpot.transform.GetChild(1).gameObject);

                currentQuad.SetActive(false);
            }
            SetNewValues(current);
        }
    }

    public void OnPointerExit(PointerEventData data)
    {
        if (!playerInputs.UsesKeyboard()) return;

        if (currentQuad == null)
            currentQuad = quadSpot.transform.GetChild(0).gameObject;

        if (currentQuad.name.Contains(quadModel.name)) return;

        SetNewValues(playerStats.transform.GetComponent<Stats>().GetStats(), true);

        if (quadModel != null && quadSpot.transform.childCount > 1)
        {
            Destroy(quadSpot.transform.GetChild(1).gameObject);
        }
        else if (quadModel != null && !placed)
        {
            Destroy(quadSpot.transform.GetChild(0).gameObject);
        }

        if (!currentQuad.activeSelf)
        {
            currentQuad.SetActive(true);
        }
    }

    public void SetQuad()
    {
        if (currentQuad == null)
            currentQuad = quadSpot.transform.GetChild(0).gameObject;

        if (currentQuad.name.Contains(quadModel.name)) return;

        for (int i = 0; i < quadSpot.transform.childCount; i++)
        {
            Destroy(quadSpot.transform.GetChild(i).gameObject);
        }

        Transform clone = Instantiate(quadModel, quadSpot.transform).transform;

        playersManager.GetPlayerModifier(playerId).GetComponent<ModifierManager>().SetNewModifierSpots(clone.GetChild(clone.childCount - 1));

        playerStats.SetStats();

        //SetNewValues(playerStats.transform.GetComponent<Stats>().GetStats(), true);
        placed = true;
    }

    private void SetNewValues(Stats.Data _stats, bool placed = false)
    {
        stats.SetSliderValue(_stats, placed);
    }
}
