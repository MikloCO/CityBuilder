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
            if(structureToBemodified.ContainsKey(gridPositionInt))
            {
                RevokePlacementAt(gridPositionInt);
            }
            else
            {
                PlaceNewRoadAt(roadStructure, gridPosition, gridPositionInt);

            }
        }
    }

    private void PlaceNewRoadAt(RoadStructureHelper roadStructure, Vector3 gridPosition, Vector3Int gridPositionInt)
    {
        structureToBemodified.Add(gridPositionInt, placementManger.CreateGhostStructure(gridPosition, roadStructure.RoadPrefab, roadStructure.RoadPrefabRotation));
      //  structuresToBeModified.Add(gridPositionInt, placementManager.CreateGhostStructure(gridPosition, roadStructure.RoadPrefab, roadStructure.RoadPrefabRotation));
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

  


}
