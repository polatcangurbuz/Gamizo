using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;
using TMPro;

public class GameStateTests
{
    private GameObject gameStateObject;
    private GameState gameState;
    private GameObject gameOverCanvas;
    private GameObject gameWinCanvas;
    private GameObject gameCanvas;
    private GameObject storyPanel;
    private TextMeshProUGUI countdownText;

    [SetUp]
    public void SetUp()
    {
        gameStateObject = new GameObject("GameState");
        gameState = gameStateObject.AddComponent<GameState>();

        // Create UI elements
        gameOverCanvas = new GameObject("GameOverCanvas");
        gameWinCanvas = new GameObject("GameWinCanvas");
        gameCanvas = new GameObject("GameCanvas");
        storyPanel = new GameObject("StoryPanel");

        GameObject textObject = new GameObject("CountdownText");
        countdownText = textObject.AddComponent<TextMeshProUGUI>();

        // Create audio sources
        GameObject musicObject = new GameObject("Music");
        AudioSource musicSource = musicObject.AddComponent<AudioSource>();

        GameObject audioObject = new GameObject("Audio");
        AudioSource audioSource = audioObject.AddComponent<AudioSource>();

        // Set private fields using reflection
        SetPrivateField("gameOverCanvas", gameOverCanvas);
        SetPrivateField("gameWinCanvas", gameWinCanvas);
        SetPrivateField("gameCanvas", gameCanvas);
        SetPrivateField("storyPanel", storyPanel);
        SetPrivateField("countdownText", countdownText);
        SetPrivateField("music", musicSource);
        SetPrivateField("audioSource", audioSource);
        SetPrivateField("countdown", 120f);

        // Reset time scale
        Time.timeScale = 1f;

        // Create character health instance
        GameObject healthObject = new GameObject("CharacterHealth");
        healthObject.AddComponent<characterHealth>();
    }

    private void SetPrivateField(string fieldName, object value)
    {
        var field = typeof(GameState).GetField(fieldName,
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        field?.SetValue(gameState, value);
    }

    [Test]
    public void Update_CanSifir_GameOverAktifMi()
    {
        // Arrange
        characterHealth.Instance.Health = 0;
        gameOverCanvas.SetActive(false);

        // Act
        gameState.SendMessage("Update");

        // Assert
        Assert.IsTrue(gameOverCanvas.activeInHierarchy);
        Assert.AreEqual(0f, Time.timeScale);
    }

    [Test]
    public void Update_SureSifir_GameWinAktifMi()
    {
        // Arrange
        SetPrivateField("countdown", 0f);
        gameWinCanvas.SetActive(false);

        // Act
        gameState.SendMessage("Update");

        // Assert
        Assert.IsTrue(gameWinCanvas.activeInHierarchy);
        Assert.AreEqual(0f, Time.timeScale);
    }

    [Test]
    public void CountdownText_FormatDogruMu_60SaniyeAltinda()
    {
        // Arrange
        SetPrivateField("countdown", 90f);
        gameCanvas.SetActive(true);
        storyPanel.SetActive(false);
        SetPrivateField("isPlaying", true);

        // Act
        gameState.SendMessage("Update");

        // Assert
        Assert.AreEqual("1:29", countdownText.text);
    }

    [Test]
    public void CountdownText_FormatDogruMu_60SaniyeUstunde()
    {
        // Arrange
        SetPrivateField("countdown", 30f);
        gameCanvas.SetActive(true);
        storyPanel.SetActive(false);
        SetPrivateField("isPlaying", true);

        // Act
        gameState.SendMessage("Update");

        // Assert
        Assert.AreEqual("29", countdownText.text);
    }

    [UnityTest]
    public IEnumerator Update_SureAzaliyorMu()
    {
        // Arrange
        SetPrivateField("countdown", 120f);
        gameCanvas.SetActive(true);
        storyPanel.SetActive(false);
        SetPrivateField("isPlaying", true);

        // Act
        gameState.SendMessage("Update");
        yield return new WaitForSeconds(0.1f);
        gameState.SendMessage("Update");

        // Assert
        var countdownField = typeof(GameState).GetField("countdown",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        float currentCountdown = (float)countdownField.GetValue(gameState);
        Assert.Less(currentCountdown, 120f);
    }

    [TearDown]
    public void TearDown()
    {
        Time.timeScale = 1f;
        Object.DestroyImmediate(gameStateObject);
        Object.DestroyImmediate(gameOverCanvas);
        Object.DestroyImmediate(gameWinCanvas);
        Object.DestroyImmediate(gameCanvas);
        Object.DestroyImmediate(storyPanel);
        Object.DestroyImmediate(countdownText?.gameObject);

        // Clean up character health instance
        if (characterHealth.Instance != null)
            Object.DestroyImmediate(characterHealth.Instance.gameObject);
    }
}