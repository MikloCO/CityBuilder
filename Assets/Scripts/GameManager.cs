using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlacementManager placementManager;
    public StructureRepository structureRepositoryy;
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
    public PlayerBuildingRoadState buidlingRoadState;
    public PlayerBuildZoneState buildingAreaState;

    public PlayerState State { get => state; }

    private void Awake()
    {
        PrepareStates();

#if (UNITY_EDITOR && TEST) || !(UNITY_IOS || UNITY_ANDRIOD)
        inputManager = gameObject.AddComponent<InputManager>();
#endif
#if(UNITY_IOS || UNITY_ANDRIOD)

#endif
    }

    private void PrepareStates()
    {
        buildingManager = new BuildingManager(cellSize, width, length, placementManager, structureRepositoryy);
        selectionState = new PlayerSelectionState(this, cameraMovement);
        demolishState = new PlayerRemoveBuildingState(this, buildingManager);
        buildingSingleStructureState = new PlayerBuildingSingleStructureState(this, buildingManager);
        buildingAreaState = new PlayerBuildZoneState(this, buildingManager);
        buidlingRoadState = new PlayerBuildingRoadState(this, buildingManager);
        state = selectionState;
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
        uiController.AddListenerOnBuildAreaEvent((structureName)=>state.OnBuildArea(structureName)); // Access state through game manager (using lambda) that is stored on the heap instead of the stack memory.
        uiController.AddListenerOnBuildSingleStructureEvent((structureName)=>state.OnBuildSingleStructure(structureName));
        uiController.AddListenerOnBuildRoadHandlerEvent((structureName) => state.OnBuildRoad(structureName));
        uiController.AddListenerOnCancelActionEvent(()=>state.OnCancel());
        uiController.AddListenerOnOnDemolishActionEvent(()=>state.OnDemolishAction());
    }

    private void AssignInputListeners()
    {
        inputManager.AddListenerOnPointerSecondChangeEvent((position)=>state.OnInputPointerDown(position));
        inputManager.AddListenerOnPointerSecondDownEvent((position)=>state.OnInputPanChange(position));
        inputManager.AddListenerOnPointerSecondUpEvent(()=>state.OnInputPanUp());
        inputManager.AddListenerOnPointerChangeEvent((position) => state.OnInputPointerChange(position));
    }

    public void TransistionToState(PlayerState newState, string variable)
    {
        this.state = newState;
        this.state.EnterState(variable);
    }
}
