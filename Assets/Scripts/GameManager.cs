using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject placementManagerGameObject;
    private IPlacementManager placementManager;
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

    public GameObject resourceManagerGameObject;
    private IResourceManager resourceManager;

    public WorldManager worldManager;

    private void Awake()
    {


#if (UNITY_EDITOR && TEST) || !(UNITY_IOS || UNITY_ANDRIOD)
        inputManager = gameObject.AddComponent<InputManager>();
#endif
#if(UNITY_IOS || UNITY_ANDRIOD)

#endif
    }

    private void PrepareStates()
    {
        buildingManager = new BuildingManager(worldManager.Grid, placementManager, structureRepositoryy, resourceManager);
        resourceManager.PrepareResourceManager(buildingManager);
        selectionState = new PlayerSelectionState(this, buildingManager);
        demolishState = new PlayerRemoveBuildingState(this, buildingManager);
        buildingSingleStructureState = new PlayerBuildingSingleStructureState(this, buildingManager);
        buildingAreaState = new PlayerBuildZoneState(this, buildingManager);
        buidlingRoadState = new PlayerBuildingRoadState(this, buildingManager);
        state = selectionState;
    }

    // Start is called before the first frame update
    void Start()
    {
        placementManager = placementManagerGameObject.GetComponent<IPlacementManager>();
        placementManager.PreparePlacementManager(worldManager);
        resourceManager = resourceManagerGameObject.GetComponent<IResourceManager>();
        worldManager.PrepareWorld(cellSize, width, length);
        PrepareStates();
        PrepareGameComponents();
        AssignInputListeners();
        AssignUIControllerListeners();
    }

    private void PrepareGameComponents()
    {
        inputManager.mouseInputMask = inputMask;
        cameraMovement.SetCameralimits(0, width, 0, length);
        inputManager = FindObjectsOfType<MonoBehaviour>().OfType<IInputManager>().FirstOrDefault();

    }

    private void AssignUIControllerListeners()
    {
        uiController.AddListenerOnBuildAreaEvent((structureName)=>state.OnBuildArea(structureName)); // Access state through game manager (using lambda) that is stored on the heap instead of the stack memory.
        uiController.AddListenerOnBuildSingleStructureEvent((structureName)=>state.OnBuildSingleStructure(structureName));
        uiController.AddListenerOnBuildRoadHandlerEvent((structureName) => state.OnBuildRoad(structureName));
        uiController.AddListenerOnCancelActionEvent(()=>state.OnCancel());
        uiController.AddListenerOnOnDemolishActionEvent(()=>state.OnDemolishAction());
        uiController.AddListenerOnConfirmActionEvent(() => state.OnConfirmAction());
    }

    private void AssignInputListeners()
    {
        inputManager.AddListenerOnPointerSecondChangeEvent((position)=>state.OnInputPointerDown(position));
        inputManager.AddListenerOnPointerSecondDownEvent((position)=>state.OnInputPanChange(position));
        inputManager.AddListenerOnPointerSecondUpEvent(()=>state.OnInputPanUp());
        inputManager.AddListenerOnPointerChangeEvent((position) => state.OnInputPointerChange(position));
        inputManager.AddListenerOnPointerUpEvent(() => state.OnInputPointerUp());
    }

    private void StartPlacementMode(string variable)
    {
        TransistionToState(buildingSingleStructureState, variable);
    }

    public void TransistionToState(PlayerState newState, string variable)
    {
        this.state = newState;
        this.state.EnterState(variable);
    }
}
