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
}

public enum RotationValue
{ // How much we should rotate to match our currect road-setup.
    R0,
    R90,
    R180,
    R270
}
