using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    protected GameManager gameManager;
    public PlayerState(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public virtual void OnInputPointerDown(Vector3 position)
    {

    }

    public virtual void OnInputPointerChange(Vector3 position)
    {

    }

    public virtual void OnInputPointerUp()
    {

    }

    public virtual void OnInputPanChange(Vector3 position)
    {

    }

    public virtual void OnInputPanUp()
    {

    }

    public virtual void EnterState(string variable)
    {

    }

    public abstract void OnCancel();

    public virtual void OnBuildArea(string structureName)
    {
        this.gameManager.TransistionToState(this.gameManager.buildingAreaState, structureName);
    }

    public virtual void OnBuildSingleStructure(string structureName)
    {
        this.gameManager.TransistionToState(this.gameManager.buildingSingleStructureState, structureName);
    }

    public virtual void OnBuildRoad(string structureName)
    {
        this.gameManager.TransistionToState(this.gameManager.buidlingRoadState, structureName);
    }

    public virtual void OnDemolishAction()
    {
        this.gameManager.TransistionToState(this.gameManager.demolishState, null);
    }

}
