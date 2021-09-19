using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    [TestFixture]
    public class SingleStructureModificationHelperTests
    {
        GameObject tempObject;
        GridStructure grid;
        StructureType structureType = StructureType.Road;
        string structureName = "Road";
        Vector3 gridposition1 = Vector3.zero;
        StructureModificationHelper helper;

        [SetUp]
        public void Init()
        {
            StructureRepository structureRepository = TestHelpers.CreateStructureRepositoryContainingRoad();
            IPlacementManager placementManager = Substitute.For<IPlacementManager>();
            tempObject = new GameObject();
            placementManager.CreateGhostStructure(default, default).ReturnsForAnyArgs(tempObject);
            grid = new GridStructure(3, 10, 10);
            helper = new SingleStructurePlacementHelper(structureRepository, grid, placementManager, Substitute.For<ResourceManager>());

        }
        // A Test behaves as an ordinary method
        [Test]
        public void SingleStructureModificationHelperAddPositionPass()
        {
            helper.PrepareStructureForModification(gridposition1, structureName, structureType);
            GameObject objectInDictionary = helper.AccessStructureInDictionary(gridposition1);
            Assert.AreEqual(tempObject, objectInDictionary);
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
  
    }
}
