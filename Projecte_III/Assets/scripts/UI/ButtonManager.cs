using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] internal int playerId;
    /*    const int MODIFIERS_IDX = 1;

        [SerializeField] private Button[] mainButtons;
        [SerializeField] private Transform[] subButtonsParents;
        [SerializeField] private GameObject[] backgrounds;
        [SerializeField] private Color selectedMainButtonColor = Color.red;
        [SerializeField] private Color selectedSubButtonColor = Color.red;

        PlayerMenuInputsPressed playerMenuInputs;
        List<List<Button>> subButtons = new List<List<Button>>();
        List<int> subButtons_Idx = new List<int>();
        List<int> lastSelectedSubButtons_Idx = new List<int>();
        internal int mainIdx = 0;
        int lastSelectedMainIdx = 0;
        int modIdx = 0;
        ModifierManager modManager;
        Transform modSpotsTrans;
        Vector3 spotMargin = new Vector3(0.0f, 0.8f, 0.0f);
        Color prevSelectedButtonColor;

        public enum MenuState { MAIN, SUB, EDIT_MODIFIERS, DONE };
        public MenuState menuState = MenuState.MAIN;


        private void Start()
        {
            playerMenuInputs = GetComponent<PlayerMenuInputsPressed>();
            modManager = GameObject.FindGameObjectWithTag("PlayersManager").GetComponent<PlayersManager>().GetPlayerModifier(playerId).GetComponent<ModifierManager>();

            InitSubButton(0);
            InitSubButton(1);
            InitSubButton(2);


            //mainButtons[mainIdx].Select();
            prevSelectedButtonColor = mainButtons[mainIdx].GetComponent<Image>().color;
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
                    else if (menuState == MenuState.SUB)
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
                if (playerMenuInputs.MenuRightPressed)
                {
                    if (menuState == MenuState.EDIT_MODIFIERS)
                    {
                        //int tmpIdx = modIdx;
                        //GameObject spot;
                        //bool spotAvailable;
                        //do
                        //{
                        //    tmpIdx++;
                        //    if (tmpIdx >= modSpotsTrans.childCount) tmpIdx = 0;
                        //    spot = modSpotsTrans.GetChild(tmpIdx).gameObject;
                        //    spotAvailable = spot.activeSelf && spot.GetComponent<ModifierSpotData>().IsAvailable(modManager.GetTargetContent());
                        //} while (tmpIdx != modIdx && !spotAvailable);
                        //
                        //modIdx = tmpIdx;
                        //modManager.SetTargetPos(spot.transform.position + spotMargin);

                        modIdx++;
                        if (modIdx >= modSpotsTrans.childCount) modIdx = 0;
                        modManager.SetTargetPos(modSpotsTrans.GetChild(modIdx).transform.position + spotMargin);
                    }
                }
                if (playerMenuInputs.MenuLeftPressed)
                {
                    if (menuState == MenuState.EDIT_MODIFIERS)
                    {
                        //int tmpIdx = modIdx;
                        //GameObject spot;
                        //bool spotAvailable;
                        //do
                        //{
                        //    tmpIdx--;
                        //    if (tmpIdx < 0) tmpIdx = modSpotsTrans.childCount - 1;
                        //    spot = modSpotsTrans.GetChild(tmpIdx).gameObject;
                        //    spotAvailable = spot.activeSelf && spot.GetComponent<ModifierSpotData>().IsAvailable(modManager.GetTargetContent());
                        //} while (tmpIdx != modIdx && !spotAvailable);
                        //
                        //modIdx = tmpIdx;
                        //modManager.SetTargetPos(spot.transform.position + spotMargin);

                        modIdx--;
                        if (modIdx < 0) modIdx = modSpotsTrans.childCount - 1;
                        modManager.SetTargetPos(modSpotsTrans.GetChild(modIdx).transform.position + spotMargin);
                    }
                }
                if (playerMenuInputs.MenuAcceptPressed)
                {
                    Debug.Log("in 1");
                    int subIdx = subButtons_Idx[mainIdx];
                    if (menuState == MenuState.MAIN)
                    {
                        prevSelectedButtonColor = subButtons[mainIdx][subIdx].GetComponent<Image>().color;
                        mainButtons[mainIdx].onClick.Invoke();
                        //menuState = MenuState.SUB;
                        //subButtons[mainIdx][subIdx].Select();
                        SelectButton(subButtons[mainIdx][subIdx]);
                        Debug.Log("in 2");
                    }
                    else if (menuState == MenuState.SUB)
                    {
                        if (mainIdx == MODIFIERS_IDX) //Checks if wants to edit modifiers
                        {
                            menuState = MenuState.EDIT_MODIFIERS;
                            subButtons[mainIdx][subIdx].onClick.Invoke();
                            Debug.Log(modManager.transform.GetChild(0).GetChild(0).name);
                            modSpotsTrans = modManager.transform.GetChild(0);
                            bool found = false;
                            for (int i = 0; i < modSpotsTrans.childCount; i++)
                            {
                                GameObject spot = modSpotsTrans.GetChild(i).gameObject;
                                if (spot.activeSelf && spot.GetComponent<ModifierSpotData>().IsAvailable(modManager.GetTargetContent()))
                                {
                                    modManager.SetTargetPos(spot.transform.position + spotMargin);
                                    modIdx = i;
                                    found = true;
                                    break;
                                }
                            }
                            if (!found) 
                                modManager.SetTargetPos(modSpotsTrans.GetChild(modIdx).position + spotMargin);
                        }
                    }
                    else if (menuState == MenuState.EDIT_MODIFIERS)
                    {
                        modManager.PlaceModifierByButton(modSpotsTrans.GetChild(modIdx));
                    }
                }
                if (playerMenuInputs.MenuDeclinePressed || Input.GetKeyDown(KeyCode.Escape))
                {
                    if (menuState == MenuState.SUB)
                    {
                        //menuState = MenuState.MAIN;
                        //mainButtons[mainIdx].onClick.Invoke();
                        //mainButtons[mainIdx].Select();
                        subButtons[mainIdx][subButtons_Idx[mainIdx]].GetComponent<Image>().color = prevSelectedButtonColor;
                        mainButtons[mainIdx].GetComponent<ButtonScript>().ChangeListGeneral(mainIdx);
                        prevSelectedButtonColor = mainButtons[mainIdx].GetComponent<Image>().color;
                        SelectButton(mainButtons[mainIdx]);
                    }
                    else if (menuState == MenuState.EDIT_MODIFIERS)
                    {
                        menuState = MenuState.SUB;
                        int subIdx = subButtons_Idx[mainIdx];
                        modManager.DestroyTargetModifer();
                        //subButtons[mainIdx][subIdx].onClick.Invoke();
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

        public void SelectButton(Button _buttonSelected, bool _refreshPrevSelectedBttnColor = true)
        {
            Image bttnImage = _buttonSelected.GetComponent<Image>();
            if (menuState == MenuState.MAIN)
            {
                mainButtons[lastSelectedMainIdx].GetComponent<Image>().color = prevSelectedButtonColor;
                lastSelectedMainIdx = mainIdx;
                if (_refreshPrevSelectedBttnColor)
                    prevSelectedButtonColor = bttnImage.color;
                bttnImage.color = selectedMainButtonColor;
            }
            else if (menuState == MenuState.SUB)
            {
                subButtons[lastSelectedMainIdx][lastSelectedSubButtons_Idx[lastSelectedMainIdx]].GetComponent<Image>().color = prevSelectedButtonColor;
                lastSelectedSubButtons_Idx[lastSelectedMainIdx] = subButtons_Idx[mainIdx];
                if (_refreshPrevSelectedBttnColor)
                    prevSelectedButtonColor = bttnImage.color;
                bttnImage.color = selectedSubButtonColor;
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
    */
}
