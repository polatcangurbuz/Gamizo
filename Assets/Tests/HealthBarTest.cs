using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class HealthBarTest
{
    private GameObject healthBarObject;
    private GameObject characterHealthObject;
    private Image healthImage;

    [SetUp]
    public void Setup()
    {
        characterHealthObject = new GameObject("CharacterHealth");

        healthBarObject = new GameObject("HealthBar");
        healthImage = healthBarObject.AddComponent<Image>();
    }

    [TearDown]
    public void TearDown()
    {
        if (healthBarObject != null)
            Object.DestroyImmediate(healthBarObject);
        if (characterHealthObject != null)
            Object.DestroyImmediate(characterHealthObject);
    }

    [Test]
    public void HealthImage_ExistsInScene()
    {
        Assert.IsNotNull(healthImage);
    }

    [Test]
    public void HealthImage_InitialScaleIsCorrect()
    {
        Vector3 initialScale = new Vector3(1f, 1, 1);
        healthImage.transform.localScale = initialScale;
        Assert.AreEqual(initialScale, healthImage.transform.localScale);
    }

    [Test]
    public void HealthImage_ScaleChangesCorrectly()
    {
        Vector3 halfScale = new Vector3(0.5f, 1, 1);
        healthImage.transform.localScale = halfScale;
        Assert.AreEqual(halfScale, healthImage.transform.localScale);
    }

    [Test]
    public void HealthImage_ZeroScaleWorks()
    {
        Vector3 zeroScale = new Vector3(0f, 1, 1);
        healthImage.transform.localScale = zeroScale;
        Assert.AreEqual(zeroScale, healthImage.transform.localScale);
    }
}