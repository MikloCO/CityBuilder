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
    private GridStructure grid;
    private int cellSize = 3;
    private bool buildingModeActive = false;

    // Start is called before the first frame update
    void Start()
    {
        cameraMovement.SetCameralimits(0, width, 0, length);
        inputManager = FindObjectsOfType<MonoBehaviour>().OfType<IInputManager>().FirstOrDefault();
        grid = new GridStructure(cellSize, width, length);
        inputManager.AddListenerOnPointerDownEvent(HandleInput);
        inputManager.AddListenerOnPointerSecondDownEvent(HandleInputCameraPan);
        inputManager.AddListenerOnPointerSecondUpEvent(HandleInputCameraPanStop);
        uiController.AddListenerOnBuildAreaEvent(StartPlacementMode);
        uiController.AddListenerOnCancelActionEvent(CancelAction);
    }

    private void HandleInputCameraPanStop()
    {
        cameraMovement.StopCameraMovement();
    }

    private void HandleInputCameraPan(Vector3 position)
    {
        if(!buildingModeActive)
        {
            cameraMovement.MoveCamera(position);
        }
    }

    private void HandleInput(Vector3 position)
    {
        Vector3 gridPosition = grid.CalculateGridPosition(position);
      
        if (buildingModeActive && grid.bIsCellTaken(gridPosition) == false)
        {
            placementManager.CreateBuilding(gridPosition, grid);
        }

    }

    private void StartPlacementMode()
    {
        buildingModeActive = true;
    }

    private void CancelAction()
    {
        buildingModeActive = false;
    }
}
