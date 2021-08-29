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

    //public override void OnInputPanChange(Vector3 position)
    //{
    //    return;
    //}

    //public override void OnInputPanUp()
    //{
    //    return;
    //}

    public override void OnConfirmAction()
    {
        this.buildingManager.ConfirmPlacement();
        base.OnConfirmAction();
    }


    public override void OnInputPointerChange(Vector3 position)
    {
        return;
    }

    public override void OnInputPointerDown(Vector3 position)
    {
        buildingManager.PrepareStructureForPlacement(position, structureName, StructureType.SingleStructure);
    }

    public override void OnInputPointerUp()
    {
        return;
    }

    public override void OnBuildArea(string structureName)
    {
        this.buildingManager.CancelPlacement();
        base.OnBuildArea(structureName);
    }

    public override void OnBuildRoad(string structureName)
    {
        this.buildingManager.CancelPlacement();
        base.OnBuildRoad(structureName);
    }

    public override void OnCancel()
    {
        this.buildingManager.CancelPlacement();
        this.gameManager.TransistionToState(this.gameManager.selectionState, null);
    }

    public override void EnterState(string structureName)
    {
        base.EnterState(structureName);
        this.structureName = structureName;
    }
}
