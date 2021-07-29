using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager
{
    GridStructure grid;
    PlacementManager placementManger;

    public BuildingManager(int cellSize, int width, int length, PlacementManager placementManger)
    {
        this.grid = new GridStructure(cellSize, width, length);
        this.placementManger = placementManger;
    }

    public void PlaceStructureAt(Vector3 inputPosition)
    {
        Vector3 gridPosition = grid.CalculateGridPosition(inputPosition);
        if(!grid.bIsCellTaken(gridPosition))
        {
            placementManger.CreateBuilding(gridPosition, grid);
        }
    }

    public void RemoveBuildingAt(Vector3 inputPosition)
    {
        Vector3 gridPosition = grid.CalculateGridPosition(inputPosition);
        if (!grid.bIsCellTaken(gridPosition))
        {
            placementManger.RemoveBuilding(gridPosition, grid);
        }
    }
}
