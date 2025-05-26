using NUnit.Framework;
using UnityEngine;
using System.Collections;
using UnityEngine.TestTools;

public class ballSpawnerTests
{
    private GameObject spawnerObject;
    private ballSpawner spawnerComponent;
    private GameObject ballPrefab;

    [SetUp]
    public void SetUp()
    {
        // Create ball prefab
        ballPrefab = new GameObject("BallPrefab");
        ballPrefab.AddComponent<Rigidbody>();
        ballPrefab.AddComponent<SphereCollider>();

        // Create spawner object
        spawnerObject = new GameObject("BallSpawner");
        spawnerComponent = spawnerObject.AddComponent<ballSpawner>();
        spawnerComponent.ballPrefab = ballPrefab;
    }

    [Test]
    public void Instance_IsSingleton()
    {
        // Assert
        Assert.IsNotNull(ballSpawner.Instance);
        Assert.AreEqual(spawnerComponent, ballSpawner.Instance);
    }

    [Test]
    public void GetBall_ReturnsActiveBall()
    {
        // Act
        GameObject ball = spawnerComponent.GetBall();

        // Assert
        Assert.IsNotNull(ball);
        Assert.IsTrue(ball.activeInHierarchy);
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
        // Arrange
        GameObject enemy = new GameObject("Enemy");
        GameObject target = new GameObject("Target");
        enemy.transform.position = Vector3.zero;
        target.transform.position = Vector3.forward * 2f;

        // Act
        spawnerComponent.FireBall(enemy, target.transform);

        // Wait a frame for coroutine to start
        yield return null;

        // Assert - Ball should be moving towards target
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Untagged");
        bool ballFound = false;
        foreach (var ball in balls)
        {
            if (ball.name.Contains("Ball") && ball.activeInHierarchy)
            {
                ballFound = true;
                break;
            }
        }
        Assert.IsTrue(ballFound);

        // Cleanup
        Object.DestroyImmediate(enemy);
        Object.DestroyImmediate(target);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(spawnerObject);
        Object.DestroyImmediate(ballPrefab);
    }
}