using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager
{
    GridStructure grid;
    PlacementManager placementManger;
    StructureRepository structureRepository;
    Dictionary<Vector3Int, GameObject> structureToBemodified = new Dictionary<Vector3Int, GameObject>();

    public BuildingManager(int cellSize, int width, int length, PlacementManager placementManger, StructureRepository structureRepository)
    {
        this.grid = new GridStructure(cellSize, width, length);
        this.placementManger = placementManger;
        this.structureRepository = structureRepository;
    }

    public void PrepareStructureForPlacement(Vector3 inputPosition, string structureName, StructureType structureType)
    {
        GameObject buildingPrefab = this.structureRepository.GetBuildingPrefabByName(structureName, structureType);
        Vector3 gridPosition = grid.CalculateGridPosition(inputPosition);

        var gridPositionInt = Vector3Int.FloorToInt(gridPosition);
        if (grid.bIsCellTaken(gridPosition) == false)
        {
            if(structureToBemodified.ContainsKey(gridPositionInt))
            {
                RevokeStructurePlacementAt(gridPositionInt);
            }
            else
            {
                PlaceNewStructureAt(buildingPrefab, gridPosition, gridPositionInt);
            }
        }
    }

    private void PlaceNewStructureAt(GameObject buildingPrefab, Vector3 gridPosition, Vector3Int gridPositionInt)
    {
        structureToBemodified.Add(gridPositionInt, placementManger.CreateGhostStructure(gridPosition, buildingPrefab));
    }

    private void RevokeStructurePlacementAt(Vector3Int gridPositionInt)
    {
        var structure = structureToBemodified[gridPositionInt];
        placementManger.DestroySingleStructure(structure);
        structureToBemodified.Remove(gridPositionInt);
    }

    public void ConfirmPlacement()
    {
        placementManger.PlaceStructureOnTheMap(structureToBemodified.Values);
       
        foreach (var keyValuePair in structureToBemodified)
        {
            grid.PlaceStructureOnTheGrid(keyValuePair.Value, keyValuePair.Key);
        }
        structureToBemodified.Clear();
        


    }

    public void CancelPlacement()
    {
        placementManger.DestroyStructures(structureToBemodified.Values);
        structureToBemodified.Clear();
    }

    public void PrepareStructureFromDemolishionAt(Vector3 inputPosition)
    {
        Vector3 gridPosition = grid.CalculateGridPosition(inputPosition);
        if (grid.bIsCellTaken(gridPosition))
        {
            var gridPositionInt = Vector3Int.FloorToInt(gridPosition);
            var structure = grid.GetStructureFromGrid(gridPositionInt);
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
        placementManger.SetBuildingForDemolishion(structure);
    }

    private void RevokeStructureDemolishionAt(Vector3Int gridPositionInt, GameObject structure)
    {
        placementManger.ResetBuildingMaterial(structure);
        structureToBemodified.Remove(gridPositionInt);
    }

    public void CancelDemolishion()
    {
        this.placementManger.PlaceStructureOnTheMap(structureToBemodified.Values);
        structureToBemodified.Clear();
    }

    public void ConfirmDemolishion()
    {
        foreach (var gridPosition in structureToBemodified.Keys)
        {
            grid.RemoveStructureFromTheGrid(gridPosition);
        }
        this.placementManger.DestroyStructures(structureToBemodified.Values);
        structureToBemodified.Clear();
    }
}
