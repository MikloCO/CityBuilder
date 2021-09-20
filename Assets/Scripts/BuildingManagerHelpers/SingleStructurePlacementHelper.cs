using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleStructurePlacementHelper : StructureModificationHelper
{
    public SingleStructurePlacementHelper(StructureRepository structureRepository, GridStructure grid, IPlacementManager placementManger, IResourceManager resourceManager) : base(structureRepository, grid, placementManger, resourceManager)
    {
    }

    public override void PrepareStructureForModification(Vector3 inputPosition, string structureName, StructureType structureType)
    {
        base.PrepareStructureForModification(inputPosition, structureName, structureType);
        GameObject buildingPrefab = structureData.prefab;
        Vector3 gridPosition = grid.CalculateGridPosition(inputPosition);
        var gridPositionInt = Vector3Int.FloorToInt(gridPosition);

        if (grid.bIsCellTaken(gridPosition) == false)
        {
            if (structureToBemodified.ContainsKey(gridPositionInt))
            {
                resourceManager.AddMoney(structureData.placementCost);
                RevokeStructurePlacementAt(gridPositionInt);
            }
            else if(resourceManager.CanIBuyIt(structureData.placementCost))
            {
                PlaceNewStructureAt(buildingPrefab, gridPosition, gridPositionInt);
                resourceManager.SpendMoney(structureData.placementCost);
            }
        }
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

    public override void CancelModification()
    {
        resourceManager.AddMoney(structureToBemodified.Count * structureData.placementCost);
        base.CancelModification();
    }

}
