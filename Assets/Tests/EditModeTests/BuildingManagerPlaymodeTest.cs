using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    [TestFixture]
    public class BuildingManagerPlaymodeTest
    {
        BuildingManager buildingManager;
        Material materialTransparent;
        
        [SetUp]
        public void InitBeforeEveryTests()
        {
            PlacementManager placementManager = Substitute.For<PlacementManager>();
            materialTransparent = new Material(Shader.Find("Standard"));
            placementManager.transparentMaterial = materialTransparent;
            GameObject ground = new GameObject();
            ground.transform.position = Vector3.zero;
            StructureRepository structureRepository = Substitute.For<StructureRepository>();
            CollectionSO collection = new CollectionSO();
            RoadStructureSO road = new RoadStructureSO();
            road.buildingName = "Road";
            GameObject roadChild = new GameObject("Road", typeof(MeshRenderer));
            roadChild.GetComponent<MeshRenderer>().sharedMaterial.color = Color.blue; //.material.color = Color.blue;
            GameObject roadPrefab = new GameObject("Road");
            roadPrefab.transform.SetParent(roadPrefab.transform);
            road.prefab = roadPrefab;
            collection.roadStructure = road;
            structureRepository.modelDataCollection = collection;
            buildingManager = new BuildingManager(3, 10, 10, placementManager, structureRepository);
        }

        [UnityTest]
        public IEnumerator BuildingManagerPlaymodeDemolishConfirmationTest()
        {
            Vector3 inputPosition = PreparePlacement();
            PrepareDemolishion(inputPosition);
            buildingManager.ConfirmDemolishion();
            yield return new WaitForEndOfFrame();
            Assert.IsNull(buildingManager.CheckForStructureInGrid(inputPosition));
            
        }

        [UnityTest]
        public IEnumerator BuildingManagerPlaymodeDemolishNoConfirmationTest()
        {
            Vector3 inputPosition = PreparePlacement();
            PrepareDemolishion(inputPosition);
            yield return new WaitForEndOfFrame();
            Assert.IsNotNull(buildingManager.CheckForStructureInGrid(inputPosition));
        }

        [UnityTest]
        public IEnumerator BuildingManagerPlaymodeDemolishCancelTest()
        {
            Vector3 inputPosition = PreparePlacement();
            PrepareDemolishion(inputPosition);
            buildingManager.CancelDemolishion();
            yield return new WaitForEndOfFrame();
            Assert.IsNotNull(buildingManager.CheckForStructureInGrid(inputPosition));
        }

        [UnityTest]
        public IEnumerator BuildingManagerPlaymodePlacementConfirmationPassTests()
        {
            Vector3 inputPosition = PreparePlacement();
            buildingManager.ConfirmModification();
            yield return new WaitForEndOfFrame();
            Assert.IsNotNull(buildingManager.CheckForStructureInGrid(inputPosition));
        }

        private Vector3 PreparePlacement()
        {
            Vector3 inputPosition = new Vector3(1, 0, 1);
            string structureName = "Road";
            buildingManager.PrepareStructureForModification(inputPosition, structureName, StructureType.Road);
            return inputPosition;
        }

        private void PrepareDemolishion(Vector3 inputPosition)
        {
            buildingManager.ConfirmModification();
            buildingManager.PrepareStructureFromDemolishionAt(inputPosition);
        }

    }
}
