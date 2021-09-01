using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleStructurePlacementHelper : StructureModificationHelper
{
    public SingleStructurePlacementHelper(StructureRepository structureRepository, GridStructure grid, IPlacementManager placementManger) : base(structureRepository, grid, placementManger)
    {
    }

    public override void PrepareStructureForModification(Vector3 inputPosition, string structureName, StructureType structureType)
    {
        GameObject buildingPrefab = this.structureRepository.GetBuildingPrefabByName(structureName, structureType);
        Vector3 gridPosition = grid.CalculateGridPosition(inputPosition);

        var gridPositionInt = Vector3Int.FloorToInt(gridPosition);
        if (grid.bIsCellTaken(gridPosition) == false)
        {
            if (structureToBemodified.ContainsKey(gridPositionInt))
            {
                RevokeStructurePlacementAt(gridPositionInt);
            }
            else
            {
                PlaceNewStructureAt(buildingPrefab, gridPosition, gridPositionInt);
            }
        }
    }

    public override void ConfirmModification()
    {
        placementManger.PlaceStructureOnTheMap(structureToBemodified.Values);

        foreach (var keyValuePair in structureToBemodified)
        {
            grid.PlaceStructureOnTheGrid(keyValuePair.Value, keyValuePair.Key);
        }
        structureToBemodified.Clear();
    }

    public override void CancelModification()
    {
        placementManger.DestroyStructures(structureToBemodified.Values);
        structureToBemodified.Clear();
    }

    private void RevokeStructurePlacementAt(Vector3Int gridPositionInt)
    {
        var structure = structureToBemodified[gridPositionInt];
        placementManger.DestroySingleStructure(structure);
        structureToBemodified.Remove(gridPositionInt);
    }

    private void PlaceNewStructureAt(GameObject buildingPrefab, Vector3 gridPosition, Vector3Int gridPositionInt)
    {
        structureToBemodified.Add(gridPositionInt, placementManger.CreateGhostStructure(gridPosition, buildingPrefab));
    }

}
