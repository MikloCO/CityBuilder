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
        this.gameManager.TransistionToState(this.gameManager.selectionState);
    }

    public override void OnInputPanChange(Vector3 position)
    {
        throw new System.NotImplementedException();
    }

    public override void OnInputPanUp()
    {
        throw new System.NotImplementedException();
    }

    public override void OnInputPointerChange(Vector3 position)
    {
        throw new System.NotImplementedException();
    }

    public override void OnInputPointerDown(Vector3 position)
    {
        this.buidlingManager.RemoveBuildingAt(position);
    }

    public override void OnInputPointerUp()
    {
        throw new System.NotImplementedException();
    }
}
