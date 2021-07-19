using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private Action OnBuildAreaHandler;
    private Action OnCancleActionHandler;

    public Button buildResidentialAreaButton;
    public Button cancleActionButton;
    public GameObject cancelActionPanel;

    // Start is called before the first frame update
    void Start()
    {
        cancelActionPanel.SetActive(false);
        buildResidentialAreaButton.onClick.AddListener(OnBuildAreaCallback);
        cancleActionButton.onClick.AddListener(OnCancelActionCallBack);
    }

    private void OnBuildAreaCallback()
    {
        cancelActionPanel.SetActive(true);
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
}
