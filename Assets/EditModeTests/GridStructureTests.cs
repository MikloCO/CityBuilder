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
        public void GridStructureTestsSimplePasses()
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
    }
}
