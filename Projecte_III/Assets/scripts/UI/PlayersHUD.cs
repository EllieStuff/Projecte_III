using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayersHUD : MonoBehaviour
{
    [SerializeField] Sprite[] possibleModifiers;

    [SerializeField] RandomModifierGet player;
    public int id;

    Image modifier;
    Image[] lifes;

    Transform deadLine;

    Color mainColor;

    int currentLifes;

    // Start is called before the first frame update
    void Start()
    {
        modifier = transform.GetChild(2).GetChild(0).GetComponent<Image>();

        modifier.gameObject.SetActive(false);

        player = GameObject.Find("PlayersManager").GetComponent<PlayersManager>().GetPlayer(id).GetComponentInChildren<RandomModifierGet>();
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

    public void RollModifiers()
    {
        if(!modifier.gameObject.activeSelf)
        {
            IEnumerator roll = RollModifier();
            StartCoroutine(roll);
        }
    }

    IEnumerator RollModifier()
    {
        int _modifierIdx = Random.Range(0, possibleModifiers.Length);
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
