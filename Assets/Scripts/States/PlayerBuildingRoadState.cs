using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuildingRoadState : PlayerState
{
    BuildingManager buildingManager;

    string structureName;
    public PlayerBuildingRoadState(GameManager gameManager, BuildingManager buildingManager) : base(gameManager)
    {
        this.buildingManager = buildingManager;

    }
    public override void OnCancel()
    {
        this.buildingManager.CancelPlacement();
        this.gameManager.TransistionToState(this.gameManager.selectionState, null);
    }

    public override void OnBuildArea(string structureName)
    {
        this.buildingManager.CancelPlacement();
        base.OnBuildArea(structureName);
    }

    public override void OnBuildSingleStructure(string structureName)
    {
        this.buildingManager.CancelPlacement();
        base.OnBuildSingleStructure(structureName);
    }

    public override void OnConfirmAction()
    {
        this.buildingManager.ConfirmPlacement();
        base.OnConfirmAction();
  
    }

    public override void EnterState(string structureName)
    {
        this.structureName = structureName;
    }

    public override void OnInputPointerDown(Vector3 position)
    {
 
        this.buildingManager.PrepareStructureForPlacement(position, this.structureName, StructureType.Road);
    }
}
