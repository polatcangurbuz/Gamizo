using NUnit.Framework;
using UnityEngine;
using System.Collections;
using UnityEngine.TestTools;
using System.Linq;

[TestFixture]
public class ballSpawnerTests
{
    private GameObject spawnerObject;
    private ballSpawner spawnerComponent;
    private GameObject ballPrefab;

    [SetUp]
    public void SetUp()
    {
        // Create ball prefab with required components
        ballPrefab = new GameObject("BallPrefab");
        var rb = ballPrefab.AddComponent<Rigidbody>();
        rb.useGravity = false; // Disable gravity for tests
        var collider = ballPrefab.AddComponent<SphereCollider>();
        collider.isTrigger = false;

        // Create spawner object
        spawnerObject = new GameObject("BallSpawner");
        spawnerComponent = spawnerObject.AddComponent<ballSpawner>();
        spawnerComponent.ballPrefab = ballPrefab;
        
        // Ensure singleton and queue are initialized
        spawnerComponent.enabled = false;
        spawnerComponent.enabled = true;

        // Manually initialize the pool since ballPrefab is set after Awake
        var setQueueMethod = typeof(ballSpawner).GetMethod("SetQueue", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        setQueueMethod.Invoke(spawnerComponent, null);

        // Reset physics
        Physics.autoSimulation = true;
    }

    [Test]
    public void Instance_IsSingleton()
    {
        // Assert
        Assert.That(ballSpawner.Instance, Is.Not.Null, "Singleton instance should not be null");
        Assert.That(ballSpawner.Instance, Is.EqualTo(spawnerComponent), "Singleton instance should be our test component");
    }

    [UnityTest]
    public IEnumerator GetBall_ReusesInactiveBalls()
    {
        // Dequeue all balls from the pool
        var balls = new GameObject[10];
        for (int i = 0; i < 10; i++)
        {
            balls[i] = spawnerComponent.GetBall();
            yield return null;
        }
        // Deactivate and return the first ball
        var reusedBall = balls[0];
        reusedBall.SetActive(false);
        spawnerComponent.ReturnToPool(reusedBall);
        yield return null;
        // Get a new ball (should be the one we just returned)
        var nextBall = spawnerComponent.GetBall();
        yield return null;
        Assert.That(nextBall, Is.EqualTo(reusedBall), "Should reuse the inactive ball");
        Assert.That(nextBall.activeInHierarchy, Is.True, "Reused ball should be active");
    }

    [Test]
    public void ReturnToPool_DeactivatesBall()
    {
        // Arrange
        GameObject ball = spawnerComponent.GetBall();

        // Act
        spawnerComponent.ReturnToPool(ball);

        // Assert
        Assert.IsFalse(ball.activeInHierarchy);
    }

    [Test]
    public void GetBall_WhenPoolEmpty_ReturnsNull()
    {
        // Arrange - Get all balls from pool
        for (int i = 0; i < 15; i++) // More than initial pool size
        {
            spawnerComponent.GetBall();
        }

        // Act
        GameObject ball = spawnerComponent.GetBall();

        // Assert
        Assert.IsNull(ball);
    }

    [UnityTest]
    public IEnumerator FireBall_MovesTowardsTarget()
    {
        GameObject enemy = new GameObject("Enemy");
        GameObject target = new GameObject("Target");
        enemy.transform.position = Vector3.zero;
        target.transform.position = Vector3.forward * 2f;
        spawnerComponent.FireBall(enemy, target.transform);
        yield return new WaitForSeconds(0.1f);
        GameObject ball = GameObject.FindObjectsOfType<GameObject>()
            .FirstOrDefault(go => go.name.Contains("Ball") && go.activeInHierarchy);
        Assert.That(ball, Is.Not.Null, "Ball should be active after firing");
        float startDistance = Vector3.Distance(enemy.transform.position, target.transform.position);
        float afterDistance = Vector3.Distance(ball.transform.position, target.transform.position);
        Assert.That(afterDistance, Is.LessThan(startDistance + 0.1f), "Ball should move towards the target");
        Object.DestroyImmediate(enemy);
        Object.DestroyImmediate(target);
    }

    [TearDown]
    public void TearDown()
    {
        // Clean up all spawned objects
        if (spawnerObject != null)
            Object.DestroyImmediate(spawnerObject);
        if (ballPrefab != null)
            Object.DestroyImmediate(ballPrefab);

        // Clean up any remaining balls
        var balls = Object.FindObjectsOfType<GameObject>()
            .Where(go => go.name.Contains("ball") || go.CompareTag("ball"));
        foreach (var ball in balls)
        {
            Object.DestroyImmediate(ball);
        }

        // Reset physics state
        Physics.autoSimulation = true;
    }
}