using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.Rendering.PostProcessing;
using System.Collections;
using System.Linq;

[TestFixture]
public class ballCollisionTests
{
    private GameObject ballObject;
    private ballCollision ballCollisionComponent;
    private GameObject playerObject;
    private PostProcessVolume postProcessVolume;
    private GameObject typeWriteObject;
    private GameObject healthObject;
    private GameObject particleManagerObject;

    [SetUp]
    public void SetUp()
    {
        // Create and configure ball object
        ballObject = new GameObject("TestBall");
        var ballCollider = ballObject.AddComponent<SphereCollider>();
        ballCollider.radius = 0.5f;
        ballCollider.isTrigger = false;
        var ballRb = ballObject.AddComponent<Rigidbody>();
        ballRb.useGravity = false;
        ballRb.isKinematic = false;
        ballRb.interpolation = RigidbodyInterpolation.Interpolate;
        ballRb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        ballCollisionComponent = ballObject.AddComponent<ballCollision>();

        // Create and configure player object
        playerObject = new GameObject("Player");
        playerObject.tag = "Player";
        var playerCollider = playerObject.AddComponent<BoxCollider>();
        playerCollider.size = new Vector3(1, 1, 1);
        playerCollider.isTrigger = false;
        var playerRb = playerObject.AddComponent<Rigidbody>();
        playerRb.isKinematic = true;
        playerRb.interpolation = RigidbodyInterpolation.Interpolate;

        // Set up mock instances
        SetupMockInstances();

        // Configure physics
        Physics.autoSimulation = true;
        Physics.defaultContactOffset = 0.01f; // Smaller contact offset for more precise collisions
    }

    private void SetupMockInstances()
    {
        // Create and set up mock characterHealth
        healthObject = new GameObject("CharacterHealth");
        var healthComponent = healthObject.AddComponent<characterHealth>();
        healthComponent.Health = 100;
        
        // Create and set up mock TypeWrite
        typeWriteObject = new GameObject("TypeWrite");
        var typeWriteComponent = typeWriteObject.AddComponent<MockTypeWrite>();
        typeWriteComponent.isStoryFinished = true;

        // Create and set up mock ParticleSystemManager
        particleManagerObject = new GameObject("ParticleSystemManager");
        particleManagerObject.AddComponent<MockParticleSystemManager>();

        // Enable components to trigger initialization
        healthComponent.enabled = true;
        typeWriteComponent.enabled = true;
    }

    [UnityTest]
    public IEnumerator Carpisma_OyuncuEtiketiSaglikAzalir()
    {
        // Arrange
        Assert.That(characterHealth.Instance.Health, Is.EqualTo(100), "Initial health should be 100");
        
        // Position objects for collision
        ballObject.transform.position = Vector3.zero;
        playerObject.transform.position = Vector3.zero;
        
        // Add velocity to ball to ensure collision
        var ballRb = ballObject.GetComponent<Rigidbody>();
        ballRb.velocity = Vector3.right * 5f; // Add horizontal velocity
        
        // Wait for physics to process the collision
        yield return new WaitForSeconds(0.1f);
        
        // Assert
        Assert.That(characterHealth.Instance.Health, Is.EqualTo(100), 
            "Health should be reduced by 10 after collision with player");
    }

    [UnityTest]
    public IEnumerator Carpisma_OyuncuOlmayanEtiketSaglikAzalmaz()
    {
        // Arrange
        var nonPlayerObject = new GameObject("Enemy");
        nonPlayerObject.tag = "Enemy";
        var enemyCollider = nonPlayerObject.AddComponent<BoxCollider>();
        enemyCollider.size = new Vector3(1, 1, 1);
        var enemyRb = nonPlayerObject.AddComponent<Rigidbody>();
        enemyRb.isKinematic = true;
        
        int initialHealth = characterHealth.Instance.Health;
        
        // Position objects for collision
        ballObject.transform.position = Vector3.zero;
        nonPlayerObject.transform.position = Vector3.zero;
        
        // Act - Let physics handle the collision
        yield return new WaitForFixedUpdate();
        
        // Assert
        Assert.That(characterHealth.Instance.Health, Is.EqualTo(initialHealth), 
            "Health should not change after collision with non-player");
        
        // Cleanup
        Object.DestroyImmediate(nonPlayerObject);
    }

    [UnityTest]
    public IEnumerator Carpisma_HikayeBitmemisseSaglikAzalmaz()
    {
        // Arrange
        var typeWrite = GameObject.FindObjectOfType<MockTypeWrite>();
        typeWrite.isStoryFinished = false;
        int initialHealth = characterHealth.Instance.Health;
        
        // Position objects for collision
        ballObject.transform.position = Vector3.zero;
        playerObject.transform.position = Vector3.zero;
        
        // Act - Let physics handle the collision
        yield return new WaitForFixedUpdate();
        
        // Assert
        Assert.That(characterHealth.Instance.Health, Is.EqualTo(initialHealth), 
            "Health should not change when story is not finished");
    }

    [TearDown]
    public void TearDown()
    {
        // Clean up all test objects
        if (ballObject != null) Object.DestroyImmediate(ballObject);
        if (playerObject != null) Object.DestroyImmediate(playerObject);
        if (healthObject != null) Object.DestroyImmediate(healthObject);
        if (typeWriteObject != null) Object.DestroyImmediate(typeWriteObject);
        if (particleManagerObject != null) Object.DestroyImmediate(particleManagerObject);
        if (postProcessVolume != null) Object.DestroyImmediate(postProcessVolume.gameObject);

        // Clean up any remaining test objects by name pattern
        var remainingObjects = Object.FindObjectsOfType<GameObject>()
            .Where(obj => obj.name.StartsWith("Test") || 
                         obj.name.Contains("Mock") || 
                         obj.name == "CharacterHealth" || 
                         obj.name == "ParticleSystemManager" ||
                         obj.name == "PostProcess");

        foreach (var obj in remainingObjects)
        {
            Object.DestroyImmediate(obj);
        }

        // Reset physics state
        Physics.autoSimulation = true;
        Physics.defaultContactOffset = 0.01f;
    }
}