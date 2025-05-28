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
    public void GameObject_BasariliOlusturulduMu()
    {
        Assert.IsNotNull(adsManagerObject);
        Assert.AreEqual("GoogleAdsManager", adsManagerObject.name);
    }

    [Test]
    public void GameObject_AdiDogruMu()
    {
        Assert.AreEqual("GoogleAdsManager", adsManagerObject.name);
    }

    [Test]
    public void GameObject_AktifMi()
    {
        Assert.IsTrue(adsManagerObject.activeInHierarchy);
    }

    [UnityTest]
    public IEnumerator GameObject_SahnedeVarMi()
    {
        yield return null;
        Assert.IsNotNull(GameObject.Find("GoogleAdsManager"));
    }

    [Test]
    public void GameObject_YokEdilebilirMi()
    {
        Assert.DoesNotThrow(() => Object.DestroyImmediate(adsManagerObject));
    }
}