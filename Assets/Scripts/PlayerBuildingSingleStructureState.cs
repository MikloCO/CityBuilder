using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuildingSingleStructureState : PlayerState
{
    BuildingManager buildingManager;
    string structureName;

    public PlayerBuildingSingleStructureState(GameManager gameManager, 
       BuildingManager buildingManager) : base(gameManager)
    {
        this.buildingManager = buildingManager;
    }

    public override void OnConfirmAction()
    {
        this.buildingManager.ConfirmModification();
        Audiomanager.Instance.PlayPlaceBuildingSound();
        base.OnConfirmAction();
    }


    public override void OnInputPointerChange(Vector3 position)
    {
        return;
    }

    public override void OnDemolishAction()
    {
        this.buildingManager.CancelModification();
        base.OnDemolishAction();
    }

    public override void OnInputPointerDown(Vector3 position)
    {
        buildingManager.PrepareStructureForModification(position, structureName, StructureType.SingleStructure);
    }

    public override void OnInputPointerUp()
    {
        return;
    }

    public override void OnBuildArea(string structureName)
    {
        this.buildingManager.CancelModification();
        base.OnBuildArea(structureName);
    }

    public override void OnBuildRoad(string structureName)
    {
        this.buildingManager.CancelModification();
        base.OnBuildRoad(structureName);
    }

    public override void OnCancel()
    {
        this.buildingManager.CancelModification();
        this.gameManager.TransistionToState(this.gameManager.selectionState, null);
    }

    public override void EnterState(string structureName)
    {
        this.buildingManager.PrepareBuildingManager(this.GetType());
        this.structureName = structureName;
    }
}
