using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureModificationFactory 
{

    private readonly StructureModificationHelper singleStructurePlacementHelper;
    private readonly StructureModificationHelper StructureDemolishionHelper;
    private readonly RoadPlacementModificationHelper roadStructurePlacementHelper;

    public StructureModificationFactory(StructureRepository structureRepository, GridStructure grid, IPlacementManager placementManger)
    {
        singleStructurePlacementHelper = new SingleStructurePlacementHelper(structureRepository, grid, placementManger);
        StructureDemolishionHelper = new StructureDemolishionHelper(structureRepository, grid, placementManger);
        roadStructurePlacementHelper = new RoadPlacementModificationHelper(structureRepository, grid, placementManger);
    }

    public StructureModificationHelper GetHelper(Type classType)
    {
        if(classType == typeof(PlayerRemoveBuildingState))
        {
            return StructureDemolishionHelper;
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
