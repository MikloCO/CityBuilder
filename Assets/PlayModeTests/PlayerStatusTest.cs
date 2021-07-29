using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

[TestFixture]
public class PlayerStatusTest
{
    UIController uiController;
    GameManager gameMangerComponent;

    [SetUp]
    public void Init()
    {
        GameObject gameManagerObject = new GameObject();
        var cameraMovementComponent = gameManagerObject.AddComponent<CameraMovement>();
        uiController = gameManagerObject.AddComponent<UIController>();
        GameObject buttonBuildObject = new GameObject();
        GameObject cancelButtonObject = new GameObject();
        GameObject cancelPanel = new GameObject();
        uiController.cancleActionButton = cancelButtonObject.AddComponent<Button>();
        var buttonBuildComponent = buttonBuildObject.AddComponent<Button>();
        uiController.buildResidentialAreaButton = buttonBuildComponent;
        uiController.cancelActionPanel = cancelButtonObject;
        gameMangerComponent = gameManagerObject.AddComponent<GameManager>();
        gameMangerComponent.cameraMovement = cameraMovementComponent;
        gameMangerComponent.uiController = uiController;
    }

    // A Test behaves as an ordinary method
    [Test]
    public void PlayerStatusTestSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator PlayerBuildingSingleStructureStateTestWithEnumeratorPasses()
    {
        yield return new WaitForEndOfFrame(); //calls awake
        yield return new WaitForEndOfFrame(); //calls start methid,
        uiController.buildResidentialAreaButton.onClick.Invoke();
        yield return new WaitForEndOfFrame();
        Assert.IsTrue(gameMangerComponent.State is PlayerBuildingSingleStructureState);
    }

    [UnityTest]
    public IEnumerator PlayerSelectionStateTestWithEnumeratorPasses()
    {
        yield return new WaitForEndOfFrame(); //calls awake
        yield return new WaitForEndOfFrame(); //calls start method,
        Assert.IsTrue(gameMangerComponent.State is PlayerSelectionState);
    }
}
