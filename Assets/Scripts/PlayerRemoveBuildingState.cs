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
        this.gameManager.TransistionToState(this.gameManager.selectionState, null);
    }

    public override void OnInputPanChange(Vector3 position)
    {
        return;
    }

    public override void OnInputPanUp()
    {
        return;
    }

    public override void OnInputPointerChange(Vector3 position)
    {
        // throw new System.NotImplementedException();
        //  this.buidlingManager.PrepareStructureForPlacement(this.gameManager.selectionState);
        //    this.buidlingManager.RemoveBuildingAt(position);
        return;
    }

    public override void OnInputPointerDown(Vector3 position)
    {
        Debug.Log("Remove");
        this.buidlingManager.RemoveBuildingAt(position);
    }

    public override void OnInputPointerUp()
    {
        return;
    }
}
