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
            var neighbourStructureData = grid.GetDataStructureFromTheGrid(neighbourPosition.Value);
            if (neighbourStructureData != null && neighbourStructureData.GetType() == typeof(RoadStructureSO))
            {
                return true;
            }
        }
       return false;
    }
    public static bool CheckIfRoadNeighbourIsInDictionary(Vector3Int? neighbourPosition, Dictionary<Vector3Int, GameObject> structureToBeModified)
    {
        if (structureToBeModified == null)
            return false;
        return structureToBeModified.ContainsKey(neighbourPosition.Value);
    }



    public static RoadStructureHelper CheckIfStraighRoadFits(int neighbourStatus, RoadStructureHelper roadToReturn, StructureBaseSO structureData)
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
    //public GameObject prefab;
 

    public static RoadStructureHelper CheckifCornerFits(int neighbourStatus, RoadStructureHelper roadToReturn, StructureBaseSO structureData)
    {
        switch (neighbourStatus)
        {
            case ((int)Direction.Up | (int)Direction.Right):
                roadToReturn = new RoadStructureHelper(((RoadStructureSO)structureData).cornerPrefab, RotationValue.R0);
                break;
            case ((int)Direction.Down | (int)Direction.Right):
                roadToReturn = new RoadStructureHelper(((RoadStructureSO)structureData).cornerPrefab, RotationValue.R90);
                break;
            case ((int)Direction.Down | (int)Direction.Left):
                roadToReturn = new RoadStructureHelper(((RoadStructureSO)structureData).cornerPrefab, RotationValue.R180);
                break;
            case ((int)Direction.Up | (int)Direction.Left):
                roadToReturn = new RoadStructureHelper(((RoadStructureSO)structureData).cornerPrefab, RotationValue.R270);
                break;
        }
        return roadToReturn;
    }
    public static RoadStructureHelper CheckIfFourWaysFit(int neighbourStatus, RoadStructureHelper roadToReturn, StructureBaseSO structureData)
    {
        if (neighbourStatus == ((int)Direction.Up | (int)Direction.Right | (int)Direction.Down | (int)Direction.Left))
        {
            roadToReturn = new RoadStructureHelper(((RoadStructureSO)structureData).FourWayPrefab, RotationValue.R0);
        }
        return roadToReturn;
    }


    public static RoadStructureHelper CheckifThreeWayFits(int neighbourStatus, RoadStructureHelper roadToReturn, StructureBaseSO structureData)
    {
        switch (neighbourStatus)
        {
            case ((int)Direction.Up | (int)Direction.Right | (int)Direction.Down):
                roadToReturn = new RoadStructureHelper(((RoadStructureSO)structureData).threeWayPrefab, RotationValue.R0);
                break;
            case ((int)Direction.Left | (int)Direction.Up | (int)Direction.Right):
                roadToReturn = new RoadStructureHelper(((RoadStructureSO)structureData).threeWayPrefab, RotationValue.R270);
                break;
            case ((int)Direction.Down | (int)Direction.Left | (int)Direction.Up):
                roadToReturn = new RoadStructureHelper(((RoadStructureSO)structureData).threeWayPrefab, RotationValue.R180);
                break;
            case ((int)Direction.Right | (int)Direction.Down | (int)Direction.Left):
                roadToReturn = new RoadStructureHelper(((RoadStructureSO)structureData).threeWayPrefab, RotationValue.R90);
                break;
        }
        return roadToReturn;
    }

    public static void ModifyRoadCellsOnTheGrid(Dictionary<Vector3Int, GameObject> neighbourDictionary, StructureBaseSO structureData, 
        Dictionary<Vector3Int, GameObject> structureToBeModified, GridStructure grid,IPlacementManager placementManager)
    {
        foreach (var keyValuePair in neighbourDictionary)
        {
            grid.RemoveStructureFromTheGrid(keyValuePair.Key);
            placementManager.DestroySingleStructure(keyValuePair.Value);
            var roadStructure = GetCorrectRoadPrefab(keyValuePair.Key, structureData, structureToBeModified, grid);
            var structure = placementManager.PlaceStructureOnTheMap(keyValuePair.Key, roadStructure.RoadPrefab, roadStructure.RoadPrefabRotation);
            grid.PlaceStructureOnTheGrid(structure, keyValuePair.Key, GameObject.Instantiate(structureData));
        }
        neighbourDictionary.Clear();
    }

    public static RoadStructureHelper GetCorrectRoadPrefab(Vector3 gridPosition, StructureBaseSO structureData, 
        Dictionary<Vector3Int, GameObject> structureToBeModified, GridStructure grid)
    {
        var neighbourStatus = getRoadNeighbourStatus(gridPosition, grid, structureToBeModified);
        RoadStructureHelper roadToReturn = null;

        roadToReturn = CheckIfStraighRoadFits(neighbourStatus, roadToReturn, structureData);
        if (roadToReturn != null)
        {
            return roadToReturn;
        }
        roadToReturn = CheckifCornerFits(neighbourStatus, roadToReturn, structureData);
        if (roadToReturn != null)
        {
            return roadToReturn;
        }
        roadToReturn = CheckifThreeWayFits(neighbourStatus, roadToReturn, structureData);
        if (roadToReturn != null)
        {
            return roadToReturn;
        }
        roadToReturn = CheckIfFourWaysFit(neighbourStatus, roadToReturn, structureData);
        return roadToReturn;
    }


    public static Dictionary<Vector3Int, GameObject> GetRoadNeighbourForPosition(GridStructure grid, Vector3Int position)
    {
        Dictionary<Vector3Int, GameObject> dictionaryToReturn = new Dictionary<Vector3Int, GameObject>();
        List<Vector3Int?> neighbourPossibleLocations = new List<Vector3Int?>();
        neighbourPossibleLocations.Add(grid.GetPositionOfTheNeighbourIfExists(position, Direction.Up));
        neighbourPossibleLocations.Add(grid.GetPositionOfTheNeighbourIfExists(position, Direction.Down));
        neighbourPossibleLocations.Add(grid.GetPositionOfTheNeighbourIfExists(position, Direction.Left));
        neighbourPossibleLocations.Add(grid.GetPositionOfTheNeighbourIfExists(position, Direction.Right));

        foreach (var possiblePosition in neighbourPossibleLocations)
        {
            if (possiblePosition.HasValue)
            {
                if (CheckIfNeighbourIsRoadOnTheGrid(grid, possiblePosition.Value) && dictionaryToReturn.ContainsKey(possiblePosition.Value) == false)
                {
                    dictionaryToReturn.Add(possiblePosition.Value, grid.GetStructureFromGrid(possiblePosition.Value));
                }
            }
        }
        return dictionaryToReturn;
    }
}
