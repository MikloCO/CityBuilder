using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Tests
{
    [TestFixture]
    public class PlayerStatusTest
    {
        UIController uiController;
        GameManager gameManagerComponent;



        [SetUp]
        public void Init()
        {
            GameObject gameManagerObject = new GameObject();
            var camerMovementComponent = gameManagerObject.AddComponent<CameraMovement>();

            uiController = Substitute.For<UIController>();

            gameManagerComponent = gameManagerObject.AddComponent<GameManager>();
            gameManagerComponent.cameraMovement = camerMovementComponent;
            gameManagerComponent.uiController = uiController;
        }

        [UnityTest]
        public IEnumerator PlayerStatusPlayerBuildingSingleStructureStateTestWithEnumeratorPasses()
        {
            yield return new WaitForEndOfFrame(); //awake
            yield return new WaitForEndOfFrame(); //start
            gameManagerComponent.State.OnBuildSingleStructure(null);
            yield return new WaitForEndOfFrame();
            Assert.IsTrue(gameManagerComponent.State is PlayerBuildingSingleStructureState);

        }

        [UnityTest]
        public IEnumerator PlayerStatusPlayerBuildAreaStateTestWithEnumeratorPasses()
        {
            yield return new WaitForEndOfFrame(); //awake
            yield return new WaitForEndOfFrame(); //start
            gameManagerComponent.State.OnBuildArea(null);
            yield return new WaitForEndOfFrame();
            Assert.IsTrue(gameManagerComponent.State is PlayerBuildZoneState);

        }

        [UnityTest]
        public IEnumerator PlayerStatusPlayerBuildingRoadStateTestWithEnumeratorPasses()
        {
            yield return new WaitForEndOfFrame(); //awake
            yield return new WaitForEndOfFrame(); //start
            gameManagerComponent.State.OnBuildRoad(null);
            yield return new WaitForEndOfFrame();
            Assert.IsTrue(gameManagerComponent.State is PlayerBuildingRoadState);

        }

        [UnityTest]
        public IEnumerator PlayerStatusPlayerRemoveBuildingStateTestWithEnumeratorPasses()
        {
            yield return new WaitForEndOfFrame(); //awake
            yield return new WaitForEndOfFrame(); //start
            gameManagerComponent.State.OnDemolishAction();
            yield return new WaitForEndOfFrame();
            Assert.IsTrue(gameManagerComponent.State is PlayerRemoveBuildingState);

        }

        [UnityTest]
        public IEnumerator PlayerStatusPlayerSelectionStateTestWithEnumeratorPasses()
        {
            yield return new WaitForEndOfFrame(); //awake
            yield return new WaitForEndOfFrame(); //start
            Assert.IsTrue(gameManagerComponent.State is PlayerSelectionState);

        }
    }
}
