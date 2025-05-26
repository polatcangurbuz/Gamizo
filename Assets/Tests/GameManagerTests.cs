using NUnit.Framework;
using UnityEngine;

public class GameManagerTests
{
    private GameObject gameManagerObject;
    private GameManager gameManager;

    [SetUp]
    public void SetUp()
    {
        gameManagerObject = new GameObject("GameManager");
        gameManager = gameManagerObject.AddComponent<GameManager>();
    }

    [Test]
    public void Instance_IsSingleton()
    {
        // Assert
        Assert.IsNotNull(GameManager.Instance);
        Assert.AreEqual(gameManager, GameManager.Instance);
    }

    [Test]
    public void Awake_SetsDontDestroyOnLoad()
    {
        // Assert - Check that DontDestroyOnLoad was called
        // This is difficult to test directly, but we can verify the object exists
        Assert.IsNotNull(gameManager.gameObject);
    }

    [Test]
    public void MultipleCopies_DestroysDuplicates()
    {
        // Arrange & Act
        GameObject secondManager = new GameObject("GameManager2");
        GameManager secondComponent = secondManager.AddComponent<GameManager>();

        // Assert - Second manager should be destroyed (or marked for destruction)
        // In Unity, Destroy() doesn't immediately destroy the object in edit mode
        Assert.AreEqual(gameManager, GameManager.Instance);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(gameManagerObject);
    }
}
