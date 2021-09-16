using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadPlacementModificationHelper : StructureModificationHelper
{
    Dictionary<Vector3Int, GameObject> existingRoadStructuresToModify = new Dictionary<Vector3Int, GameObject>();
    public RoadPlacementModificationHelper(StructureRepository structureRepository, GridStructure grid, IPlacementManager placementManger) : base(structureRepository, grid, placementManger)
    {

    }

    public override void PrepareStructureForModification(Vector3 inputPosition, string structureName, StructureType structureType)
    {
        base.PrepareStructureForModification(inputPosition, structureName, structureType);
        Vector3 gridPosition = grid.CalculateGridPosition(inputPosition);
        if (grid.bIsCellTaken(gridPosition) == false)
        {
            var gridPositionInt = Vector3Int.FloorToInt(gridPosition);
            var roadStructure = GetCorrectRoadPrefab(gridPosition);
            if (structureToBemodified.ContainsKey(gridPositionInt))
            {
                RevokePlacementAt(gridPositionInt);
            }
            else
            {
                PlaceNewRoadAt(roadStructure, gridPosition, gridPositionInt);
            }
            AdjustNeighboursIfRiadsStructures(gridPosition);
        }
    }

    private void AdjustNeighboursIfRiadsStructures(Vector3 gridPosition)
    {
        AdjustNeighbourIfRoad(gridPosition, Direction.Up);
        AdjustNeighbourIfRoad(gridPosition, Direction.Down);
        AdjustNeighbourIfRoad(gridPosition, Direction.Right);
        AdjustNeighbourIfRoad(gridPosition, Direction.Left);
    }

    private void AdjustNeighbourIfRoad(Vector3 gridPosition, Direction direction)
    {
        var neighbourGridPosition = grid.GetPositionOfTheNeighbourIfExists(gridPosition, direction);
        if(neighbourGridPosition.HasValue)
        {
            var neighbourPositionInt = neighbourGridPosition.Value;
            AdjustStructureIfIsInDictionary(neighbourGridPosition, neighbourPositionInt);
            AdjustStructureIfIsOnGrid(neighbourGridPosition, neighbourPositionInt);
        }
    }

    private void AdjustStructureIfIsOnGrid(Vector3Int? neighbourGridPosition, Vector3Int neighbourPositionInt)
    {
        if (RoadManager.CheckIfNeighbourIsRoadOnTheGrid(grid, neighbourPositionInt))
        {
            var neighboursStructureData = grid.GetDataStructureFromTheGrid(neighbourGridPosition.Value);
            if (neighboursStructureData != null && neighboursStructureData.GetType() == typeof(RoadStructureSO) && existingRoadStructuresToModify.ContainsKey(neighbourPositionInt) == false)
            {
                existingRoadStructuresToModify.Add(neighbourPositionInt, grid.GetStructureFromGrid(neighbourGridPosition.Value));
            }
        }
    }

    private void AdjustStructureIfIsInDictionary(Vector3Int? neighbourGridPosition, Vector3Int neighbourPositionInt)
    {
        if (RoadManager.CheckIfRoadNeighbourIsInDictionary(neighbourPositionInt, structureToBemodified))
        {
            RevokePlacementAt(neighbourPositionInt);
            var neighbourStructure = GetCorrectRoadPrefab(neighbourGridPosition.Value);
            PlaceNewRoadAt(neighbourStructure, neighbourGridPosition.Value, neighbourPositionInt);
        }
    }

    private void PlaceNewRoadAt(RoadStructureHelper roadStructure, Vector3 gridPosition, Vector3Int gridPositionInt)
    {
        structureToBemodified.Add(gridPositionInt, placementManger.CreateGhostStructure(gridPosition, roadStructure.RoadPrefab, roadStructure.RoadPrefabRotation));
    }

    private void RevokePlacementAt(Vector3Int gridPositionInt)
    {
        var structure = structureToBemodified[gridPositionInt];
        placementManger.DestroySingleStructure(structure);
        structureToBemodified.Remove(gridPositionInt);
    }

    private RoadStructureHelper GetCorrectRoadPrefab(Vector3 gridPosition)
    {
        var neighbourStatus = RoadManager.getRoadNeighbourStatus(gridPosition, grid, structureToBemodified);
        RoadStructureHelper roadToReturn = null;

        roadToReturn = RoadManager.CheckIfStraighRoadFits(neighbourStatus, roadToReturn, structureData);
        if(roadToReturn != null)
        {
            return roadToReturn;
        }
        roadToReturn = RoadManager.CheckifCornerFits(neighbourStatus, roadToReturn, structureData);
        if (roadToReturn != null)
        {
            return roadToReturn;
        }
        roadToReturn = RoadManager.CheckifThreeWayFits(neighbourStatus, roadToReturn, structureData);
        if (roadToReturn != null)
        {
            return roadToReturn;
        }
        roadToReturn = RoadManager.CheckIfFourWaysFit(neighbourStatus, roadToReturn, structureData);
        return roadToReturn;
    }

    public override void CancelModification()
    {
        base.CancelModification();
        existingRoadStructuresToModify.Clear();
    }

    public override void ConfirmModification()
    {
        foreach (var keyValuePair in existingRoadStructuresToModify)
        {
            grid.RemoveStructureFromTheGrid(keyValuePair.Key);
            placementManger.DestroySingleStructure(keyValuePair.Value);
            var roadStructure = GetCorrectRoadPrefab(keyValuePair.Key);
            var structure = placementManger.PlaceStructureOnTheMap(keyValuePair.Key, roadStructure.RoadPrefab, roadStructure.RoadPrefabRotation);
            grid.PlaceStructureOnTheGrid(structure, keyValuePair.Key, structureData);
        }
        existingRoadStructuresToModify.Clear();
        base.ConfirmModification();
    }


}
