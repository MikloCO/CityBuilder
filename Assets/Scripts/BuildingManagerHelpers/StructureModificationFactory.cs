using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StructureModificationFactory 
{

    private static StructureModificationHelper singleStructurePlacementHelper;
    private static StructureModificationHelper StructureDemolishionHelper;
    private static StructureModificationHelper roadStructurePlacementHelper;
    private static StructureModificationHelper zonePlacementHelper;

    public static void PrepareFactory(StructureRepository structureRepository, GridStructure grid, IPlacementManager placementManger)
    {
        singleStructurePlacementHelper = new SingleStructurePlacementHelper(structureRepository, grid, placementManger);
        StructureDemolishionHelper = new StructureDemolishionHelper(structureRepository, grid, placementManger);
        roadStructurePlacementHelper = new RoadPlacementModificationHelper(structureRepository, grid, placementManger);
        zonePlacementHelper = new ZonePlacementHelper(structureRepository, grid, placementManger, Vector3.zero);
    }

    public static StructureModificationHelper GetHelper(Type classType)
    {
        if(classType == typeof(PlayerRemoveBuildingState))
        {
            return StructureDemolishionHelper;
        }
        else if(classType == typeof(PlayerBuildZoneState))
        {
            return zonePlacementHelper;
        }
        else if (classType == typeof(PlayerBuildingRoadState))
        {
            return roadStructurePlacementHelper;
        }
        else
        {
           return singleStructurePlacementHelper;
        }
    }
}
