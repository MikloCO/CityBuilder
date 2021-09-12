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
            if (neighbourPosition.HasValue && grid.bIsCellTaken(neighbourPosition.Value))
            {
                var neighbourStructureData = grid.GetDataStructureFromTheGrid(neighbourPosition.Value);
                if (neighbourStructureData != null || CheckDictionaryForRoadAtNeighbour(neighbourPosition.Value, structureToBemodified))
                {
                    roadNeighbourStatus += (int)direction;
                }
            }
        }
        return roadNeighbourStatus;
    }

    public static bool CheckDictionaryForRoadAtNeighbour(Vector3Int value, Dictionary<Vector3Int, GameObject> structureToBemodified)
    {
        return structureToBemodified.ContainsKey(value);
    }

    internal static RoadStructureHelper CheckIfStraighRoadFits(int neighbourStatus, RoadStructureHelper roadToReturn)
    {
        throw new NotImplementedException();
    }

    internal static RoadStructureHelper CheckifCornerFits(int neighbourStatus, RoadStructureHelper roadToReturn)
    {
        throw new NotImplementedException();
    }

    internal static RoadStructureHelper CheckifWayFits(int neighbourStatus, RoadStructureHelper roadToReturn)
    {
        throw new NotImplementedException();
    }

    internal static RoadStructureHelper CheckIfFourWaysFit(int neighbourStatus, RoadStructureHelper roadToReturn)
    {
        throw new NotImplementedException();
    }
}
