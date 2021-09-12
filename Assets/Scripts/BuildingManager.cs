using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager
{
    GridStructure grid;
    IPlacementManager placementManger;
    StructureRepository structureRepository;
    StructureModificationFactory helperFactory;
    StructureModificationHelper helper;
    
    public BuildingManager(int cellSize, int width, int length, IPlacementManager placementManger, StructureRepository structureRepository)
    {
        this.grid = new GridStructure(cellSize, width, length);
        this.placementManger = placementManger;
        this.structureRepository = structureRepository;
        this.helperFactory = new StructureModificationFactory(structureRepository, grid, placementManger);
    }

    public void PrepareBuildingManager(Type classType)
    {
        helper = helperFactory.GetHelper(classType);
    }

    public void ConfirmModification()
    {
        helper.ConfirmModification();
    }

    public void CancelModification()
    {
        helper.CancelModification();
    }

    public void PrepareStructureForModification(Vector3 inputPosition, string structureName, StructureType structureType)
    {
        helper.PrepareStructureForModification(inputPosition, structureName, structureType);
    }

    public void PrepareStructureFromDemolishionAt(Vector3 inputPosition)
    {
        helper.PrepareStructureForModification(inputPosition, "", StructureType.None);
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
}