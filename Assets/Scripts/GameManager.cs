using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlacementManager placementManager;
    public IInputManager inputManager;
    public UIController uiController;
    public int width, length;
    public CameraMovement cameraMovement;
    private BuildingManager buildingManager;
    private int cellSize = 3;
    public LayerMask inputMask;
    private PlayerState state;
    
    public PlayerSelectionState selectionState;
    public PlayerBuildingSingleStructureState buildingSingleStructureState;
    public PlayerRemoveBuildingState demolishState;

    public PlayerState State { get => state; }

    private void Awake()
    {
        buildingManager = new BuildingManager(cellSize, width, length, placementManager);
        selectionState = new PlayerSelectionState(this, cameraMovement);
        demolishState = new PlayerRemoveBuildingState(this, buildingManager);
        buildingSingleStructureState = new PlayerBuildingSingleStructureState(this, buildingManager); 
        state = selectionState;

#if (UNITY_EDITOR && TEST) || !(UNITY_IOS || UNITY_ANDRIOD)
        inputManager = gameObject.AddComponent<InputManager>();
#endif
#if(UNITY_IOS || UNITY_ANDRIOD)

#endif
    }

    // Start is called before the first frame update
    void Start()
    {
        PrepareGameComponents();
        AssignInputListeners();
        AssignUIControllerListeners();
    }

    private void PrepareGameComponents()
    {
        inputManager.mouseInputMask = inputMask;
        cameraMovement.SetCameralimits(0, width, 0, length);
     //   inputManager = FindObjectsOfType<MonoBehaviour>().OfType<IInputManager>().FirstOrDefault();
        inputManager = FindObjectsOfType<MonoBehaviour>().OfType<IInputManager>().FirstOrDefault();

    }

    private void AssignUIControllerListeners()
    {
        uiController.AddListenerOnBuildAreaEvent(StartPlacementMode);
        uiController.AddListenerOnCancelActionEvent(CancelAction);
        uiController.AddListenerOnOnDemolishActionEvent(StartDemolishMode);
    }

    private void AssignInputListeners()
    {
        inputManager.AddListenerOnPointerSecondChangeEvent(HandleInput);
        inputManager.AddListenerOnPointerSecondDownEvent(HandleInputCameraPan);
        inputManager.AddListenerOnPointerSecondUpEvent(HandleInputCameraPanStop);
        inputManager.AddListenerOnPointerChangeEvent(HandlePointerChange);
    }

    private void StartDemolishMode()
    {
        TransistionToState(demolishState);
    }

    private void HandlePointerChange(Vector3 position)
    {
        state.OnInputPointerChange(position);
    }

    private void HandleInputCameraPanStop()
    {
        state.OnInputPanUp();
    }

    private void HandleInputCameraPan(Vector3 position)
    {
        state.OnInputPanChange(position);
    }

    private void HandleInput(Vector3 position)
    {
        state.OnInputPointerDown(position);
    }

    private void StartPlacementMode()
    {
        TransistionToState(buildingSingleStructureState);
    }

    private void CancelAction()
    {
        //  state.OnCancel();
        state.OnCancel();
    }
    public void TransistionToState(PlayerState newState)
    {
        this.state = newState;
        this.state.EnterState();
    }
}
