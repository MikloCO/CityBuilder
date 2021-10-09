using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager
{
    GridStructure grid;
    IPlacementManager placementManger;
    StructureRepository structureRepository;
    StructureModificationHelper helper;
    
    public BuildingManager(GridStructure grid, IPlacementManager placementManger, StructureRepository structureRepository, IResourceManager resourceManager)
    {
        this.grid = grid;
        this.placementManger = placementManger;
        this.structureRepository = structureRepository;
        StructureModificationFactory.PrepareFactory(structureRepository, grid, placementManger, resourceManager);
    }

    public void PrepareBuildingManager(Type classType)
    {
        helper = StructureModificationFactory.GetHelper(classType);
    }

    public void ConfirmModification()
    {
        helper.ConfirmModification();
    }

    public void CancelModification()
    {
        helper?.CancelModification();
    }

    public void PrepareStructureForModification(Vector3 inputPosition, string structureName, StructureType structureType)
    {
        helper.PrepareStructureForModification(inputPosition, structureName, structureType);
    }

    public void PrepareStructureFromDemolishionAt(Vector3 inputPosition)
    {
        helper.PrepareStructureForModification(inputPosition, "", StructureType.None);
    }

    public IEnumerable<StructureBaseSO> GetAllStructures()
    {
        return grid.GetAllStructures();
    }

    public void CancelDemolishion()
    {
        helper.CancelModification();
    }

    public void ConfirmDemolishion()
    {
        helper.ConfirmModification();
    }

    public GameObject CheckForStructureInGrid(Vector3 inputPosition)
    {
        Vector3 gridposition = grid.CalculateGridPosition(inputPosition);
        if(grid.bIsCellTaken(gridposition))
        {
            return grid.GetStructureFromGrid(gridposition);
        }
        return null;
    }

    public GameObject CheckForStructureInDictionary(Vector3 inputPosition)
    {
        Vector3 gridPosition = grid.CalculateGridPosition(inputPosition);
        GameObject structureToReturn = null;
        structureToReturn = helper.AccessStructureInDictionary(gridPosition);

        if(structureToReturn != null)
        {
            return structureToReturn;
        }

        structureToReturn = helper.AccessStructureInDictionary(gridPosition);
        return structureToReturn;
    }

    public void StopContinousPlacement()
    {
        helper.StopContinousPlacement();
    }

    public StructureBaseSO GetStructureDataFromPosition(Vector3 inputposition)
    {
        Vector3 gridposition = grid.CalculateGridPosition(inputposition);
        if (grid.bIsCellTaken(gridposition))
        {
            return grid.GetDataStructureFromTheGrid(inputposition);
        }
        return null;
    }
}