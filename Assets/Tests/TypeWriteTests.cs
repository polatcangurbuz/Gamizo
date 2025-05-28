using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;
using UnityEngine.UI;

public class TypeWriteTests
{
    private GameObject typeWriteObject;
    private TypeWrite typeWrite;
    private GameObject storyObject;
    private StorySound storySound;

    [SetUp]
    public void Setup()
    {
        typeWriteObject = new GameObject();
        typeWrite = typeWriteObject.AddComponent<TypeWrite>();
        storySound = typeWriteObject.AddComponent<StorySound>();
        
        storyObject = new GameObject();
        storyObject.AddComponent<TextMesh>();
        storyObject.SetActive(false);
        
        typeWrite.StoryList = new System.Collections.Generic.List<GameObject> { storyObject };
    }

    [TearDown]
    public void TearDown()
    {
        Object.Destroy(typeWriteObject);
        Object.Destroy(storyObject);
    }

    [Test]
    public void TypeWrite_IlkDurum_DogruMu()
    {
        Assert.IsNotNull(typeWrite);
        Assert.AreEqual(0.05f, typeWrite.typingSpeed);
        Assert.IsFalse(typeWrite.isStoryFinished);
    }

    [Test]
    public void TypeWrite_HikayeListesi_BaslatildiMi()
    {
        Assert.IsNotNull(typeWrite.StoryList);
        Assert.AreEqual(1, typeWrite.StoryList.Count);
    }

    [Test]
    public void TypeWrite_TekilNesne_AyarlandiMi()
    {
        Assert.IsNotNull(TypeWrite.Instance);
        Assert.AreEqual(typeWrite, TypeWrite.Instance);
    }
} 