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
}
