using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StructureEconomyManager
{
    private static void PrepareNewStructure(Vector3Int gridPosition, GridStructure grid)
    {
        var structureData = grid.GetDataStructureFromTheGrid(gridPosition);
        var structuresAroundThisStructure = grid.GetStructuresDataInRange(gridPosition, structureData.structureRange);

        structureData.PrepareStructure(structuresAroundThisStructure);
    }

    public static void PrepareZoneStructure(Vector3Int gridPosition, GridStructure grid)
    {
        PrepareNewStructure(gridPosition, grid);
    }

    public static void PrepareRoadStructure(Vector3Int gridPosition, GridStructure grid)
    {
        RoadStructureSO roadData = (RoadStructureSO)grid.GetDataStructureFromTheGrid(gridPosition);
        var structureAroundRoad = grid.GetStructuresDataInRange(gridPosition, roadData.structureRange);
        roadData.PrepareRoad(structureAroundRoad);
    }

    public static void PrepareFacilityStructure(Vector3Int gridPosition, GridStructure grid)
    {
        PrepareNewStructure(gridPosition, grid);
        SingleFacilitySO facilityData = (SingleFacilitySO)grid.GetDataStructureFromTheGrid(gridPosition);
        var structuresAroundFacility = grid.GetStructuresDataInRange(gridPosition, facilityData.singleStructureRange);
        facilityData.AddClients(structuresAroundFacility);
    }

    public static IEnumerable<StructureBaseSO> PrepareFacilityDemolishion(Vector3Int gridPosition, GridStructure grid)
    {
        SingleFacilitySO facilityData = (SingleFacilitySO)grid.GetDataStructureFromTheGrid(gridPosition);
        return facilityData.PrepareForDestruction();
    }

    public static IEnumerable<StructureBaseSO> PrepareRoadDemolishion(Vector3Int gridPosition, GridStructure grid)
    {
        RoadStructureSO roadData = (RoadStructureSO)grid.GetDataStructureFromTheGrid(gridPosition);
        var structureAroundRoad = grid.GetStructuresDataInRange(gridPosition, roadData.structureRange);
        return roadData.PrepareRoadDemolishion(structureAroundRoad);
    }

    public static void PrepareStructureForDemolishion(Vector3Int gridPosition, GridStructure grid)
    {
        var structureData = grid.GetDataStructureFromTheGrid(gridPosition);
        structureData.PrepareForDestruction();
    }
       
}
