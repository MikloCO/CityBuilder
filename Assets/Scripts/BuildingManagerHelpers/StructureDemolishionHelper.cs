using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StructureDemolishionHelper : StructureModificationHelper
{
    Dictionary<Vector3Int, GameObject> roadToDemolish = new Dictionary<Vector3Int, GameObject>();
    public StructureDemolishionHelper(StructureRepository structureRepository, GridStructure grid, IPlacementManager placementManger, ResourceManager resourceManager) : base(structureRepository, grid, placementManger, resourceManager)
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

        foreach (var keyValuePair in roadToDemolish)
        {
            Dictionary<Vector3Int, GameObject> neighboursDictionary = RoadManager.GetRoadNeighbourForPosition(grid, keyValuePair.Key);
            if(neighboursDictionary.Count > 0)
            {
                var structureData = grid.GetDataStructureFromTheGrid(neighboursDictionary.Keys.First());
                RoadManager.ModifyRoadCellsOnTheGrid(neighboursDictionary, structureData, null, grid, placementManger);

            }
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



    private void AddStructureForDemolishion(Vector3Int gridPositionInt, GameObject structure)
    {
        structureToBemodified.Add(gridPositionInt, structure);
        placementManger.SetBuildingForDemolition(structure);
        if (RoadManager.CheckIfNeighbourIsRoadOnTheGrid(grid, gridPositionInt) && roadToDemolish.ContainsKey(gridPositionInt) == false)
        {
            roadToDemolish.Add(gridPositionInt, structure);
        }
    }
    private void RevokeStructureDemolishionAt(Vector3Int gridPositionInt, GameObject structure)
    {
        placementManger.ResetBuildingLook(structure);
        structureToBemodified.Remove(gridPositionInt);
        if (RoadManager.CheckIfNeighbourIsRoadOnTheGrid(grid, gridPositionInt) && roadToDemolish.ContainsKey(gridPositionInt))
        {
            roadToDemolish.Remove(gridPositionInt);
        }
    }
}
