using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Reflection;

public class StorySoundTests
{
    private GameObject storySoundObject;
    private StorySound storySound;
    private AudioSource audioSource;
    private List<AudioClip> testClips;

    [SetUp]
    public void Setup()
    {
        // Test için gerekli GameObject ve component'leri oluştur
        storySoundObject = new GameObject();
        storySound = storySoundObject.AddComponent<StorySound>();
        audioSource = storySoundObject.AddComponent<AudioSource>();
        
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
        // Test sonrası temizlik
        Object.Destroy(storySoundObject);
        
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
    public void StorySound_InitialState_IsCorrect()
    {
        // Başlangıç durumunun doğruluğunu kontrol et
        Assert.IsNotNull(storySound);
    }

    [Test]
    public void StorySound_MusicPlayFunction_HandlesNegativeIndex()
    {
        // Negatif index değeri
        int negativeIndex = -1;

        // Act & Assert
        Assert.DoesNotThrow(() => storySound.MusicPlayFunction(negativeIndex));
    }

    [Test]
    public void StorySound_MusicPlayFunction_HandlesZeroIndex()
    {
        // Sıfır index değeri
        int zeroIndex = 0;

        // Act & Assert
        Assert.DoesNotThrow(() => storySound.MusicPlayFunction(zeroIndex));
    }

    [Test]
    public void StorySound_MusicPlayFunction_HandlesLargeIndex()
    {
        // Büyük index değeri
        int largeIndex = 999;

        // Act & Assert
        Assert.DoesNotThrow(() => storySound.MusicPlayFunction(largeIndex));
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
