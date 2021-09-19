using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    [TestFixture]
    public class GridStructureTests
    {
        GridStructure grid;
        [SetUp]
        public void Init()
        {
            grid = new GridStructure(3, 100, 100);
        }
        #region GridPositionTests
        // A Test behaves as an ordinary method
        [Test]
        public void CalculateGridPositionPasses()
        {
            Vector3 position = new Vector3(0, 0, 0);
            //Act
            Vector3 returnPosition = grid.CalculateGridPosition(position);
            //Assert
            Assert.AreEqual(Vector3.zero, returnPosition);
        }

        [Test]
        public void CalculateGridPositionFloatsPasses()
        {

            Vector3 position = new Vector3(2.9f, 0, 2.9f);
            //Act
            Vector3 returnPosition = grid.CalculateGridPosition(position);
            //Assert
            Assert.AreEqual(Vector3.zero, returnPosition);
        }

        [Test]
        public void CalculateGridPositionFail()
        {

            Vector3 position = new Vector3(3.1f, 0, 0);
            //Act
            Vector3 returnPosition = grid.CalculateGridPosition(position);
            //Assert
            Assert.AreNotEqual(Vector3.zero, returnPosition);
        }
        #endregion


       
        #region GridCellTests
        [Test]
        public void PlaceStructure303AndCheckIsTakenPasses()
        {

            Vector3 position = new Vector3(3, 0, 3);
            //Act
            Vector3 returnPosition = grid.CalculateGridPosition(position);
            GameObject testGameObject = new GameObject("TestGameObject");
            grid.PlaceStructureOnTheGrid(testGameObject, position, null);
            //Assert
            Assert.IsTrue(grid.bIsCellTaken(position));
        }
        [Test]
        public void PlaceStructureMINAndCheckIsTakenPasses()
        {

            Vector3 position = new Vector3(0, 0, 0);
            //Act
            Vector3 returnPosition = grid.CalculateGridPosition(position);
            GameObject testGameObject = new GameObject("TestGameObject");
            grid.PlaceStructureOnTheGrid(testGameObject, position, null);
            //Assert
            Assert.IsTrue(grid.bIsCellTaken(position));
        }
        [Test]
        public void PlaceStructureMAXAndCheckIsTakenPasses()
        {

            Vector3 position = new Vector3(297, 0, 297);
            //Act
            Vector3 returnPosition = grid.CalculateGridPosition(position);
            GameObject testGameObject = new GameObject("TestGameObject");
            grid.PlaceStructureOnTheGrid(testGameObject, position, null);
            //Assert
            Assert.IsTrue(grid.bIsCellTaken(position));
        }

        [Test]
        public void PlaceStructure303AndCheckIsTakenNullObjectShouldFail()
        {

            Vector3 position = new Vector3(3, 0, 3);
            //Act
            Vector3 returnPosition = grid.CalculateGridPosition(position);
            GameObject testGameObject = null;
            grid.PlaceStructureOnTheGrid(testGameObject, position, null);
            //Assert
            Assert.IsFalse(grid.bIsCellTaken(position));
        }

        [Test]
        public void PlaceStructureAndCheckIsTakenIndexOutOfBoundsFail()
        {

            Vector3 position = new Vector3(303, 0, 303);
            //Act
            //Assert
            Assert.Throws<IndexOutOfRangeException>(() => grid.bIsCellTaken(position));
        }

        [Test]
        public void RetreiveStructureFromCellGameObjectPasses()
        {

            Vector3 position = new Vector3(297, 0, 297);
            //Act
            Vector3 returnPosition = grid.CalculateGridPosition(position);
            GameObject testGameObject = new GameObject("TestGameObject");
            grid.PlaceStructureOnTheGrid(testGameObject, position, null);
            GameObject retreivedGameObject = grid.GetStructureFromGrid(position);
            //Assert
            Assert.AreEqual(testGameObject, retreivedGameObject);
        }

        [Test]
        public void RetreiveStructureFromCellNullPasses()
        {

            Vector3 position = new Vector3(297, 0, 297);
            //Act
            Vector3 returnPosition = grid.CalculateGridPosition(position);
            //clean up after any other test
            grid.RemoveStructureFromTheGrid(position);
            GameObject retreivedGameObject = grid.GetStructureFromGrid(position);
            //Assert
            Assert.AreEqual(null, retreivedGameObject);
        }

        [Test]
        public void GetPositionOfTheNeighbourIfExistsTestPass()
        {

            Vector3 position = new Vector3(0, 0, 0);

            var neighbourPosition = grid.GetPositionOfTheNeighbourIfExists(position, Direction.Up);
            Assert.AreEqual(new Vector3Int(0, 0, 3), neighbourPosition.Value);
        }

        [Test]
        public void GetPositionOfTheNeighbourIfExistsTestFail()
        {

            Vector3 position = new Vector3(0, 0, 0);

            var neighbourPosition = grid.GetPositionOfTheNeighbourIfExists(position, Direction.Down);
            Assert.IsFalse(neighbourPosition.HasValue);
        }

        #endregion

        [Test]
        public void GetAllPositionFromTo()
        {

            Vector3Int startPosition = new Vector3Int(0, 0, 0);
            Vector3Int endPosition = new Vector3Int(6, 0, 3);

            var returnValues = grid.GetAllPositionsFromTo(startPosition, endPosition);
            Assert.IsTrue(returnValues.Count == 6);
            Assert.IsTrue(returnValues.Contains(new Vector3Int(0, 0, 0)));
            Assert.IsTrue(returnValues.Contains(new Vector3Int(3, 0, 0)));
            Assert.IsTrue(returnValues.Contains(new Vector3Int(6, 0, 0)));
            Assert.IsTrue(returnValues.Contains(new Vector3Int(0, 0, 3)));
            Assert.IsTrue(returnValues.Contains(new Vector3Int(3, 0, 3)));
            Assert.IsTrue(returnValues.Contains(new Vector3Int(6, 0, 3)));
        }

        [Test]
        public void GetDataStructureTest()
        {
            RoadStructureSO road = ScriptableObject.CreateInstance<RoadStructureSO>();
            SingleStructureBaseSO singleStructure = ScriptableObject.CreateInstance<SingleFacilitySO>();
            GameObject gameObject = new GameObject();
            grid.PlaceStructureOnTheGrid(gameObject, new Vector3(0, 0, 0), road);
            grid.PlaceStructureOnTheGrid(gameObject, new Vector3(3, 0, 0), road);
            grid.PlaceStructureOnTheGrid(gameObject, new Vector3(0, 0, 3), singleStructure);
            grid.PlaceStructureOnTheGrid(gameObject, new Vector3(3, 0, 3), singleStructure);
            var list = grid.GetAllStructures().ToList();
            Assert.IsTrue(list.Count == 4);
        }
    }
}