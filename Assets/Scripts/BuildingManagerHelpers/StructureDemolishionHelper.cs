using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureDemolishionHelper : StructureModificationHelper
{
    public StructureDemolishionHelper(StructureRepository structureRepository, GridStructure grid, IPlacementManager placementManger) : base(structureRepository, grid, placementManger)
    {
    }
    public override void CancelModification()
    {
        this.placementManger.PlaceStructureOnTheMap(structureToBemodified.Values);
        structureToBemodified.Clear();
    }

    public override void ConfirmModification()
    {
        foreach (var gridPosition in structureToBemodified.Keys)
        {
            grid.RemoveStructureFromTheGrid(gridPosition);
        }
        this.placementManger.DestroyStructures(structureToBemodified.Values);
        structureToBemodified.Clear();
    }

    public override void PrepareStructureForModification(Vector3 inputPosition, string structureName, StructureType structureType)
    {
        Vector3 gridPosition = grid.CalculateGridPosition(inputPosition);
        if (grid.bIsCellTaken(gridPosition))
        {
            var gridPositionInt = Vector3Int.FloorToInt(gridPosition);
            var structure = grid.GetStructureFromGrid(gridPosition);
            if (structureToBemodified.ContainsKey(gridPositionInt))
            {
                RevokeStructureDemolishionAt(gridPositionInt, structure);
            }
            else
            {
                AddStructureForDemolishion(gridPositionInt, structure);
            }
        }
    }

    private void RevokeStructureDemolishionAt(Vector3Int gridPositionInt, GameObject structure)
    {
        placementManger.ResetBuildingLook(structure);
        structureToBemodified.Remove(gridPositionInt);
    }


    private void AddStructureForDemolishion(Vector3Int gridPositionInt, GameObject structure)
    {
        structureToBemodified.Add(gridPositionInt, structure);
        placementManger.SetBuildingForDemolition(structure);
    }
}
