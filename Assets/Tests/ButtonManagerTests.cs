using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManagerTests
{
    private GameObject buttonManagerObject;
    private ButtonManager buttonManager;
    private GameObject canvasGroupObject;
    private GameObject gameCanvasObject;
    private GameObject optionsCanvasObject;

    [SetUp]
    public void SetUp()
    {
        buttonManagerObject = new GameObject("ButtonManager");
        buttonManager = buttonManagerObject.AddComponent<ButtonManager>();

        // Setup UI elements
        canvasGroupObject = new GameObject("CanvasGroup");
        CanvasGroup canvasGroup = canvasGroupObject.AddComponent<CanvasGroup>();

        gameCanvasObject = new GameObject("GameCanvas");
        optionsCanvasObject = new GameObject("OptionsCanvas");

        // Use reflection to set private fields
        var canvasGroupField = typeof(ButtonManager).GetField("canvasGroup",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        canvasGroupField?.SetValue(buttonManager, canvasGroup);

        var gameCanvasField = typeof(ButtonManager).GetField("gameCanvas",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        gameCanvasField?.SetValue(buttonManager, gameCanvasObject);

        var optionsCanvasField = typeof(ButtonManager).GetField("optionsCanvas",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        optionsCanvasField?.SetValue(buttonManager, optionsCanvasObject);
    }

    [Test]
    public void SecenekleriAc_SeceneklerCanvasiniAktiflestirir()
    {
        // Arrange
        optionsCanvasObject.SetActive(false);

        // Act
        buttonManager.OpenOptions();

        // Assert
        Assert.IsTrue(optionsCanvasObject.activeInHierarchy);
    }

    [Test]
    public void SecenekleriKapat_SeceneklerCanvasiniDevreDisiBirakir()
    {
        // Arrange
        optionsCanvasObject.SetActive(true);

        // Act
        buttonManager.CloseOptions();

        // Assert
        Assert.IsFalse(optionsCanvasObject.activeInHierarchy);
    }

    [Test]
    public void DusukButon_KalitesiniDusugeAyarlar()
    {
        // Act
        buttonManager.LowButton();

        // Assert
        Assert.AreEqual(0, QualitySettings.GetQualityLevel());
    }

    [Test]
    public void OrtaButon_KalitesiniOrtayaAyarlar()
    {
        // Act
        buttonManager.MediumButton();

        // Assert
        Assert.AreEqual(1, QualitySettings.GetQualityLevel());
    }

    [Test]
    public void YuksekButon_KalitesiniYuksegeAyarlar()
    {
        // Act
        buttonManager.HighButton();

        // Assert
        Assert.AreEqual(2, QualitySettings.GetQualityLevel());
    }

    [Test]
    public void MuzikSesiniAyarla_AudioSourceSesiniGunceller()
    {
        // Arrange
        GameObject audioObject = new GameObject("AudioSource");
        AudioSource audioSource = audioObject.AddComponent<AudioSource>();

        GameObject sliderObject = new GameObject("Slider");
        Slider slider = sliderObject.AddComponent<Slider>();
        slider.value = 0.5f;

        var audioSourceField = typeof(ButtonManager).GetField("audioSource",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        audioSourceField?.SetValue(buttonManager, audioSource);

        var musicSliderField = typeof(ButtonManager).GetField("musicSlider",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        musicSliderField?.SetValue(buttonManager, slider);

        // Act
        buttonManager.AdjustMusicVolume();

        // Assert
        Assert.AreEqual(0.5f, audioSource.volume, 0.01f);

        // Cleanup
        Object.DestroyImmediate(audioObject);
        Object.DestroyImmediate(sliderObject);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(buttonManagerObject);
        Object.DestroyImmediate(canvasGroupObject);
        Object.DestroyImmediate(gameCanvasObject);
        Object.DestroyImmediate(optionsCanvasObject);
    }
}