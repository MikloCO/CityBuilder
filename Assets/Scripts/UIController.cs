using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private Action OnBuildAreaHandler; //delegates
    private Action OnCancleActionHandler;
    private Action OnDemolishActionHandler;

    public StructureRepository structureRepository;
    public Button buildResidentialAreaButton;
    public Button cancleActionButton;
    public GameObject cancelActionPanel;

    public GameObject buildingMenuPanel;
    public Button openBuildMenuButton;
    public Button demolishButton;

    public GameObject zonePanel;
    public GameObject facilitiesPanel;
    public GameObject roadsPanel;
    public Button closeBuildMenuBtn;

    public GameObject buildButtonPrefab;

    // Start is called before the first frame update
    void Start()
    {
        cancelActionPanel.SetActive(false);
        OnCloseMenuButtonHandler();
    //    buildResidentialAreaButton.onClick.AddListener(OnBuildAreaCallback);
        cancleActionButton.onClick.AddListener(OnCancelActionCallBack);
        openBuildMenuButton.onClick.AddListener(OnOpenBuildMenu);
        demolishButton.onClick.AddListener(OnDemolishHandler);
        closeBuildMenuBtn.onClick.AddListener(OnCloseMenuButtonHandler);
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
        CreateButtonsInPanel(zonePanel.transform, structureRepository.GetZoneNames());
        CreateButtonsInPanel(facilitiesPanel.transform, structureRepository.GetSingleStructureNames());
        CreateButtonsInPanel(roadsPanel.transform, 
            new List<string>() { 
            structureRepository.GetRoadStructureName()
            }
        );
    }

    private void CreateButtonsInPanel(Transform panelTransform, List<string> dataToShow)
    {
        if(dataToShow.Count > panelTransform.childCount)
        {
            int quantityDifference = dataToShow.Count - panelTransform.childCount;
            for(int i = 0; i < quantityDifference; i++)
            {
                Instantiate(buildButtonPrefab, panelTransform);
            }
        }

        for(int i = 0; i < panelTransform.childCount; i++)
        {
            var button = panelTransform.GetChild(i).GetComponent<Button>();
            if(button != null)
            {
                button.GetComponentInChildren<TextMeshProUGUI>().text = dataToShow[i];
                button.onClick.AddListener(OnBuildAreaCallback);
            }
        }

        //foreach(Transform child in panelTransform)
        //{
        //    var button = child.GetComponent<Button>();
        //    if(button != null)
        //    {
        //        button.onClick.RemoveAllListeners();
        //        button.onClick.AddListener(OnBuildAreaCallback);
        //    }
        //}
    }

    private void OnBuildAreaCallback()
    {
        cancelActionPanel.SetActive(true);
        OnCloseMenuButtonHandler();
        OnBuildAreaHandler?.Invoke();
    }
    private void OnCancelActionCallBack()
    {
        cancelActionPanel.SetActive(false);
        OnCancleActionHandler?.Invoke();
    }

    public void  AddListenerOnBuildAreaEvent(Action listener)
    {
        OnBuildAreaHandler += listener;
    }

    public void RemoveListenerOnBuildAreaEvent(Action listener)
    {
        OnBuildAreaHandler -= listener;
    }

    public void AddListenerOnCancelActionEvent(Action listener)
    {
        OnCancleActionHandler += listener;
    }

    public void RemoveListenerOnCancelActionEvent(Action listener)
    {
        OnCancleActionHandler -= listener;
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
