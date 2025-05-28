using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using NUnit.Framework;

public class HealthBarTest
{
    private GameObject healthBarObject;
    private HealthBar healthBar;
    private Image healthImage;
    private GameObject characterHealthObject;
    private characterHealth characterHealthInstance;

    [UnitySetUp]
    public IEnumerator SetUp()
    {

        characterHealthObject = new GameObject("CharacterHealth");
        characterHealthInstance = characterHealthObject.AddComponent<characterHealth>();

        healthBarObject = new GameObject("HealthBar");


        GameObject canvasObject = new GameObject("Canvas");
        Canvas canvas = canvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasObject.AddComponent<CanvasScaler>();
        canvasObject.AddComponent<GraphicRaycaster>();

        healthBarObject.transform.SetParent(canvasObject.transform);

        GameObject healthImageObject = new GameObject("HealthImage");
        healthImageObject.transform.SetParent(healthBarObject.transform);
        healthImage = healthImageObject.AddComponent<Image>();

        healthBar = healthBarObject.AddComponent<HealthBar>();

        var healthImageField = typeof(HealthBar).GetField("healthImage",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        healthImageField.SetValue(healthBar, healthImage);

        yield return null; 
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        if (healthBarObject != null)
            Object.DestroyImmediate(healthBarObject.transform.root.gameObject);
        if (characterHealthObject != null)
            Object.DestroyImmediate(characterHealthObject);

        yield return null;
    }

    [UnityTest]
    public IEnumerator HealthBar_DogruBaslat()
    {
        // Arrange & Act - Start method otomatik çalýþýr
        yield return null;

        // Assert
        Assert.IsNotNull(healthBar);
        Assert.IsNotNull(healthImage);

        // Baþlangýç saðlýk deðeri ile scale kontrolü
        Vector3 expectedScale = new Vector3(characterHealthInstance.Health / 100f, 1, 1);
        Assert.AreEqual(expectedScale, healthImage.transform.localScale);
    }

    [UnityTest]
    public IEnumerator HealthBar_CanGidinceUpdate()
    {
        yield return null; // Setup tamamlansýn

        // Arrange
        int newHealth = 75;
        Vector3 expectedScale = new Vector3(newHealth / 100f, 1, 1);

        // Act
        characterHealthInstance.Health = newHealth; 
        yield return null;

        // Assert
        Assert.AreEqual(expectedScale, healthImage.transform.localScale);
    }

    [UnityTest]
    public IEnumerator HealthBar_CanSifirken_Update()
    {
        yield return null;

        // Arrange
        int newHealth = 0;
        Vector3 expectedScale = new Vector3(0f, 1, 1);

        // Act
        characterHealthInstance.Health = newHealth;
        yield return null;

        // Assert
        Assert.AreEqual(expectedScale, healthImage.transform.localScale);
    }

    [UnityTest]
    public IEnumerator HealthBar_CanFull_Update()
    {
        yield return null;

        // Arrange
        int newHealth = 100;
        Vector3 expectedScale = new Vector3(1f, 1, 1);

        // Act
        characterHealthInstance.Health = newHealth;
        yield return null;

        // Assert
        Assert.AreEqual(expectedScale, healthImage.transform.localScale);
    }

    [UnityTest]
    public IEnumerator HealthBar_BirdenFazlaCanDegisikligi()
    {
        yield return null;

        // Arrange & Act - Birden fazla saðlýk deðiþikliði
        int[] healthValues = { 90, 50, 25, 75, 10 };

        foreach (int health in healthValues)
        {
            characterHealthInstance.Health = health;
            yield return null;

            // Assert her deðiþiklik için
            Vector3 expectedScale = new Vector3(health / 100f, 1, 1);
            Assert.AreEqual(expectedScale, healthImage.transform.localScale,
                $"Health {health} için scale doðru deðil");
        }
    }

    [UnityTest]
    public IEnumerator HealthBar_DestroySirasinda()
    {
        yield return null;

        // Arrange
        bool eventFired = false;
        characterHealthInstance.OnHealthChanged += (health) => eventFired = true;

        // Act - HealthBar'ý yok et
        Object.DestroyImmediate(healthBarObject);
        yield return null;
        // Saðlýk deðiþtir
        characterHealthInstance.Health = 50;
        yield return null;

        Assert.IsTrue(eventFired); 

        LogAssert.NoUnexpectedReceived();
    }
}




