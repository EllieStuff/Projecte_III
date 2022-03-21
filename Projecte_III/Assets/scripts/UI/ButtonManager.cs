using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    const int MODIFIERS_IDX = 1;

    [SerializeField] private Button[] mainButtons;
    [SerializeField] private Transform[] subButtonsParents;
    [SerializeField] private GameObject[] backgrounds;
    [SerializeField] private Color selectedMainButtonColor = Color.red;
    [SerializeField] private Color selectedSubButtonColor = Color.red;
    [SerializeField] internal int playerId;

    PlayerMenuInputsPressed playerMenuInputs;
    List<List<Button>> subButtons = new List<List<Button>>();
    List<int> subButtons_Idx = new List<int>();
    List<int> lastSelectedSubButtons_Idx = new List<int>();
    internal int mainIdx = 0;
    int lastSelectedMainIdx = 0;

    public enum MenuState { MAIN, SUB, EDIT_MODIFIERS, DONE };
    public MenuState menuState = MenuState.MAIN;


    private void Start()
    {
        playerMenuInputs = GetComponent<PlayerMenuInputsPressed>();

        InitSubButton(0);
        InitSubButton(1);
        InitSubButton(2);


        //mainButtons[mainIdx].Select();
        SelectButton(mainButtons[mainIdx]);

    }

    private void Update()
    {
        if (playerMenuInputs.Inited())
        {
            Debug.Log("Idx " + mainIdx);
            if (playerMenuInputs.MenuUpPressed)
            {
                if (menuState == MenuState.MAIN)
                {
                    mainIdx--;
                    if (mainIdx < 0) 
                        mainIdx = mainButtons.Length - 1;
                    //mainButtons[mainIdx].Select();
                    SelectButton(mainButtons[mainIdx]);
                }
                else if(menuState == MenuState.SUB)
                {
                    subButtons_Idx[mainIdx]--;
                    if (subButtons_Idx[mainIdx] < 0) 
                        subButtons_Idx[mainIdx] = subButtons[mainIdx].Count - 1;
                    //subButtons[mainIdx][subButtons_Idx[mainIdx]].Select();
                    SelectButton(subButtons[mainIdx][subButtons_Idx[mainIdx]]);
                    if (mainIdx != MODIFIERS_IDX)
                        subButtons[mainIdx][subButtons_Idx[mainIdx]].onClick.Invoke();
                }
            }
            if (playerMenuInputs.MenuDownPressed)
            {
                if (menuState == MenuState.MAIN)
                {
                    mainIdx++;
                    if (mainIdx >= mainButtons.Length) 
                        mainIdx = 0;
                    //mainButtons[mainIdx].Select();
                    SelectButton(mainButtons[mainIdx]);
                }
                else if (menuState == MenuState.SUB)
                {
                    subButtons_Idx[mainIdx]++;
                    if (subButtons_Idx[mainIdx] >= subButtons[mainIdx].Count) 
                        subButtons_Idx[mainIdx] = 0;
                    //subButtons[mainIdx][subButtons_Idx[mainIdx]].Select();
                    SelectButton(subButtons[mainIdx][subButtons_Idx[mainIdx]]);
                    if (mainIdx != MODIFIERS_IDX)
                        subButtons[mainIdx][subButtons_Idx[mainIdx]].onClick.Invoke();
                }
            }
            if (playerMenuInputs.MenuAcceptPressed)
            {
                int subIdx = subButtons_Idx[mainIdx];
                if (menuState == MenuState.MAIN)
                {
                    mainButtons[mainIdx].onClick.Invoke();
                    //menuState = MenuState.SUB;
                    //subButtons[mainIdx][subIdx].Select();
                    SelectButton(subButtons[mainIdx][subIdx]);
                }
                else if (menuState == MenuState.SUB)
                {
                    if (mainIdx == MODIFIERS_IDX) //Checks if wants to edit modifiers
                    {
                        menuState = MenuState.EDIT_MODIFIERS;
                        subButtons[mainIdx][subIdx].onClick.Invoke();
                    }
                }
            }
            if (playerMenuInputs.MenuDeclinePressed)
            {
                if(menuState == MenuState.SUB)
                {
                    //menuState = MenuState.MAIN;
                    mainButtons[mainIdx].onClick.Invoke();
                    //mainButtons[mainIdx].Select();
                    SelectButton(mainButtons[mainIdx]);
                }
                else if (menuState == MenuState.EDIT_MODIFIERS)
                {
                    menuState = MenuState.SUB;
                    int subIdx = subButtons_Idx[mainIdx];
                    subButtons[mainIdx][subIdx].onClick.Invoke();
                    //subButtons[mainIdx][subIdx].Select();
                    SelectButton(subButtons[mainIdx][subIdx]);
                }
            }

        }

    }

    void InitSubButton(int _idx)
    {
        subButtons.Add(new List<Button>());
        for (int i = 0; i < subButtonsParents[_idx].childCount; i++)
        {
            subButtons[_idx].Add(subButtonsParents[_idx].GetChild(i).GetComponent<Button>());
        }
        subButtons_Idx.Add(0);
        lastSelectedSubButtons_Idx.Add(0);
    }

    public void SelectButton(Button _buttonSelected)
    {
        if (menuState == MenuState.MAIN)
        {
            mainButtons[lastSelectedMainIdx].GetComponent<Image>().color = Color.white;
            lastSelectedMainIdx = mainIdx;
            _buttonSelected.GetComponentInChildren<Image>().color = selectedMainButtonColor;
        }
        else if (menuState == MenuState.SUB)
        {
            subButtons[lastSelectedMainIdx][lastSelectedSubButtons_Idx[lastSelectedMainIdx]].GetComponent<Image>().color = Color.white;
            lastSelectedSubButtons_Idx[lastSelectedMainIdx] = subButtons_Idx[mainIdx];
            _buttonSelected.GetComponentInChildren<Image>().color = selectedSubButtonColor;
        }


    }


    public void OpenButtons()
    {
        foreach (Button item in mainButtons)
        {
            Vector3 localPos = item.transform.localPosition;
            if (localPos.x != -130)
            {
                localPos.x = -130;
                item.transform.localPosition = localPos;
            }
            item.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            item.GetComponent<ButtonScript>().SetListActive(false);
        }

        if (!backgrounds[1].activeSelf)
        {
            backgrounds[0].SetActive(false);
            backgrounds[1].SetActive(true);
        }
    }

    public void CloseButtons()
    {
        foreach (Button item in mainButtons)
        {
            Vector3 localPos = item.transform.localPosition;
            if (localPos.x != 0)
            {
                localPos.x = 0;
                item.transform.localPosition = localPos;
            }
            item.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }

        if (!backgrounds[0].activeSelf)
        {
            backgrounds[0].SetActive(true);
            backgrounds[1].SetActive(false);
        }
    }
}
