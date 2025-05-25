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
        // Test için gerekli GameObject ve component'leri oluştur
        typeWriteObject = new GameObject();
        typeWrite = typeWriteObject.AddComponent<TypeWrite>();
        storySound = typeWriteObject.AddComponent<StorySound>();
        
        // Story objesi oluştur
        storyObject = new GameObject();
        storyObject.AddComponent<TextMesh>();
        storyObject.SetActive(false);
        
        // TypeWrite'a story listesini ekle
        typeWrite.StoryList = new System.Collections.Generic.List<GameObject> { storyObject };
    }

    [TearDown]
    public void TearDown()
    {
        // Test sonrası temizlik
        Object.Destroy(typeWriteObject);
        Object.Destroy(storyObject);
    }

    [Test]
    public void TypeWrite_InitialState_IsCorrect()
    {
        // Başlangıç durumunun doğruluğunu kontrol et
        Assert.IsNotNull(typeWrite);
        Assert.AreEqual(0.05f, typeWrite.typingSpeed);
        Assert.IsFalse(typeWrite.isStoryFinished);
    }

    [Test]
    public void TypeWrite_StoryList_IsInitialized()
    {
        // Story listesinin doğru şekilde başlatıldığını kontrol et
        Assert.IsNotNull(typeWrite.StoryList);
        Assert.AreEqual(1, typeWrite.StoryList.Count);
    }

    [Test]
    public void TypeWrite_Instance_IsSet()
    {
        // Singleton instance'ın doğru şekilde ayarlandığını kontrol et
        Assert.IsNotNull(TypeWrite.Instance);
        Assert.AreEqual(typeWrite, TypeWrite.Instance);
    }
} 