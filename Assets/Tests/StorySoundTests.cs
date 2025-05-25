using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Reflection;

public class StorySoundTests
{
    private GameObject testObject;
    private StorySound storySound;
    private AudioSource audioSource;
    private List<AudioClip> testClips;

    [SetUp]
    public void Setup()
    {
        // Test için gerekli nesneleri oluştur
        testObject = new GameObject("TestObject");
        storySound = testObject.AddComponent<StorySound>();
        audioSource = testObject.AddComponent<AudioSource>();
        
        // Test için örnek ses klipleri oluştur
        testClips = new List<AudioClip>();
        for (int i = 0; i < 3; i++)
        {
            AudioClip clip = AudioClip.Create("TestClip" + i, 44100, 1, 44100, false);
            testClips.Add(clip);
        }

        // StorySound bileşenine gerekli referansları ata
        var storyClipField = typeof(StorySound).GetField("storyClip", BindingFlags.NonPublic | BindingFlags.Instance);
        var audioSourceField = typeof(StorySound).GetField("audioSource", BindingFlags.NonPublic | BindingFlags.Instance);

        if (storyClipField != null)
        {
            storyClipField.SetValue(storySound, testClips);
        }
        else
        {
            Debug.LogError("storyClip field not found!");
        }

        if (audioSourceField != null)
        {
            audioSourceField.SetValue(storySound, audioSource);
        }
        else
        {
            Debug.LogError("audioSource field not found!");
        }
    }

    [TearDown]
    public void TearDown()
    {
        if (testObject != null)
        {
            Object.Destroy(testObject);
        }
        
        if (testClips != null)
        {
            foreach (var clip in testClips)
            {
                if (clip != null)
                {
                    Object.Destroy(clip);
                }
            }
        }
    }

    [Test]
    public void MusicPlayFunction_ValidIndex_PlaysCorrectClip()
    {
        // Arrange
        int testIndex = 1;

        // Act
        storySound.MusicPlayFunction(testIndex);

        // Assert
        Assert.AreEqual(testClips[testIndex], audioSource.clip);
        Assert.IsTrue(audioSource.isPlaying);
    }

    [Test]
    public void MusicPlayFunction_InvalidIndex_DoesNotThrowException()
    {
        // Arrange
        int invalidIndex = 999;

        // Act & Assert
        Assert.DoesNotThrow(() => storySound.MusicPlayFunction(invalidIndex));
    }

    // A Test behaves as an ordinary method
    [Test]
    public void StorySoundTestsSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator StorySoundTestsWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
