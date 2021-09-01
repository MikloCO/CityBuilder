using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StructureRepository : MonoBehaviour
{
    public CollectionSO modelDataCollection;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<string> GetZoneNames()
    {
        return modelDataCollection.zonesList.Select(zone => zone.buildingName).ToList();
    }

    public List<string> GetSingleStructureNames()
    {
        return modelDataCollection.singleStructureList.Select(facility => facility.buildingName).ToList();
    }

    public string GetRoadStructureName()
    {
        return modelDataCollection.roadStructure.buildingName;
    }

    public GameObject GetBuildingPrefabByName(string structureName, StructureType structuretype)
    {
        GameObject structurePrefabToReturn = null;
        switch (structuretype)
        {
            case StructureType.Zone:
                structurePrefabToReturn = GetZoneBuildingPrefabByName(structureName);
                break;
            case StructureType.SingleStructure:
                structurePrefabToReturn = GetSingleStructurePrefabByName(structureName);
                break;
            case StructureType.Road:
                structurePrefabToReturn = GetRoadPrefabByName();
                break;
            default:
                throw new Exception("No such type implemented for " + structuretype);
        }

        if(structurePrefabToReturn == null)
        {
            throw new Exception("No prefab for that name " + structureName);
        }
        return structurePrefabToReturn;
    }

    private GameObject GetRoadPrefabByName()
    {
        return modelDataCollection.roadStructure.prefab;
    }

    private GameObject GetSingleStructurePrefabByName(string structureName)
    {
        var foundstructure = modelDataCollection.singleStructureList.Where(structure => structure.buildingName == structureName).FirstOrDefault(); //Return the SO (Scriptable Object)
        if(foundstructure != null)
        {
            return foundstructure.prefab;
        }
        return null;
    }

    private GameObject GetZoneBuildingPrefabByName(string structureName)
    {
        var foundstructure = modelDataCollection.zonesList.Where(structure => structure.buildingName == structureName).FirstOrDefault(); //Return the SO (Scriptable Object)
        if (foundstructure != null)
        {
            return foundstructure.prefab;
        }
        return null;
    }
}

public enum StructureType
{
    Zone,
    SingleStructure,
    Road,
    None
}
