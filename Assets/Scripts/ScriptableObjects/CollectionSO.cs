using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New collection", menuName = "CityBuilding/CollectionSO")]
public class CollectionSO : ScriptableObject
{
    public RoadStructureSO roadStructure;
    public List<SingleStructureBaseSO> singleStructureList;
    public List<ZoneStructureSO> zonesList;
}
