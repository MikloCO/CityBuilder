using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New road structure", menuName = "CityBuilding/StructureData/RoadStructure")]
public class RoadStructureSO : StructureBaseSO
{
    [Tooltip("Road facing up and right")]
    public GameObject cornerPrefab;
    [Tooltip("Road facing up, right and down")]
    public GameObject threeWayPrefab;
    public GameObject FourWayPrefab;
    public RotationValue prefabRotation = RotationValue.R0;

    public void PrepareRoad(IEnumerable<StructureBaseSO> structuresAround)
    {
        foreach (var nearbyStructure in structuresAround)
        {
            nearbyStructure.PrepareStructure(new StructureBaseSO[] { this });
        }
    }

    public IEnumerable<StructureBaseSO> PrepareRoadDemolishion(IEnumerable<StructureBaseSO> structuresAround)
    {
        List<StructureBaseSO> listToReturn = new List<StructureBaseSO>();
        foreach (var nearbyStructure in structuresAround)
        {
            if (nearbyStructure.RoadProvider == this)
            {
                nearbyStructure.RemoveRoadProvider();
                listToReturn.Add(nearbyStructure);
            }
        }
        return listToReturn;
    }
}

public enum RotationValue
{ // How much we should rotate to match our currect road-setup.
    R0,
    R90,
    R180,
    R270
}
