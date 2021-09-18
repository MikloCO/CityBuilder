using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonePlacementHelper : StructureModificationHelper
{
    Vector3 mapBottomLeftCorner;
    Vector3 startPoint;
    Vector3? previousEndPosition = null;
    bool startPositionAcquired = false;
    Queue<GameObject> gameObjectsToReuse = new Queue<GameObject>();

    public ZonePlacementHelper(StructureRepository structureRepository, GridStructure grid, IPlacementManager placementManger, Vector3 mapBottomLeftCorner) : base(structureRepository, grid, placementManger)
    {
        this.mapBottomLeftCorner = mapBottomLeftCorner;
    }

    public override void PrepareStructureForModification(Vector3 inputPosition, string structureName, StructureType structureType)
    {
        base.PrepareStructureForModification(inputPosition, structureName, structureType);
        Vector3 gridPosition = grid.CalculateGridPosition(inputPosition);
        if (startPositionAcquired == false && grid.bIsCellTaken(gridPosition) == false)
        {
            startPoint = gridPosition;
            startPositionAcquired = true;
        }
        if (startPositionAcquired && (previousEndPosition == null || ZoneCalculator.CheckIfPositionHasChanged(gridPosition, previousEndPosition.Value, grid)))
        {
            PlaceNewZoneUpToPosition(gridPosition);
        }

    }

    private void PlaceNewZoneUpToPosition(Vector3 endPoint)
    {
        Vector3Int minPoint = Vector3Int.FloorToInt(startPoint);
        Vector3Int maxPoint = Vector3Int.FloorToInt(endPoint);

        ZoneCalculator.PrepareStartAndEndPoints(startPoint, endPoint, ref minPoint, ref maxPoint, mapBottomLeftCorner);
        HashSet<Vector3Int> newPositionsSet = grid.GetAllPositionsFromTo(minPoint, maxPoint);
        previousEndPosition = endPoint;
        ZoneCalculator.CalculateZone(newPositionsSet, structureToBemodified, gameObjectsToReuse);

        foreach (var positionToPlaceStructure in newPositionsSet)
        {
            if(grid.bIsCellTaken(positionToPlaceStructure))
                continue;
            GameObject structureToAdd = null;
            if(gameObjectsToReuse.Count > 0)
            {
                var gameObjectToReuse = gameObjectsToReuse.Dequeue();
                gameObjectToReuse.SetActive(true);
                structureToAdd = placementManger.MoveStructureOnTheMap(positionToPlaceStructure, gameObjectToReuse, structureData.prefab);
            }
            else
            {
                structureToAdd = placementManger.CreateGhostStructure(positionToPlaceStructure, structureData.prefab);
            }
            structureToBemodified.Add(positionToPlaceStructure, structureToAdd);
            
        }
    }

    public override void CancelModification()
    {
        base.CancelModification();
        ResetZonePlacementHelper();
    }

    public override void ConfirmModification()
    {
        base.ConfirmModification();
        ResetZonePlacementHelper();
    }

    private void ResetZonePlacementHelper()
    {
        placementManger.DestroyStructures(gameObjectsToReuse);
        gameObjectsToReuse.Clear();
        startPositionAcquired = false;
        previousEndPosition = null;
    }

    public override void StopContinousPlacement()
    {
        startPositionAcquired = false;
        base.StopContinousPlacement();
    }
}
