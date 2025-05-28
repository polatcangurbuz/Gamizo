using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;

[TestFixture]
public class characterMovementTests
{
    private GameObject characterObject;
    private characterMovement movementComponent;
    private Rigidbody rigidBody;
    private MockTopuzMovement mockTopuzMovement;
    private GameObject topuzObject;

    [SetUp]
    public void SetUp()
    {
        // Create character with required components
        characterObject = new GameObject("Character");
        rigidBody = characterObject.AddComponent<Rigidbody>();
        movementComponent = characterObject.AddComponent<characterMovement>();

        // Configure rigidbody
        rigidBody.useGravity = false;
        rigidBody.isKinematic = false;

        // Create and configure mock TopuzMovement
        topuzObject = new GameObject("TopuzMovement");
        mockTopuzMovement = topuzObject.AddComponent<MockTopuzMovement>();

        // Set the TopuzMovement reference using reflection
        var topuzField = typeof(characterMovement).GetField("TopuzMovement",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        topuzField?.SetValue(movementComponent, mockTopuzMovement);

        // Reset physics
        Physics.autoSimulation = true;
    }

    [UnityTest]
    public IEnumerator FixedUpdate_KonumSinirlarIcerisindeMi()
    {
        // Arrange
        characterObject.transform.position = new Vector3(0, 3f, 1f); // Outside bounds

        // Allow physics to update
        yield return new WaitForFixedUpdate();

        // Assert
        Vector3 pos = characterObject.transform.position;
        Assert.That(pos.y, Is.LessThanOrEqualTo(2.713f).And.GreaterThanOrEqualTo(1.624f),
            "Y position should be clamped between 1.624 and 2.713");
        Assert.That(pos.z, Is.LessThanOrEqualTo(0.879f).And.GreaterThanOrEqualTo(-0.288f),
            "Z position should be clamped between -0.288 and 0.879");
    }

    [UnityTest]
    public IEnumerator Update_HareketYonuHesaplanir()
    {
        mockTopuzMovement.currentX = 1f;
        mockTopuzMovement.currentZ = -1f;
        yield return null; // Let Update run
        yield return new WaitForFixedUpdate(); // Let FixedUpdate run
        Assert.That(rigidBody.velocity.y, Is.EqualTo(-0.17f).Within(0.17f), "Y velocity should match expected");
        Assert.That(rigidBody.velocity.z, Is.EqualTo(-0.17f).Within(0.17f), "Z velocity should match expected");
    }

    [UnityTest]
    public IEnumerator HareketHizi_HareketHesabiniEtkiler()
    {
        // X-Z yönünde hareket başlat
        mockTopuzMovement.currentX = 1f;
        mockTopuzMovement.currentZ = 1f;

        // Düşük hızla başla
        movementComponent.movSpeed = 0.08f;
        yield return WaitForPhysicsFrame();
        float velocityWithDefault = rigidBody.velocity.magnitude;

        // Hızı artır
        movementComponent.movSpeed = 0.16f;
        yield return WaitForPhysicsFrame();
        float velocityWithIncreased = rigidBody.velocity.magnitude;

        Assert.That(
            velocityWithIncreased,
            Is.GreaterThan(velocityWithDefault),
            "Increasing movSpeed should increase velocity"
        );
    }

    private IEnumerator WaitForPhysicsFrame()
    {
        yield return new WaitForFixedUpdate();
        yield return null;
    }

    [TearDown]
    public void TearDown()
    {
        // Clean up all test objects
        if (characterObject != null)
            Object.DestroyImmediate(characterObject);
        if (topuzObject != null)
            Object.DestroyImmediate(topuzObject);

        // Reset physics state
        Physics.autoSimulation = true;
    }
}