using UnityEngine;
using NUnit.Framework;

public class TestBase
{
    protected GameObject audioListenerObject;

    [SetUp]
    public virtual void BaseSetUp()
    {
        // Create audio listener if one doesn't exist
        if (Object.FindObjectOfType<AudioListener>() == null)
        {
            audioListenerObject = new GameObject("AudioListener");
            audioListenerObject.AddComponent<AudioListener>();
        }
    }

    [TearDown]
    public virtual void BaseTearDown()
    {
        if (audioListenerObject != null)
        {
            Object.DestroyImmediate(audioListenerObject);
        }
    }
}
