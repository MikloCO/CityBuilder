using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StructureDemolishionHelper : StructureModificationHelper
{
    Dictionary<Vector3Int, GameObject> roadToDemolish = new Dictionary<Vector3Int, GameObject>();
    public StructureDemolishionHelper(StructureRepository structureRepository, GridStructure grid, IPlacementManager placementManger, IResourceManager resourceManager) : base(structureRepository, grid, placementManger, resourceManager)
    {
    }
    public override void CancelModification()
    {
        foreach (var item in structureToBemodified)
        {
            resourceManager.AddMoney(resourceManager.DemolishionPrice);
        }
        this.placementManger.PlaceStructureOnTheMap(structureToBemodified.Values);
        structureToBemodified.Clear();
    }

    public override void ConfirmModification()
    {
        foreach (var gridPosition in structureToBemodified.Keys)
        {
            PrepareStructureForModification(gridPosition);
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

    private void PrepareStructureForModification(Vector3Int gridPosition)
    {
        var data = grid.GetDataStructureFromTheGrid(gridPosition);
        if(data != null)
        {
            if(data.GetType()==typeof(ZoneStructureSO) && ((ZoneStructureSO)data).zoneType == ZoneType.Residential)
            {
                resourceManager.ReducePopulation(1);
            }
        }
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
                resourceManager.AddMoney(resourceManager.DemolishionPrice);   
                RevokeStructureDemolishionAt(gridPositionInt, structure);
            }
            else if(resourceManager.CanIBuyIt(resourceManager.DemolishionPrice))
            {
                AddStructureForDemolishion(gridPositionInt, structure);
                resourceManager.SpendMoney(resourceManager.DemolishionPrice);
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
