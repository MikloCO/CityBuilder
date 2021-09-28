using System;
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
        ZoneStructureSO zoneData = (ZoneStructureSO)grid.GetDataStructureFromTheGrid(gridPosition);
        if(DoeStructureRequireAnyResource(zoneData))
        {
            var structuresAroundPositions = grid.GetStructurePositionInRange(gridPosition, zoneData.maxFacilitySearchRange);
            foreach (var structurePositionNearby in structuresAroundPositions)
            {
                var data = grid.GetDataStructureFromTheGrid(structurePositionNearby);
                if (data.GetType() == typeof(SingleFacilitySO))
                {
                    SingleFacilitySO facility = (SingleFacilitySO)data;
                    if((facility.facilityType==FacilityType.Power && zoneData.HasPower()==false && zoneData.requirePower)
                    || facility.facilityType == FacilityType.Water && zoneData.HasWater() == false && zoneData.requireWater)
                    {
                        if(grid.AddPositionsInRange(gridPosition, structurePositionNearby, facility.singleStructureRange))
                        {
                            if(facility.IsFull() == false)
                            {
                                facility.AddClients(new StructureBaseSO[] { zoneData });
                                if(DoeStructureRequireAnyResource(zoneData) == false)
                                {
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    private static bool DoeStructureRequireAnyResource(ZoneStructureSO zoneData)
    {
        return (zoneData.requirePower) && zoneData.HasPower() == false || (zoneData.requireWater && zoneData.HasWater() == false);
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

    public static void CreateStructureLogic(Type structuretype, Vector3Int gridPosition, GridStructure grid)
    {
        if (structuretype == typeof(ZoneStructureSO))
        {
            PrepareZoneStructure(gridPosition, grid);
        }
        else if (structuretype == typeof(RoadStructureSO))
        {
            PrepareRoadStructure(gridPosition, grid);
        }
        else if (structuretype == typeof(SingleFacilitySO))
        {
            PrepareFacilityStructure(gridPosition, grid);
        }
    }

    public static void DemolishionStructureLogic(Type structuretype, Vector3Int gridPosition, GridStructure grid)
    {
        if (structuretype == typeof(ZoneStructureSO))
        {
            PrepareStructureForDemolishion(gridPosition, grid);
        }
        else if (structuretype == typeof(RoadStructureSO))
        {
            PrepareRoadDemolishion(gridPosition, grid);
        }
        else if (structuretype == typeof(SingleFacilitySO))
        {
            PrepareFacilityDemolishion(gridPosition, grid);
        }
    }

}
