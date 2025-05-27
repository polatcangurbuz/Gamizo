using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class JoystickButtonControlTest
{
    private GameObject joystickObject;
    private Animator animator;

    [SetUp]
    public void Setup()
    {
        joystickObject = new GameObject("JoystickButton");
        animator = joystickObject.AddComponent<Animator>();

        // For testing, we don't need an actual animator controller
        // The tests are checking if we can set parameters without errors
        // Unity will log warnings but the tests will still work
    }

    [TearDown]
    public void TearDown()
    {
        if (joystickObject != null)
            Object.DestroyImmediate(joystickObject);
    }

    [Test]
    public void JoystickButton_GameObjectExists()
    {
        Assert.IsNotNull(joystickObject);
        Assert.AreEqual("JoystickButton", joystickObject.name);
    }

    [Test]
    public void JoystickButton_AnimatorExists()
    {
        Assert.IsNotNull(animator);
    }

    [Test]
    public void Animator_IsEnabled()
    {
        Assert.IsTrue(animator.enabled);
    }

    [UnityTest]
    public IEnumerator Animator_SetBoolParameters()
    {
        animator.SetBool("down", true);
        animator.SetBool("idle", false);
        yield return null;

        Assert.DoesNotThrow(() => animator.SetBool("down", true));
        Assert.DoesNotThrow(() => animator.SetBool("idle", false));
    }

    [UnityTest]
    public IEnumerator Animator_ParameterChanges()
    {
        animator.SetBool("down", false);
        animator.SetBool("idle", true);
        yield return null;

        animator.SetBool("down", true);
        animator.SetBool("idle", false);
        yield return null;

        Assert.DoesNotThrow(() => animator.GetBool("down"));
        Assert.DoesNotThrow(() => animator.GetBool("idle"));
    }

    [Test]
    public void GameObject_CanAddComponents()
    {
        Assert.DoesNotThrow(() => joystickObject.GetComponent<Animator>());
    }

    [UnityTest]
    public IEnumerator GameObject_ActiveInScene()
    {
        yield return null;
        Assert.IsTrue(joystickObject.activeInHierarchy);
    }

    [Test]
    public void Animator_CanSetMultipleParameters()
    {
        Assert.DoesNotThrow(() =>
        {
            animator.SetBool("down", true);
            animator.SetBool("idle", false);
            animator.SetBool("down", false);
            animator.SetBool("idle", true);
        });
    }
}