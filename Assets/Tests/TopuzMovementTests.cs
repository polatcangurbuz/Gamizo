using NUnit.Framework;
using UnityEngine;

public class TopuzMovementTests
{
    private GameObject topuzObject;
    private TopuzMovement topuzMovement;

    [SetUp]
    public void Setup()
    {
        topuzObject = new GameObject();
        topuzMovement = topuzObject.AddComponent<TopuzMovement>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.Destroy(topuzObject);
    }

    [Test]
    public void TopuzMovement_InitialValues_AreCorrect()
    {
        Assert.AreEqual(0f, topuzMovement.currentX);
        Assert.AreEqual(0f, topuzMovement.currentZ);
        Assert.AreEqual(0.3f, topuzMovement.sensitivity);
    }
}