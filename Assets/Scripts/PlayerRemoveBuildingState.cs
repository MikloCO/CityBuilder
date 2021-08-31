using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRemoveBuildingState : PlayerState
{
    BuildingManager buidlingManager;
    public PlayerRemoveBuildingState(GameManager gameManager, BuildingManager buildingManager) : base(gameManager)
    {
        this.buidlingManager = buildingManager;
    }

    public override void OnCancel()
    {
        this.buidlingManager.CancelDemolishion();
        this.gameManager.TransistionToState(this.gameManager.selectionState, null);
    }

    public override void OnConfirmAction()
    {
        this.buidlingManager.ConfirmDemolishion();
        base.OnConfirmAction();
    }

    public override void OnInputPointerChange(Vector3 position)
    {
        return;
    }

    public override void OnBuildArea(string structureName)
    {
        this.buidlingManager.CancelDemolishion();
        base.OnBuildArea(structureName);
    }

    public override void OnBuildRoad(string structureName)
    {
        this.buidlingManager.CancelDemolishion();
        base.OnBuildRoad(structureName);
    }

    public override void OnBuildSingleStructure(string structureName)
    {
        this.buidlingManager.CancelDemolishion();
        base.OnBuildSingleStructure(structureName);
    }

    public override void OnInputPointerDown(Vector3 position)
    {
        this.buidlingManager.PrepareStructureFromDemolishionAt(position);
    }

    public override void OnInputPointerUp()
    {
        return;
    }
}
