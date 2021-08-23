using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class StructureRepositoryTest
{
    StructureRepository repository;

    [OneTimeSetUp]
    public void Init()
    {
        CollectionSO collection = new CollectionSO();
        var road = new RoadStructureSO();
        road.buildingName = "Road";
        var facility = new SingleFacilitySO();
        facility.buildingName = "PowerPlant";
        var zone = new ZoneStructureSO();
        zone.buildingName = "Commercial";
        collection.roadStructure = road;
        collection.singleStructureList = new List<SingleStructureBaseSO>();
        collection.singleStructureList.Add(facility);
        collection.zonesList.Add(zone);
        GameObject testObject = new GameObject();
        repository = testObject.AddComponent<StructureRepository>();
        repository.modelDataCollection = collection;
    }


    [UnityTest]
    public IEnumerator StructureRepositoryTestWithEnumeratorPasses()
    {
        int quantity = repository.GetZoneNames().Count; 
        yield return new WaitForEndOfFrame();
        Assert.AreEqual(1, quantity);
    }

    [UnityTest]
    public IEnumerator StructureRepositoryTestZoneListNamePasses()
    {
        string name = repository.GetZoneNames()[0];
        yield return new WaitForEndOfFrame();
        Assert.AreEqual("Commercial", name);
    }
}
