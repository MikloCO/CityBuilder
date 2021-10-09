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
        this.buildingManager.CancelModification();
        this.gameManager.TransistionToState(this.gameManager.selectionState, null);
    }

    public override void OnBuildArea(string structureName)
    {
        base.OnBuildArea(structureName);
        this.buildingManager.CancelModification();
    }

    public override void OnBuildSingleStructure(string structureName)
    {
        base.OnBuildSingleStructure(structureName);
        this.buildingManager.CancelModification();
    }

    public override void OnConfirmAction()
    {
        this.buildingManager.ConfirmModification();
        Audiomanager.Instance.PlayPlaceBuildingSound();
        base.OnConfirmAction();
    }

    public override void OnDemolishAction()
    {
        this.buildingManager.CancelModification();
        base.OnDemolishAction();
    }

    public override void EnterState(string structureName)
    {
        this.buildingManager.PrepareBuildingManager(this.GetType());
        this.structureName = structureName;
    }

    public override void OnInputPointerDown(Vector3 position)
    {
 
        buildingManager.PrepareStructureForModification(position, this.structureName, StructureType.Road);
    }
}
