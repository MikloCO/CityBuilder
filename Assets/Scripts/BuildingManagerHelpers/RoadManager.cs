using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RoadManager
{
    public static int getRoadNeighbourStatus(Vector3 gridPosition, GridStructure grid, Dictionary<Vector3Int, GameObject> structureToBemodified)
    {
        int roadNeighbourStatus = 0;
        foreach (Direction direction in Enum.GetValues(typeof(Direction)))
        {
            var neighbourPosition = grid.GetPositionOfTheNeighbourIfExists(gridPosition, direction);
            if(neighbourPosition.HasValue)
            {
                if(CheckIfNeighbourIsRoadOnTheGrid(grid, neighbourPosition) || CheckIfRoadNeighbourIsInDictionary(neighbourPosition, structureToBemodified))
                {
                    roadNeighbourStatus += (int)direction;
                }
            }
        }
        return roadNeighbourStatus;
    }

    public static bool CheckIfNeighbourIsRoadOnTheGrid(GridStructure grid, Vector3Int? neighbourPosition)
    {
        if (grid.bIsCellTaken(neighbourPosition.Value))
        {
            var neighbourStructureData = grid.GetStructureFromGrid(neighbourPosition.Value);
            if (neighbourStructureData != null && neighbourStructureData.GetType() == typeof(RoadStructureSO))
            {
                return true;
            }
        }
        return false;
    }
    public static bool CheckIfRoadNeighbourIsInDictionary(Vector3Int? neighbourPosition, Dictionary<Vector3Int, GameObject> structureToBeModified)
    {
        return structureToBeModified.ContainsKey(neighbourPosition.Value);
    }
    internal static RoadStructureHelper CheckIfStraighRoadFits(int neighbourStatus, RoadStructureHelper roadToReturn, StructureBaseSO structureData)
    {
       if(neighbourStatus == ((int)Direction.Up | (int)Direction.Down) || neighbourStatus == (int)Direction.Up || neighbourStatus == (int)Direction.Down)
        {
            roadToReturn = new RoadStructureHelper(((RoadStructureSO)structureData).prefab, RotationValue.R90);
        }
       else if(neighbourStatus == ((int)Direction.Right | (int)Direction.Left) || neighbourStatus == (int)Direction.Right 
       || neighbourStatus == (int)Direction.Left || neighbourStatus == 0)
        {
            roadToReturn = new RoadStructureHelper(((RoadStructureSO)structureData).prefab, RotationValue.R0);
        }
        return roadToReturn;
    }

    internal static RoadStructureHelper CheckifCornerFits(int neighbourStatus, RoadStructureHelper roadToReturn, StructureBaseSO structureData)
    {
        if(neighbourStatus == ((int)Direction.Up | (int)Direction.Right))
        {
                roadToReturn = new RoadStructureHelper(((RoadStructureSO)structureData).cornerPrefab, RotationValue.R0);
        }
        else if(neighbourStatus == ((int)Direction.Down | (int)Direction.Right))
        {
            roadToReturn = new RoadStructureHelper(((RoadStructureSO)structureData).cornerPrefab, RotationValue.R90);
        }
        else if (neighbourStatus == ((int)Direction.Down | (int)Direction.Left))
        {
            roadToReturn = new RoadStructureHelper(((RoadStructureSO)structureData).cornerPrefab, RotationValue.R180);
        }
        else if (neighbourStatus == ((int)Direction.Up | (int)Direction.Left)) 
        {
            roadToReturn = new RoadStructureHelper(((RoadStructureSO)structureData).cornerPrefab, RotationValue.R270);
        }
        return roadToReturn;
    }

    internal static RoadStructureHelper CheckifThreeWayFits(int neighbourStatus, RoadStructureHelper roadToReturn, StructureBaseSO structureData)
    {
        if (neighbourStatus == ((int)Direction.Up | (int)Direction.Right | (int)Direction.Down))
        {
            roadToReturn = new RoadStructureHelper(((RoadStructureSO)structureData).threeWayPrefab, RotationValue.R0);
        }
        else if (neighbourStatus == ((int)Direction.Left | (int)Direction.Up | (int)Direction.Right))
        {
            roadToReturn = new RoadStructureHelper(((RoadStructureSO)structureData).threeWayPrefab, RotationValue.R270);
        }
        else if (neighbourStatus == ((int)Direction.Down | (int)Direction.Left | (int)Direction.Up))
        {
            roadToReturn = new RoadStructureHelper(((RoadStructureSO)structureData).threeWayPrefab, RotationValue.R180);
        }
        else if (neighbourStatus == ((int)Direction.Right | (int)Direction.Down | (int)Direction.Left)) // might be (int)Direction.Right instead.
        {
            roadToReturn = new RoadStructureHelper(((RoadStructureSO)structureData).threeWayPrefab, RotationValue.R90);
        }
        return roadToReturn;
    }

    internal static RoadStructureHelper CheckIfFourWaysFit(int neighbourStatus, RoadStructureHelper roadToReturn, StructureBaseSO structureData)
    {
        if (neighbourStatus == ((int)Direction.Up | (int)Direction.Right | (int)Direction.Down | (int)Direction.Left))
        {
            roadToReturn = new RoadStructureHelper(((RoadStructureSO)structureData).FourWayPrefab, RotationValue.R0);
        }
        return roadToReturn;
    }
}
