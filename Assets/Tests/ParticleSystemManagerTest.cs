using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ParticleSystemManagerTest
{
    private GameObject particleManagerObject;
    private GameObject electricityEffectObject;
    private ParticleSystem particleSystem;

    [SetUp]
    public void Setup()
    {
        electricityEffectObject = new GameObject("ElectricityEffect");
        particleSystem = electricityEffectObject.AddComponent<ParticleSystem>();

        particleManagerObject = new GameObject("ParticleSystemManager");
    }

    [TearDown]
    public void TearDown()
    {
        if (particleManagerObject != null)
            Object.DestroyImmediate(particleManagerObject);
        if (electricityEffectObject != null)
            Object.DestroyImmediate(electricityEffectObject);
    }

    [Test]
    public void ParticleSystemManager_GameObjectExists()
    {
        Assert.IsNotNull(particleManagerObject);
        Assert.AreEqual("ParticleSystemManager", particleManagerObject.name);
    }

    [Test]
    public void ElectricityEffect_GameObjectExists()
    {
        Assert.IsNotNull(electricityEffectObject);
        Assert.AreEqual("ElectricityEffect", electricityEffectObject.name);
    }

    [Test]
    public void ParticleSystem_ComponentExists()
    {
        Assert.IsNotNull(particleSystem);
    }

    [UnityTest]
    public IEnumerator ParticleSystem_CanPlay()
    {
        Assert.DoesNotThrow(() => particleSystem.Play());
        yield return null;
    }

    [UnityTest]
    public IEnumerator ParticleSystem_CanStop()
    {
        particleSystem.Play();
        yield return null;
        Assert.DoesNotThrow(() => particleSystem.Stop());
        yield return null;
    }

    [Test]
    public void ParticleSystem_HasMainModule()
    {
        var main = particleSystem.main;
        Assert.IsNotNull(main);
    }

    [UnityTest]
    public IEnumerator ParticleSystem_PlayAndCheckIsPlaying()
    {
        particleSystem.Play();
        yield return new WaitForSeconds(0.1f);
        Assert.IsTrue(particleSystem.isPlaying);
    }

    [Test]
    public void GameObject_CanGetParticleSystemComponent()
    {
        var ps = electricityEffectObject.GetComponent<ParticleSystem>();
        Assert.IsNotNull(ps);
        Assert.AreEqual(particleSystem, ps);
    }
}