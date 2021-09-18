using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class RoadManagerTests
    {
        GridStructure grid;
        GameObject roadStraight = new GameObject();
        GameObject roadCorner = new GameObject();
        RoadStructureSO roadSO = new RoadStructureSO();
        GameObject road3Way = new GameObject();
        GameObject road4Way = new GameObject();

        [OneTimeSetUp]
        public void Init()
        {
            grid = new GridStructure(3, 10, 10);
            roadSO.prefab = roadStraight;
            roadSO.cornerPrefab = roadCorner;
            roadSO.threeWayPrefab = road3Way;
            roadSO.FourWayPrefab = road4Way;

            grid.PlaceStructureOnTheGrid(roadStraight, new Vector3(3, 0, 6), roadSO);
            grid.PlaceStructureOnTheGrid(roadStraight, new Vector3(6, 0, 3), roadSO);
            grid.PlaceStructureOnTheGrid(roadStraight, new Vector3(9, 0, 6), roadSO);
            grid.PlaceStructureOnTheGrid(roadStraight, new Vector3(6, 0, 9), roadSO);

            grid.PlaceStructureOnTheGrid(roadStraight, new Vector3(12, 0, 9), roadSO);
            grid.PlaceStructureOnTheGrid(roadStraight, new Vector3(15, 0, 6), roadSO);
        }

        [Test]
        public void GetStatusNoNeighbours()
        {
            var result = RoadManager.getRoadNeighbourStatus(new Vector3(27, 0, 27), grid, null);
            Assert.AreEqual(0, result);
        }

        [Test]
        public void GetStatusNeighbourRight()
        {
            var result = RoadManager.getRoadNeighbourStatus(new Vector3(0, 0, 6), grid, null);
            Assert.AreEqual((int)Direction.Right, result);
        }

        [Test]
        public void GetStatusNeighbourLeft()
        {
            var result = RoadManager.getRoadNeighbourStatus(new Vector3(18, 0, 6), grid, null);
            Assert.AreEqual((int)Direction.Left, result);
        }

        [Test]
        public void GetStatusNeighbourUp()
        {
            var result = RoadManager.getRoadNeighbourStatus(new Vector3(6, 0, 0), grid, null);
            Assert.AreEqual((int)Direction.Up, result);
        }

        [Test]
        public void GetStatusNeighbourDown()
        {
            var result = RoadManager.getRoadNeighbourStatus(new Vector3(12, 0, 12), grid, null);
            Assert.AreEqual((int)Direction.Down, result);
        }

        [Test]
        public void GetStatusNeighbourDownLeft()
        {
            var result = RoadManager.getRoadNeighbourStatus(new Vector3(15, 0, 9), grid, null);
            Assert.AreEqual((int)Direction.Down | (int)Direction.Left, result);
        }

        [Test]
        public void GetStatusNeighbourDownRight()
        {
            var result = RoadManager.getRoadNeighbourStatus(new Vector3(3, 0, 9), grid, null);
            Assert.AreEqual((int)Direction.Down | (int)Direction.Right, result);
        }
        [Test]
        public void GetStatusNeighbourUpRight()
        {
            var result = RoadManager.getRoadNeighbourStatus(new Vector3(3, 0, 3), grid, null);
            Assert.AreEqual((int)Direction.Up | (int)Direction.Right, result);
        }
        [Test]
        public void GetStatusNeighbourUpLeft()
        {
            var result = RoadManager.getRoadNeighbourStatus(new Vector3(9, 0, 3), grid, null);
            Assert.AreEqual((int)Direction.Up | (int)Direction.Left, result);
        }
        [Test]
        public void GetStatusNeighbourDownLeftRight()
        {
            var result = RoadManager.getRoadNeighbourStatus(new Vector3(9, 0, 9), grid, null);
            Assert.AreEqual((int)Direction.Down | (int)Direction.Left | (int)Direction.Right, result);
        }
        [Test]
        public void GetStatusNeighbourDownUpLeftRight()
        {
            var result = RoadManager.getRoadNeighbourStatus(new Vector3(6, 0, 6), grid, null);
            Assert.AreEqual((int)Direction.Up | (int)Direction.Down | (int)Direction.Left | (int)Direction.Right, result);
        }

        [Test]
        public void GetNeighboursAll4()
        {
            var result = RoadManager.GetRoadNeighbourForPosition(grid, new Vector3Int(6, 0, 6));
            Assert.IsTrue(result.ContainsKey(new Vector3Int(3, 0, 6)));
            Assert.IsTrue(result.ContainsKey(new Vector3Int(9, 0, 6)));
            Assert.IsTrue(result.ContainsKey(new Vector3Int(6, 0, 3)));
            Assert.IsTrue(result.ContainsKey(new Vector3Int(6, 0, 9)));
        }

        [Test]
        public void GetNeighboursRightLeftDown()
        {
            var result = RoadManager.GetRoadNeighbourForPosition(grid, new Vector3Int(9, 0, 9));
            Assert.IsTrue(result.ContainsKey(new Vector3Int(6, 0, 9)));
            Assert.IsTrue(result.ContainsKey(new Vector3Int(9, 0, 6)));
            Assert.IsTrue(result.ContainsKey(new Vector3Int(12, 0, 9)));
        }

        [Test]
        public void GetNeighboursRight()
        {
            var result = RoadManager.GetRoadNeighbourForPosition(grid, new Vector3Int(0, 0, 6));
            Assert.IsTrue(result.ContainsKey(new Vector3Int(3, 0, 6)));
        }

        [Test]
        public void CheckIfNeighbourIsRoadInDictionaryTrue()
        {
            var dictionary = new Dictionary<Vector3Int, GameObject>();
            var position = new Vector3Int(3, 0, 6);
            dictionary.Add(position,roadStraight);
            var result = RoadManager.CheckIfRoadNeighbourIsInDictionary(position, dictionary);
            Assert.IsTrue(result);
        }

        [Test]
        public void CheckIfNeighbourIsRoadInDictionaryFalse()
        {
            var dictionary = new Dictionary<Vector3Int, GameObject>();
            var position = new Vector3Int(3, 0, 6);
            dictionary.Add(position, roadStraight);
            var result = RoadManager.CheckIfRoadNeighbourIsInDictionary(new Vector3Int(6,0,6), dictionary);
            Assert.IsFalse(result);
        }

        [Test]
        public void CheckIfNeighbourIsRoadOnGridTrue()
        {
            var position = new Vector3Int(3, 0, 6);
            var result = RoadManager.CheckIfNeighbourIsRoadOnTheGrid(grid, position);
            Assert.IsTrue(result);
        }

        [Test] 
        public void CheckIfNeighbourIsRoadOnGridFalse()
        {
            var position = new Vector3Int(0, 0, 0);
            var result = RoadManager.CheckIfNeighbourIsRoadOnTheGrid(grid, position);
            Assert.IsFalse(result);
        }

        [Test]
        public void CheckIfNeighbourIsRoadInDictionaryCheckIfStraightFits0()
        {
            var result = RoadManager.CheckIfStraighRoadFits(0, null, roadSO);
            Assert.AreEqual(roadStraight, result.RoadPrefab);
            Assert.AreEqual(RotationValue.R0, result.RoadPrefabRotation);
        }

        [Test]
        public void CheckIfNeighbourIsRoadInDictionaryCheckIfStraightFitsDownR90()
        {
            var result = RoadManager.CheckIfStraighRoadFits((int)Direction.Down, null, roadSO);
            Assert.AreEqual(roadStraight, result.RoadPrefab);
            Assert.AreEqual(RotationValue.R90, result.RoadPrefabRotation);
        }

        [Test]
        public void CheckIfNeighbourIsRoadInDictionaryCheckIfStraightFitsUpR90()
        {
            var result = RoadManager.CheckIfStraighRoadFits((int)Direction.Up, null, roadSO);
            Assert.AreEqual(roadStraight, result.RoadPrefab);
            Assert.AreEqual(RotationValue.R90, result.RoadPrefabRotation);
        }
        [Test]
        public void CheckIfNeighbourIsRoadInDictionaryCheckIfStraightFitsUpDownR90()
        {
            var result = RoadManager.CheckIfStraighRoadFits((int)Direction.Up| (int)Direction.Down, null, roadSO);
            Assert.AreEqual(roadStraight, result.RoadPrefab);
            Assert.AreEqual(RotationValue.R90, result.RoadPrefabRotation);
        }

        [Test]
        public void CheckIfNeighbourIsRoadInDictionaryCheckIfStraightRightFitsR0()
        {
            var result = RoadManager.CheckIfStraighRoadFits((int)Direction.Right, null, roadSO);
            Assert.AreEqual(roadStraight, result.RoadPrefab);
            Assert.AreEqual(RotationValue.R0, result.RoadPrefabRotation);
        }
        [Test]
        public void CheckIfNeighbourIsRoadInDictionaryCheckIfStraightLeftFitsR0()
        {
            var result = RoadManager.CheckIfStraighRoadFits((int)Direction.Left, null, roadSO);
            Assert.AreEqual(roadStraight, result.RoadPrefab);
            Assert.AreEqual(RotationValue.R0, result.RoadPrefabRotation);
        }
        [Test]
        public void CheckIfNeighbourIsRoadInDictionaryCheckIfStraightRightLeftFitsR0()
        {
            var result = RoadManager.CheckIfStraighRoadFits((int)Direction.Left| (int)Direction.Right, null, roadSO);
            Assert.AreEqual(roadStraight, result.RoadPrefab);
            Assert.AreEqual(RotationValue.R0, result.RoadPrefabRotation);
        }

        [Test]
        public void CheckIfNeighbourIsRoadInDictionaryCheckIfCornerFitsR0()
        {
            var result = RoadManager.CheckifCornerFits((int)Direction.Up| (int)Direction.Right, null, roadSO);
            Assert.AreEqual(roadCorner, result.RoadPrefab);
            Assert.AreEqual(RotationValue.R0, result.RoadPrefabRotation);
        }

        [Test]
        public void CheckIfNeighbourIsRoadInDictionaryCheckIfCornerFitsR90()
        {
            var result = RoadManager.CheckifCornerFits((int)Direction.Down | (int)Direction.Right, null, roadSO);
            Assert.AreEqual(roadCorner, result.RoadPrefab);
            Assert.AreEqual(RotationValue.R90, result.RoadPrefabRotation);
        }
        [Test]
        public void CheckIfNeighbourIsRoadInDictionaryCheckIfCornerFitsR180()
        {
            var result = RoadManager.CheckifCornerFits((int)Direction.Down | (int)Direction.Left, null, roadSO);
            Assert.AreEqual(roadCorner, result.RoadPrefab);
            Assert.AreEqual(RotationValue.R180, result.RoadPrefabRotation);
        }
        [Test]
        public void CheckIfNeighbourIsRoadInDictionaryCheckIfCornerFitsR270()
        {
            var result = RoadManager.CheckifCornerFits((int)Direction.Up | (int)Direction.Left, null, roadSO);
            Assert.AreEqual(roadCorner, result.RoadPrefab);
            Assert.AreEqual(RotationValue.R270, result.RoadPrefabRotation);
        }
        [Test]
        public void CheckIfNeighbourIsRoadInDictionaryCheckIfThreeWayFitsR0()
        {
            var result = RoadManager.CheckifThreeWayFits((int)Direction.Up | (int)Direction.Right | (int)Direction.Down, null, roadSO);
            Assert.AreEqual(road3Way, result.RoadPrefab);
            Assert.AreEqual(RotationValue.R0, result.RoadPrefabRotation);
        }
        [Test]
        public void CheckIfNeighbourIsRoadInDictionaryCheckIfThreeWayFitsR90()
        {
            var result = RoadManager.CheckifThreeWayFits((int)Direction.Right | (int)Direction.Down | (int)Direction.Left, null, roadSO);
            Assert.AreEqual(road3Way, result.RoadPrefab);
            Assert.AreEqual(RotationValue.R90, result.RoadPrefabRotation);
        }
        [Test]
        public void IfNeighbourIsRoadInDictionaryCheckIfThreeWayFitsR180()
        {
            var result = RoadManager.CheckifThreeWayFits((int)Direction.Down | (int)Direction.Left | (int)Direction.Up, null, roadSO);
            Assert.AreEqual(road3Way, result.RoadPrefab);
            Assert.AreEqual(RotationValue.R180, result.RoadPrefabRotation);
        }
        [Test]
        public void CheckIfNeighbourIsRoadInDictionaryCheckIfThreeWayFitsR270()
        {
            var result = RoadManager.CheckifThreeWayFits((int)Direction.Up | (int)Direction.Left | (int)Direction.Right, null, roadSO);
            Assert.AreEqual(road3Way, result.RoadPrefab);
            Assert.AreEqual(RotationValue.R270, result.RoadPrefabRotation);
        }
        [Test]
        public void CheckIfNeighbourIsRoadInDictionaryCheckIfFourWayFitsR0()
        {
            var result = RoadManager.CheckIfFourWaysFit((int)Direction.Up | (int)Direction.Left | (int)Direction.Right | (int)Direction.Down , null, roadSO);
            Assert.AreEqual(road4Way, result.RoadPrefab);
            Assert.AreEqual(RotationValue.R0, result.RoadPrefabRotation);
        }

        [Test]
        public void GetCorrectRoadPrefab4WayGrid()
        {
            var result = RoadManager.GetCorrectRoadPrefab(new Vector3Int(6, 0, 6), roadSO, null, grid);
            Assert.AreEqual(road4Way, result.RoadPrefab);
            Assert.AreEqual(RotationValue.R0, result.RoadPrefabRotation);
        }
        [Test]
        public void GetCorrectRoadPrefabCornerDictionary()
        {
            var dictionary = new Dictionary<Vector3Int, GameObject>();
            dictionary.Add(new Vector3Int(3, 0, 0), roadStraight);
            dictionary.Add(new Vector3Int(0, 0, 3), roadStraight);
            var result = RoadManager.GetCorrectRoadPrefab(new Vector3Int(0, 0, 0),roadSO, dictionary, grid);
            Assert.AreEqual(roadCorner, result.RoadPrefab);
            Assert.AreEqual(RotationValue.R0, result.RoadPrefabRotation);
        }
    }
}
