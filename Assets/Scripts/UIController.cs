using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private Action OnBuildAreaHandler; //delegates
    private Action OnCancleActionHandler;
    private Action OnDemolishActionHandler;

    public Button buildResidentialAreaButton;
    public Button cancleActionButton;
    public GameObject cancelActionPanel;

    public GameObject buildingMenuPanel;
    public Button openBuildMenuButton;
    public Button demolishButton;

    // Start is called before the first frame update
    void Start()
    {
        cancelActionPanel.SetActive(false);
        buildingMenuPanel.SetActive(false);
        buildResidentialAreaButton.onClick.AddListener(OnBuildAreaCallback);
        cancleActionButton.onClick.AddListener(OnCancelActionCallBack);
        openBuildMenuButton.onClick.AddListener(OnOpenBuildMenu);
        demolishButton.onClick.AddListener(OnDemolishHandler);
    }

    private void OnDemolishHandler()
    {
        OnDemolishActionHandler?.Invoke();
        cancelActionPanel.SetActive(true);
        buildingMenuPanel.SetActive(false);
    }

    private void OnOpenBuildMenu()
    {
        buildingMenuPanel.SetActive(true);
    }

    private void OnBuildAreaCallback()
    {
        cancelActionPanel.SetActive(true);
        buildingMenuPanel.SetActive(false);
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
