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

}
