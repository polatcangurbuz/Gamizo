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
    }

    [TearDown]
    public void TearDown()
    {
        if (joystickObject != null)
            Object.DestroyImmediate(joystickObject);
    }

    [Test]
    public void JoystickButon_GameObjectVarMi()
    {
        Assert.IsNotNull(joystickObject);
        Assert.AreEqual("JoystickButton", joystickObject.name);
    }

    [Test]
    public void JoystickButon_AnimatorVarMi()
    {
        Assert.IsNotNull(animator);
    }

    [Test]
    public void Animator_AktifMi()
    {
        Assert.IsTrue(animator.enabled);
    }

    [UnityTest]
    public IEnumerator Animator_BoolParametreleriAyarla()
    {
        animator.SetBool("down", true);
        animator.SetBool("idle", false);
        yield return null;

        Assert.DoesNotThrow(() => animator.SetBool("down", true));
        Assert.DoesNotThrow(() => animator.SetBool("idle", false));
    }

    [UnityTest]
    public IEnumerator Animator_ParametreDegisiklikleri()
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
    public void GameObject_BilesenEklenebilirMi()
    {
        Assert.DoesNotThrow(() => joystickObject.GetComponent<Animator>());
    }

    [UnityTest]
    public IEnumerator GameObject_SahnedeAktifMi()
    {
        yield return null;
        Assert.IsTrue(joystickObject.activeInHierarchy);
    }

    [Test]
    public void Animator_BirdenFazlaParametreAyarla()
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