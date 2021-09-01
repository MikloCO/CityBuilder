using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRemoveBuildingState : PlayerState
{
    BuildingManager buildingManager;
    public PlayerRemoveBuildingState(GameManager gameManager, BuildingManager buildingManager) : base(gameManager)
    {
        this.buildingManager = buildingManager;
    }

    public override void OnCancel()
    {
        this.buildingManager.CancelDemolishion();
        this.gameManager.TransistionToState(this.gameManager.selectionState, null);
    }

    public override void OnConfirmAction()
    {
        this.buildingManager.ConfirmDemolishion();
        base.OnConfirmAction();
    }

    public override void OnInputPointerChange(Vector3 position)
    {
        return;
    }

    public override void OnBuildArea(string structureName)
    {
        this.buildingManager.CancelDemolishion();
        base.OnBuildArea(structureName);
    }

    public override void OnBuildRoad(string structureName)
    {
        this.buildingManager.CancelDemolishion();
        base.OnBuildRoad(structureName);
    }

    public override void OnBuildSingleStructure(string structureName)
    {
        this.buildingManager.CancelDemolishion();
        base.OnBuildSingleStructure(structureName);
    }

    public override void OnInputPointerDown(Vector3 position)
    {
        this.buildingManager.PrepareStructureFromDemolishionAt(position);
    }

    public override void OnInputPointerUp()
    {
        return;
    }

    public override void EnterState(string variable)
    {
        this.buildingManager.PrepareBuildingManager(this.GetType());
    }
}
