using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class GridStructureTests
    {
        GridStructure structure;

       [OneTimeSetUp]
        public void Init()
        {
           structure = new GridStructure(3, 100, 100);
        }
        
        // A Test behaves as an ordinary method
        [Test]
        public void CalculateGridPositionIntPass()
        {
            //Arrange
            Vector3 position = new Vector3(0, 0, 0);
            //Act
            Vector3 returnPosition = structure.CalculateGridPosition(position);
            //Assert
            Assert.AreEqual(Vector3.zero, returnPosition);
        }

        [Test]
        public void CalculateGridPositionFloatsPasses()
        {
            Vector3 position = new Vector3(2.9f, 0.0f, 2.9f);
            Vector3 returnPosition = structure.CalculateGridPosition(position);
            Assert.AreEqual(Vector3.zero, returnPosition);
        }

        [Test]
        public void CalculateGridPositionFloatsFails()
        {
            Vector3 position = new Vector3(3.1f, 0.0f, 0.0f);
            Vector3 returnPosition = structure.CalculateGridPosition(position);
            Assert.AreNotEqual(Vector3.zero, returnPosition);
        }

        [Test]
        public void PlaceStructureAndCheckIsPosTakenPass()
        {
            Vector3 position = new Vector3(3, 0, 3);
            Vector3 returnPosition = structure.CalculateGridPosition(position);
            GameObject testGameObject = new GameObject("TestGameObject");

            structure.PlaceStructureOnTheGrid(testGameObject, position, null);
            Assert.IsTrue(structure.bIsCellTaken(position));
        }
   
        [Test]
        public void PlaceStructureAndcheckIfTaken_MinRange()
        {
            Vector3 position = new Vector3(0, 0, 0);
            Vector3 returnPosition = structure.CalculateGridPosition(position);
            GameObject testGameObject = new GameObject("TestGameObject");
            
            structure.PlaceStructureOnTheGrid(testGameObject, position, null);
            Assert.IsTrue(structure.bIsCellTaken(position));
        }

        [Test]
        public void PlaceStructureAndcheckIfTaken_MaxRange()
        {
            Vector3 position = new Vector3(297, 0, 297);
            Vector3 returnposition = structure.CalculateGridPosition(position);
            GameObject testGameObject = new GameObject("TestGameObject");

            structure.PlaceStructureOnTheGrid(testGameObject, returnposition, null);
            Assert.IsTrue(structure.bIsCellTaken(position));
        }

        [Test] 
        public void PlaceStructureAndIfPosIsTakenThrowNullObject()
        {
            Vector3 position = new Vector3(3, 0, 3);
            //Act
            Vector3 returnPosition = structure.CalculateGridPosition(position);
            GameObject testGameObject = null;
            
            structure.PlaceStructureOnTheGrid(testGameObject, returnPosition, null);
            Assert.IsTrue(structure.bIsCellTaken(position)); //Might not work properly. 
        }

        [Test]
        public void PlaceStructureAndCheckIfTakenIndexIsOutOfBounds()
        {
            Vector3 position = new Vector3(303, 0, 303);
            Assert.Throws<IndexOutOfRangeException>(() => structure.bIsCellTaken(position));
        }
    }
}
