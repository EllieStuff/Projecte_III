using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayersHUD : MonoBehaviour
{
    public struct Pair_Percentage_Modifier
    {
        public RandomModifierGet.ModifierTypes modifier;
        public int percentage;
    }

    [SerializeField] Sprite[] possibleModifiers;

    [SerializeField] RandomModifierGet player;
    public int id;

    public ModifiersRollData modifiersRollValues;

    private PlayersManager pManager;

    Image modifier;
    Image[] lifes;

    Transform deadLine;

    Color mainColor;

    int currentLifes;

    Transform camLimit;

    // Start is called before the first frame update
    void Start()
    {
        modifier = transform.GetChild(2).GetChild(0).GetComponent<Image>();

        modifier.gameObject.SetActive(false);
        pManager = GameObject.Find("PlayersManager").GetComponent<PlayersManager>();

        player = pManager.GetPlayer(id).GetComponentInChildren<RandomModifierGet>();
        currentLifes = player.transform.GetComponent<PlayerVehicleScript>().lifes;

        mainColor = UseGradientMaterials.GetColor(player.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material.name);

        modifier.transform.parent.GetComponent<Image>().color = new Color(mainColor.r, mainColor.g, mainColor.b, 1.0f);
        //Destroy(GetComponent<UseGradientMaterials>());

        lifes = new Image[transform.GetChild(1).childCount];
        for (int i = 0; i < lifes.Length; i++)
        {
            lifes[i] = transform.GetChild(1).GetChild(i).GetComponent<Image>();
            lifes[i].color = new Color(mainColor.r, mainColor.g, mainColor.b, 1.0f);
        }
        
        deadLine = transform.GetChild(3);
        deadLine.gameObject.SetActive(false);

        camLimit = transform.parent.GetComponent<PlayersHUDManager>().GetCamLimit();
    }

    public int GetLifes()
    {
        return currentLifes;
    }

    public void UpdateLifes(int _currentLifes)
    {
        if (_currentLifes == currentLifes) return;

        Color c = lifes[_currentLifes].color;
        if(lifes[_currentLifes].color.a < 1.0f)
        {
            lifes[_currentLifes].color = mainColor;
        }
        else
        {
            lifes[_currentLifes].color = new Color(0.2f, 0.2f, 0.2f, 0.7f);
            modifier.gameObject.SetActive(false);
            player.ResetModifiers();
        }
        currentLifes = _currentLifes;

        if(currentLifes <= 0.0f)
        {
            modifier.transform.parent.GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f, 0.7f);
            deadLine.gameObject.SetActive(true);
        }

    }

    //Method that activates the roll couroutine
    public void RollModifiers()
    {
        if(!modifier.gameObject.activeSelf)
        {
            AudioManager.Instance.Play_SFX("ModifierBox_SFX");
            IEnumerator roll = RollModifier();
            StartCoroutine(roll);
        }
    }

    //Roll animation and set of the final modifier ingame
    IEnumerator RollModifier()
    {
        int _modifierIdx = GetModifierRolled();
        int timesShown = 0;
        int initial;
        do
        {
            initial = Random.Range(0, possibleModifiers.Length);

        } while (initial == _modifierIdx);

        if (initial >= possibleModifiers.Length) initial -= possibleModifiers.Length;
        int currentSprite = initial + 1;
        while(timesShown < 5)
        {
            if (currentSprite >= possibleModifiers.Length) currentSprite = 0;
            if (currentSprite == initial)
                timesShown++;

            modifier.sprite = possibleModifiers[currentSprite];

            if(!modifier.gameObject.activeSelf)
                modifier.gameObject.SetActive(true);

            currentSprite++;
            yield return new WaitForSeconds(0.05f);
        }

        modifier.sprite = possibleModifiers[_modifierIdx];
        RandomModifierGet.ModifierTypes _mod = (RandomModifierGet.ModifierTypes)_modifierIdx;
        player.SetModifier(_mod);

        yield return 0;
    }

    //Calculate all percentages and returns the resulting modifier
    int GetModifierRolled()
    {
        List<Pair_Percentage_Modifier> percentages = new List<Pair_Percentage_Modifier>();
        for (int i = 0; i < (int)RandomModifierGet.ModifierTypes.COUNT; i++)
        {
            Pair_Percentage_Modifier pair = new Pair_Percentage_Modifier();
            pair.modifier = (RandomModifierGet.ModifierTypes)i;
            pair.percentage = 0;

            ModifiersRollData.ModifierData modifierType = modifiersRollValues.Modifiers[(int)pair.modifier];

            ModifiersRollData.PercentageModificators[] modifierPercentages = modifierType.modificator;

            for (int j = 0; j < modifierPercentages.Length; j++)
            {
                switch (modifierPercentages[j])
                {
                    case ModifiersRollData.PercentageModificators.LESS_LIFES:
                        pair.percentage += CalculateNumberOfLifesPercentages(modifierType.invertModificator);
                        break;
                    case ModifiersRollData.PercentageModificators.MORE_POSITION:
                        pair.percentage += CalculatePositionPercentage(modifierType.invertModificator);
                        break;
                    case ModifiersRollData.PercentageModificators.NONE:
                        pair.percentage += 100;
                        break;
                }
            }
            
            percentages.Add(pair);
        }

        return CalculateModifier(ref percentages);
    }

    //Gets a modifier after calculating all percentages
    int CalculateModifier(ref List<Pair_Percentage_Modifier> percentages)
    {
        int _modifier = -1;

        int maxPercentage = 0;

        for (int i = 0; i < percentages.Count; i++)
        {
            maxPercentage += percentages[i].percentage;
        }

        //Sort percentages from lesser to greater and if percentages are equals, the first one that is in the Modifiers enum
        percentages.Sort(CompareModifierPercentages);

        int random = Random.Range(0, maxPercentage);
        int it = 0;

        while(random > percentages[it].percentage)
        {
            random -= percentages[it].percentage;
            it++;
        }

        _modifier = (int)percentages[it].modifier;

        return _modifier;
    }

    //Calculate percentage of modifier that depend on the number of lifes
    int CalculateNumberOfLifesPercentages(bool inversed)
    {
        int[] _lifes = transform.parent.GetComponent<PlayersHUDManager>().GetPlayerLifes();

        int maxLifes = -1, minLifes = 100, playersCount = 0;

        int mulTotalLifes = 1;

        for (int i = 0; i < _lifes.Length; i++)
        {
            mulTotalLifes *= _lifes[i];
            if (i == id) continue;
            if (_lifes[i] == 0) continue;

            if (_lifes[i] > maxLifes) maxLifes = _lifes[i];
            if (_lifes[i] < minLifes) minLifes = _lifes[i];

            if (_lifes[i] == currentLifes) playersCount++;
        }

        return LifesPercentageStateMachine(maxLifes, minLifes, playersCount, mulTotalLifes, inversed);
    }

    //State machine that sets the percentage depending on the lifes of all players
    int LifesPercentageStateMachine(int _max, int _min, int _playersWithSameLifes, int _totalMultiplication, bool inversed)
    {
        if (currentLifes == _max && _max == _min) return 100;                 // 100% percentage

        if (currentLifes < _min)
        {
            if (inversed) return 0;
            return 175;                                         // 175% percentage
        }
        else if (currentLifes == _min)
        {
            if (_playersWithSameLifes == 1)
            {
                if (inversed) return 75;
                return 150;                                           // 150% percentage
            }
            else if (_playersWithSameLifes == 2)
            {
                if (inversed) return 50;
                return 125;                                      // 125% percentage
            }
        }
        else if (currentLifes > _min)
        {
            if (currentLifes > _max)
            {
                if (inversed) return 175;
                return 0;                                         // 0% percentage
            }
            else if (currentLifes == _max)
            {
                if (_totalMultiplication == 18)
                {
                    if (inversed) return 150;
                    return 0;                                      //Case 2 players with 3 lifes, 1 with 2 lifes and 1 with 1 life
                }
                if (_playersWithSameLifes == 1)
                {
                    if (inversed) return 150;
                    return 75;                                       // 75% percentage
                }
                else if (_playersWithSameLifes == 2)
                {
                    if (inversed) return 125;
                    return 50;                                  // 50% percentage
                }
            }
        }
        else
        {
            if (_playersWithSameLifes == 0)
            {
                if (inversed) return 75;
                return 125;                                  // 50% percentage
            }
            if (_playersWithSameLifes == 1) return 100;
        }

        return 0;
    }

    //Calculate percentage of modifier that depends on the position
    int CalculatePositionPercentage(bool inversed)
    {
        List<Transform> positions = new List<Transform>();

        for (int i = 0; i < pManager.numOfPlayers; i++)
        {
            positions.Add(pManager.GetPlayer(i));
        }

        //Sort positions from lesser to greater
        positions.Sort(ComparePlayerPositions);

        return PositionPercentageStateMachine(ref positions, inversed);
    }

    //State machine that sets the percentage depending on the player positions
    public int PositionPercentageStateMachine(ref List<Transform> positions, bool inversed)
    {
        int _position = 0;

        while(positions[_position].GetComponentInParent<PlayerData>().id != id)
        {
            _position++;
            if (_position >= positions.Count) return 0;
        }

        if (inversed) _position = positions.Count - _position - 1;

        if(_position == 0)
            return 0;
        else if (_position == 1)
            return 75;
        else if (_position == 2)
            return 125;
        else if (_position == 3)
            return 175;

        return 0;
    }

    //Comparison of players distance to the front camera limit plane 
    public int ComparePlayerPositions(Transform player1, Transform player2)
    {
        Plane _camLimitPlane = new Plane(-camLimit.forward, camLimit.position);

        float distanceP1 = _camLimitPlane.GetDistanceToPoint(player1.position);
        float distanceP2 = _camLimitPlane.GetDistanceToPoint(player2.position);

        return distanceP1.CompareTo(distanceP2);
    }

    //Comparison of Modifier Percentages to Sort the percentage list
    public int CompareModifierPercentages(Pair_Percentage_Modifier first, Pair_Percentage_Modifier second)
    {
        int comparison = first.percentage.CompareTo(second.percentage);

        if (comparison == 0)
            return first.modifier.CompareTo(second.modifier);
        return comparison;
    }

    public void ClearModifiers()
    {
        if(modifier.gameObject.activeSelf)
            modifier.gameObject.SetActive(false);
    }

    public void SetModifierImage(int _modIdx)
    {
        ClearModifiers();
        modifier.gameObject.SetActive(true);
        modifier.sprite = possibleModifiers[_modIdx];
    }
}
