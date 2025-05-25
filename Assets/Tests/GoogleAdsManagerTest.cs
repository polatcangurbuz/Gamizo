using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GoogleAdsManagerTest
{
    private GameObject adsManagerObject;

    [SetUp]
    public void Setup()
    {
        adsManagerObject = new GameObject("GoogleAdsManager");
    }

    [TearDown]
    public void TearDown()
    {
        if (adsManagerObject != null)
            Object.DestroyImmediate(adsManagerObject);
    }

    [Test]
    public void GameObject_CreatesSuccessfully()
    {
        Assert.IsNotNull(adsManagerObject);
        Assert.AreEqual("GoogleAdsManager", adsManagerObject.name);
    }

    [Test]
    public void GameObject_HasCorrectName()
    {
        Assert.AreEqual("GoogleAdsManager", adsManagerObject.name);
    }

    [Test]
    public void GameObject_IsActive()
    {
        Assert.IsTrue(adsManagerObject.activeInHierarchy);
    }

    [UnityTest]
    public IEnumerator GameObject_ExistsInScene()
    {
        yield return null;
        Assert.IsNotNull(GameObject.Find("GoogleAdsManager"));
    }

    [Test]
    public void GameObject_CanBeDestroyed()
    {
        Assert.DoesNotThrow(() => Object.DestroyImmediate(adsManagerObject));
    }
}