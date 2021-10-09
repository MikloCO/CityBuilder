using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuildZoneState : PlayerState
{
    BuildingManager buildingManager;
    string structureName;
    public PlayerBuildZoneState(GameManager gameManager, BuildingManager buildingManager) : base(gameManager)
    {
        this.buildingManager = buildingManager;
    }

    public override void OnCancel()
    {
        this.buildingManager.CancelModification();
        this.gameManager.TransistionToState(this.gameManager.selectionState, null);
    }

    public override void OnBuildRoad(string structureName)
    {
        this.buildingManager.CancelModification();
        Audiomanager.Instance.PlayPlaceBuildingSound();
        base.OnBuildRoad(structureName);
    }

    public override void OnConfirmAction()
    {
        this.buildingManager.ConfirmModification();
        Audiomanager.Instance.PlayPlaceBuildingSound();
        base.OnConfirmAction();
    }

    public override void EnterState(string structureName)
    {
        this.buildingManager.PrepareBuildingManager(this.GetType());
        this.structureName = structureName;
    }

    public override void OnInputPointerDown(Vector3 position)
    {
        this.buildingManager.PrepareStructureForModification(position, structureName, StructureType.Zone);
    }

    public override void OnInputPointerChange(Vector3 position)
    {
        this.buildingManager.PrepareStructureForModification(position, structureName, StructureType.Zone);
    }

    public override void OnInputPointerUp()
    {
        this.buildingManager.StopContinousPlacement();
    }

    public override void OnBuildSingleStructure(string structureName)
    {
        this.buildingManager.CancelModification();
        base.OnBuildSingleStructure(structureName);
    }

    public override void OnDemolishAction()
    {
        this.buildingManager.CancelModification();
        base.OnDemolishAction();
    }
}
