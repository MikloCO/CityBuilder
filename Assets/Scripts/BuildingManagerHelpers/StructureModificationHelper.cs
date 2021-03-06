using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StructureModificationHelper
{
    protected Dictionary<Vector3Int, GameObject> structureToBemodified = new Dictionary<Vector3Int, GameObject>();
    protected readonly GridStructure grid;
    protected readonly IPlacementManager placementManger;
    protected readonly StructureRepository structureRepository;
    protected StructureBaseSO structureData;
    protected IResourceManager resourceManager;

    public StructureModificationHelper(StructureRepository structureRepository, GridStructure grid, IPlacementManager placementManger, IResourceManager resourceManager)
    {
        this.structureRepository = structureRepository;
        this.grid = grid;
        this.placementManger = placementManger;
        this.resourceManager = resourceManager;
        structureData = ScriptableObject.CreateInstance<NullScriptableSO>(); // Structure data always has value, and is never null. 
        
    }

    public GameObject AccessStructureInDictionary(Vector3 gridPosition)
    {
        var gridPositionInt = Vector3Int.FloorToInt(gridPosition);
        if (structureToBemodified.ContainsKey(gridPositionInt))
        {
            return structureToBemodified[gridPositionInt];
        }
        return null;
    }

    public virtual void ConfirmModification()
    {
        placementManger.PlaceStructureOnTheMap(structureToBemodified.Values);
        Type structureType = structureData.GetType();
        foreach (var keyValuePair in structureToBemodified)
        {
            grid.PlaceStructureOnTheGrid(keyValuePair.Value, keyValuePair.Key, GameObject.Instantiate(structureData));
            StructureEconomyManager.CreateStructureLogic(structureType, keyValuePair.Key, grid);
        }
        ResetHelpersData();
    }

    public virtual void CancelModification()
    {
        placementManger.DestroyStructures(structureToBemodified.Values);
        ResetHelpersData();
    }
    public virtual void PrepareStructureForModification(Vector3 inputPosition, string structureName, StructureType structureType)
    {
        if(structureData.GetType()==typeof(NullScriptableSO) && structureType != StructureType.None)
        {
            structureData = this.structureRepository.GetStructureData(structureName, structureType);
        }
    }
    private void ResetHelpersData()
    {
        structureToBemodified.Clear();
        structureData = ScriptableObject.CreateInstance<NullScriptableSO>();
    }

    public virtual void StopContinousPlacement()
    {

    }

}
