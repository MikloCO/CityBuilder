using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullScriptableSO : StructureBaseSO
{
    private void OnEnable()
    {
        buildingName = "nullable object";
        prefab = null;
        placementCost = 0;
        upKeepCost = 0;
        requireRoadAccess = false;
        requireWater = false;
        requirePower = false;
        income = 0;

    }
}
