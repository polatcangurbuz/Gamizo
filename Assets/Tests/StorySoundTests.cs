using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Reflection;

public class StorySoundTests : TestBase
{
    private GameObject storySoundObject;
    private StorySound storySound;
    private AudioSource audioSource;
    private List<AudioClip> testClips;

    [SetUp]
    public void Setup()
    {
        base.BaseSetUp();


        storySoundObject = new GameObject();
        storySound = storySoundObject.AddComponent<StorySound>();
        audioSource = storySoundObject.AddComponent<AudioSource>();
        
        testClips = new List<AudioClip>();
        for (int i = 0; i < 3; i++)
        {
            AudioClip clip = AudioClip.Create("TestClip" + i, 44100, 1, 44100, false);
            testClips.Add(clip);
        }

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
        base.BaseTearDown();
        
        if (storySoundObject != null)
            Object.DestroyImmediate(storySoundObject);
            
        if (testClips != null)
        {
            foreach (var clip in testClips)
            {
                if (clip != null)
                    Object.DestroyImmediate(clip);
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
        int negativeIndex = -1;

        // Act & Assert
        Assert.DoesNotThrow(() => storySound.MusicPlayFunction(negativeIndex));
    }

    [Test]
    public void StorySound_MusicPlayFunction_HandlesZeroIndex()
    {
        int zeroIndex = 0;

        // Act & Assert
        Assert.DoesNotThrow(() => storySound.MusicPlayFunction(zeroIndex));
    }

    [Test]
    public void StorySound_MusicPlayFunction_HandlesLargeIndex()
    {
        int largeIndex = 999;

        // Act & Assert
        Assert.DoesNotThrow(() => storySound.MusicPlayFunction(largeIndex));
    }

 
   
}
