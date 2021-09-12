using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridStructure
{
    private int cellSize;
    Cell[,] grid;

    private int width, length;
    public GridStructure(int cellSize, int width, int length)
    {
        this.cellSize = cellSize;
        this.width = width;
        this.length = length;
        grid = new Cell[this.width,this.length];

        for(int row = 0; row < grid.GetLength(0); row++)
            for(int col = 0; col < grid.GetLength(1); col++)
            {
                grid[row, col] = new Cell();
            }
    }

    public Vector3 CalculateGridPosition(Vector3 inputPosition)
    {
        int x = Mathf.FloorToInt((float)inputPosition.x / cellSize);
        int z = Mathf.FloorToInt((float)inputPosition.z / cellSize);
        
        return new Vector3(x * cellSize, 0, z * cellSize);
    }

    public Vector2Int CalculateGridIndex(Vector3 gridPosition)
    {
        return new Vector2Int((int)(gridPosition.x / cellSize),
        (int)(gridPosition.z / cellSize));
    }

    public bool bIsCellTaken(Vector3 gridPosition)
    {
       var cellIndex = CalculateGridIndex(gridPosition);
        if (bCellIsValid(cellIndex))
            return grid[cellIndex.y, cellIndex.x].IsTaken;
        throw new IndexOutOfRangeException("No index " + cellIndex + " in grid");
    }

    public void PlaceStructureOnTheGrid(GameObject structure, Vector3 gridPosition, StructureBaseSO structureData)
    {
        var cellIndex = CalculateGridIndex(gridPosition);
        if (bCellIsValid(cellIndex))
            grid[cellIndex.y, cellIndex.x].SetConstruction(structure, structureData);
    }

    public GameObject GetStructureFromGrid(Vector3 gridPosition)
    {
        var cellIndex = CalculateGridIndex(gridPosition);
        return grid[cellIndex.y, cellIndex.x].GetStructure();
    }

    public StructureBaseSO GetDataStructureFromTheGrid(Vector3 gridPosition)
    {
        var cellIndex = CalculateGridIndex(gridPosition);
        return grid[cellIndex.y, cellIndex.x].GetStructureData();
        
    }
    public void RemoveStructureFromTheGrid(Vector3 gridPosition)
    {
        var cellIndex = CalculateGridIndex(gridPosition);
        grid[cellIndex.y, cellIndex.x].RemoveStructure();
    }


    private bool bCellIsValid(Vector2Int cellIndex)
    {
        if (cellIndex.x >= 0 && cellIndex.x < grid.GetLength(1) && cellIndex.y >= 0 && cellIndex.y < grid.GetLength(0))
            return true;
        return false;
    }

    public Vector3Int? GetPositionOfTheNeighbourIfExists(Vector3 gridPosition, Direction direction)
    {
        Vector3Int? NeighbourPosition = Vector3Int.FloorToInt(gridPosition);
        switch (direction)
        {
            case Direction.Up:
                NeighbourPosition += new Vector3Int(0, 0, cellSize);
                break;
            case Direction.Right:
                NeighbourPosition += new Vector3Int(cellSize, 0, 0);
                break;
            case Direction.Down:
                NeighbourPosition += new Vector3Int(0, 0, -cellSize);
                break;
            case Direction.Left:
                NeighbourPosition += new Vector3Int(-cellSize, 0, 0);
                break;
            default:
                break;
        }
        var index = CalculateGridIndex(NeighbourPosition.Value);
        if(bCellIsValid(index) == false)
        {
            return null;
        }
        return NeighbourPosition;
    }
}

public enum Direction
{
Up = 1,    // 0 0 0 1 (2^0 = 1)
Right = 2, // 0 0 1 0 (2^1 = 2)
Down = 4,  // 0 1 0 0 (2^2 = 4)
Left = 8   // 1 0 0 0 (2^3 = 8)
}