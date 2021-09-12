using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace Tests
{
    public class CellTest
    {
        [Test]
        public void CellSetGameObjectPass()
        {
            Cell cell = new Cell();
            cell.SetConstruction(new GameObject(), null);
            Assert.IsTrue(cell.IsTaken);
        }

        [Test]
        public void CellSetGameObjectNullFail()
        {
            Cell cell = new Cell();
            cell.SetConstruction(new GameObject(), null);
            Assert.IsFalse(cell.IsTaken);
        }
    }
}
