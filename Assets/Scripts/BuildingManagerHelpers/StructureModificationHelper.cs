using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StructureModificationHelper
{
    protected Dictionary<Vector3Int, GameObject> structureToBemodified = new Dictionary<Vector3Int, GameObject>();
    protected readonly GridStructure grid;
    protected readonly IPlacementManager placementManger;
    protected readonly StructureRepository structureRepository;

    public StructureModificationHelper(StructureRepository structureRepository, GridStructure grid, IPlacementManager placementManger)
    {
        this.structureRepository = structureRepository;
        this.grid = grid;
        this.placementManger = placementManger;
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


    public abstract void CancelModification();
    public abstract void ConfirmModification();
    public abstract void PrepareStructureForModification(Vector3 inputPosition, string structureName, StructureType structureType);


}
