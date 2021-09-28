using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private Action<string> OnBuildAreaHandler; //delegates
    private Action<string> OnBuildSingleStructureHandler;
    private Action<string> OnBuildRoadHandler;

    private Action OnCancleActionHandler;
    private Action OnConfirmActionHandler;
    private Action OnDemolishActionHandler;

    public StructureRepository structureRepository;
    public Button buildResidentialAreaButton;
    public Button cancleActionButton;
    public Button ConfirmActionButton;
    public GameObject cancelActionPanel;

    public GameObject buildingMenuPanel;
    public Button openBuildMenuButton;
    public Button demolishButton;

    public GameObject zonePanel;
    public GameObject facilitiesPanel;
    public GameObject roadsPanel;
    public Button closeBuildMenuBtn;

    public GameObject buildButtonPrefab;

    public TextMeshProUGUI moneyValue;
    public TextMeshProUGUI populationValue;

    public GameObject structureInfoPanel;


    // Start is called before the first frame update
    void Start()
    {
        cancelActionPanel.SetActive(false);
        OnCloseMenuButtonHandler();
    //    buildResidentialAreaButton.onClick.AddListener(OnBuildAreaCallback);
        cancleActionButton.onClick.AddListener(OnCancelActionCallBack);
        ConfirmActionButton.onClick.AddListener(OnConfirmActionCallback);
        openBuildMenuButton.onClick.AddListener(OnOpenBuildMenu);
        demolishButton.onClick.AddListener(OnDemolishHandler);
        closeBuildMenuBtn.onClick.AddListener(OnCloseMenuButtonHandler);
    }

    private void OnConfirmActionCallback()
    {
        cancelActionPanel.SetActive(false);
        OnConfirmActionHandler?.Invoke();
    }



    private void OnCloseMenuButtonHandler()
    {
        buildingMenuPanel.SetActive(false);
    }

    private void OnDemolishHandler()
    {
        OnDemolishActionHandler?.Invoke();
        cancelActionPanel.SetActive(true);
        OnCloseMenuButtonHandler();
    }

    private void OnOpenBuildMenu()
    {
        buildingMenuPanel.SetActive(true);
        PrepareBuildMenu();
    }

    private void PrepareBuildMenu()
    {
        CreateButtonsInPanel(zonePanel.transform, structureRepository.GetZoneNames(), OnBuildAreaCallback);
        CreateButtonsInPanel(facilitiesPanel.transform, structureRepository.GetSingleStructureNames(), OnBuildSingleStructureCallback);
        CreateButtonsInPanel(roadsPanel.transform, 
            new List<string>() { 
            structureRepository.GetRoadStructureName()
            },
            OnBuildRoadCallback
        );
    }

    public void SetPopulationValue(int population)
    {
        populationValue.text = population + "";
    }

    private void CreateButtonsInPanel(Transform panelTransform, List<string> dataToShow, Action<string> callback)
    {
        if (dataToShow.Count > panelTransform.childCount)
        {
            int quantityDifference = dataToShow.Count - panelTransform.childCount;
            for (int i = 0; i < quantityDifference; i++)
            {
                Instantiate(buildButtonPrefab, panelTransform);
            }
        }

        for (int i = 0; i < panelTransform.childCount; i++)
        {
            var button = panelTransform.GetChild(i).GetComponent<Button>();
            if (button != null)
            {
                button.GetComponentInChildren<TextMeshProUGUI>().text = dataToShow[i];
                button.onClick.AddListener(() => callback(button.GetComponentInChildren<TextMeshProUGUI>().text));//OnBuildAreaCallback(button.GetComponentInChildren<TextMeshProUGUI>().text)); // (()=> callback(button.name)

            }
        }
    }

    public void SetMoneyValue(int money)
    {
        moneyValue.text = money + "";
    }

    private void OnBuildAreaCallback(string nameOfStructure)
    {
        PrepareUIForBuilding();
        OnBuildAreaHandler?.Invoke(nameOfStructure);
    }

    private void OnBuildRoadCallback(string nameOfStructure)
    {
        PrepareUIForBuilding();
        OnBuildRoadHandler?.Invoke(nameOfStructure);
    }

    private void OnBuildSingleStructureCallback(string nameOfStructure)
    {
        PrepareUIForBuilding();
        OnBuildSingleStructureHandler?.Invoke(nameOfStructure);
    }

    private void PrepareUIForBuilding()
    {
        cancelActionPanel.SetActive(true);
        OnCloseMenuButtonHandler();
    }

    private void OnCancelActionCallBack()
    {
        cancelActionPanel.SetActive(false);
        OnCancleActionHandler?.Invoke();
    }

    public void  AddListenerOnBuildAreaEvent(Action<string> listener)
    {
        OnBuildAreaHandler += listener;
    }

    public void RemoveListenerOnBuildAreaEvent(Action<string> listener)
    {
        OnBuildAreaHandler -= listener;
    }

    public void AddListenerOnBuildSingleStructureEvent(Action<string> listener)
    {
        OnBuildSingleStructureHandler += listener;
    }

    public void RemoveListenerOnBuildSingleStructureEvent(Action<string> listener)
    {
        OnBuildSingleStructureHandler -= listener;
    }

    public void AddListenerOnBuildRoadHandlerEvent(Action<string> listener)
    {
        OnBuildRoadHandler += listener;
    }

    public void RemoveListenerOnBuildRoadHandlerEvent(Action<string> listener)
    {
        OnBuildRoadHandler -= listener;
    }

    public void AddListenerOnCancelActionEvent(Action listener)
    {
        OnCancleActionHandler += listener;
    }

    public void RemoveListenerOnCancelActionEvent(Action listener)
    {
        OnCancleActionHandler -= listener;
    }

    public void AddListenerOnConfirmActionEvent(Action listener)
    {
        OnConfirmActionHandler += listener;
    }

    public void RemoveListenerOnConfirmActionEvent(Action listener)
    {
        OnConfirmActionHandler -= listener;
    }

    public void AddListenerOnOnDemolishActionEvent(Action listener)
    {
        OnDemolishActionHandler += listener;
    }

    public void RemoveListenerOnOnDemolishActionEvent(Action listener)
    {
        OnDemolishActionHandler -= listener;
    }

    


}
