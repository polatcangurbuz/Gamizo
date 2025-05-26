using NUnit.Framework;
using UnityEngine;

public class GamePauseTests
{
    private GameObject gamePauseObject;
    private GamePause gamePause;

    [SetUp]
    public void SetUp()
    {
        gamePauseObject = new GameObject("GamePause");
        gamePause = gamePauseObject.AddComponent<GamePause>();

        // Reset time scale
        Time.timeScale = 1f;
    }

    [Test]
    public void PauseGame_SetsTimeScaleToZero()
    {
        // Act
        gamePause.PauseGame();

        // Assert
        Assert.AreEqual(0f, Time.timeScale);
    }

    [Test]
    public void ContinueGame_SetsTimeScaleToOne()
    {
        // Arrange
        Time.timeScale = 0f;

        // Act
        gamePause.ContinueGame();

        // Assert
        Assert.AreEqual(1f, Time.timeScale);
    }

    [Test]
    public void PauseAndContinue_RestoresTimeScale()
    {
        // Arrange
        float originalTimeScale = Time.timeScale;

        // Act
        gamePause.PauseGame();
        gamePause.ContinueGame();

        // Assert
        Assert.AreEqual(originalTimeScale, Time.timeScale);
    }

    [TearDown]
    public void TearDown()
    {
        Time.timeScale = 1f; // Reset time scale
        Object.DestroyImmediate(gamePauseObject);
    }
}