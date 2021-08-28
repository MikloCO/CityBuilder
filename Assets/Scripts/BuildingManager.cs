using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager
{
    GridStructure grid;
    PlacementManager placementManger;
    StructureRepository structureRepository;

    public BuildingManager(int cellSize, int width, int length, PlacementManager placementManger, StructureRepository structureRepository)
    {
        this.grid = new GridStructure(cellSize, width, length);
        this.placementManger = placementManger;
        this.structureRepository = structureRepository;
    }

    public void PlaceStructureAt(Vector3 inputPosition, string structureName, StructureType structureType)
    {
        GameObject buildingPrefab = this.structureRepository.GetBuildingPrefabByName(structureName, structureType);
        Vector3 gridPosition = grid.CalculateGridPosition(inputPosition);
        if (grid.bIsCellTaken(gridPosition) == false)
        {
           //   placementManger.CreateBuilding(gridPosition, grid);
            placementManger.CreateBuilding(gridPosition, grid, buildingPrefab);
        }
    }

    public void RemoveBuildingAt(Vector3 inputPosition)
    {
        Vector3 gridPosition = grid.CalculateGridPosition(inputPosition);
        if (grid.bIsCellTaken(gridPosition))
        {
            Debug.Log("Cell is taken!");
            placementManger.RemoveBuilding(gridPosition, grid);
        }
    }
}
