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
        [SetUp]
        public void Init()
        {
            StructureRepository structureRepository = TestHelpers.CreateStructureRepositoryContainingRoad();
            IPlacementManager placementManager = Substitute.For<IPlacementManager>();
            tempObject = new GameObject();
            placementManager.CreateGhostStructure(default, default).ReturnsForAnyArgs(tempObject);
        }
        // A Test behaves as an ordinary method
        [Test]
        public void SingleStructureModificationHelperTestsSimplePasses()
        {
            // Use the Assert class to test conditions
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
  
    }
}
