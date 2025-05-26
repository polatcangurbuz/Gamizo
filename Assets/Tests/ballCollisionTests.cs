using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.Rendering.PostProcessing;
using System.Collections;

public class ballCollisionTests
{
    private GameObject ballObject;
    private ballCollision ballCollisionComponent;
    private GameObject playerObject;
    private PostProcessVolume postProcessVolume;

    [SetUp]
    public void SetUp()
    {
        // Create ball object with collision component
        ballObject = new GameObject("TestBall");
        ballCollisionComponent = ballObject.AddComponent<ballCollision>();
        ballObject.AddComponent<Collider>();

        // Create player object
        playerObject = new GameObject("Player");
        playerObject.tag = "Player";
        playerObject.AddComponent<Collider>();

        // Setup post-process volume for chromatic aberration testing
        GameObject postProcessObject = new GameObject("PostProcess");
        postProcessVolume = postProcessObject.AddComponent<PostProcessVolume>();
        
        // Mock PostProcessManager and characterHealth instances
        SetupMockInstances();
    }

    private void SetupMockInstances()
    {
        // Create mock characterHealth instance
        GameObject healthObject = new GameObject("CharacterHealth");
        characterHealth healthComponent = healthObject.AddComponent<characterHealth>();
        
        // Create mock TypeWrite instance
        GameObject typeWriteObject = new GameObject("TypeWrite");
        var typeWriteComponent = typeWriteObject.AddComponent<MockTypeWrite>();
        typeWriteComponent.isStoryFinished = true;
    }

    [Test]
    public void OnCollisionEnter_WithPlayerTag_ReducesHealth()
    {
        // Arrange
        Collision mockCollision = CreateMockCollision(playerObject);
        
        // Act
        ballCollisionComponent.OnCollisionEnter(mockCollision);
        
        // Assert
        Assert.AreEqual(90, characterHealth.Instance.Health);
    }

    [Test]
    public void OnCollisionEnter_WithNonPlayerTag_DoesNotReduceHealth()
    {
        // Arrange
        GameObject nonPlayerObject = new GameObject("Enemy");
        nonPlayerObject.tag = "Enemy";
        Collision mockCollision = CreateMockCollision(nonPlayerObject);
        int initialHealth = characterHealth.Instance.Health;
        
        // Act
        ballCollisionComponent.OnCollisionEnter(mockCollision);
        
        // Assert
        Assert.AreEqual(initialHealth, characterHealth.Instance.Health);
    }

    [Test]
    public void OnCollisionEnter_WhenStoryNotFinished_DoesNotReduceHealth()
    {
        // Arrange
        var typeWrite = GameObject.FindObjectOfType<MockTypeWrite>();
        typeWrite.isStoryFinished = false;
        Collision mockCollision = CreateMockCollision(playerObject);
        int initialHealth = characterHealth.Instance.Health;
        
        // Act
        ballCollisionComponent.OnCollisionEnter(mockCollision);
        
        // Assert
        Assert.AreEqual(initialHealth, characterHealth.Instance.Health);
    }

    private Collision CreateMockCollision(GameObject gameObject)
    {
        // This is a simplified mock - in real Unity tests you'd use more sophisticated mocking
        return new MockCollision { gameObject = gameObject };
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(ballObject);
        Object.DestroyImmediate(playerObject);
        Object.DestroyImmediate(postProcessVolume?.gameObject);
    }
}