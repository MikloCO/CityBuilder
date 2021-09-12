using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadPlacementModificationHelper : StructureModificationHelper
{
    public RoadPlacementModificationHelper(StructureRepository structureRepository, GridStructure grid, IPlacementManager placementManger) : base(structureRepository, grid, placementManger)
    {

    }

    public override void PrepareStructureForModification(Vector3 inputPosition, string structureName, StructureType structureType)
    {
        base.PrepareStructureForModification(inputPosition, structureName, structureType);
        Vector3 gridPosition = grid.CalculateGridPosition(inputPosition);
        if (grid.bIsCellTaken(gridPosition))
        {
            var gridPositionInt = Vector3Int.FloorToInt(gridPosition);
            var roadStructure = GetCorrectRoadPrefab(gridPosition);
        }
    }

    private RoadStructureHelper GetCorrectRoadPrefab(Vector3 gridPosition)
    {
        var neighbourStatus = RoadManager.getRoadNeighbourStatus(gridPosition, grid, structureToBemodified);
        RoadStructureHelper roadToReturn = null;

        roadToReturn = RoadManager.CheckIfStraighRoadFits(neighbourStatus, roadToReturn);
        if(roadToReturn != null)
        {
            return roadToReturn;
        }
        roadToReturn = RoadManager.CheckifCornerFits(neighbourStatus, roadToReturn);
        if (roadToReturn != null)
        {
            return roadToReturn;
        }
        roadToReturn = RoadManager.CheckifWayFits(neighbourStatus, roadToReturn);
        if (roadToReturn != null)
        {
            return roadToReturn;
        }
        roadToReturn = RoadManager.CheckIfFourWaysFit(neighbourStatus, roadToReturn);
        return roadToReturn;
    }

  


}
