using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StructureBaseSO : ScriptableObject
{
    public string buildingName;
    public GameObject prefab;
    public int placementCost;
    public int upKeepCost;
    public int income;
    public bool requireRoadAccess;
    public bool requirePower;
    public bool requireWater;

}
