using NUnit.Framework;
using UnityEngine;

public class characterHealthTests
{
    private GameObject healthObject;
    private characterHealth healthComponent;

    [SetUp]
    public void SetUp()
    {
        healthObject = new GameObject("CharacterHealth");
        healthComponent = healthObject.AddComponent<characterHealth>();
    }

    [Test]
    public void Instance_IsSingleton()
    {
        // Assert
        Assert.IsNotNull(characterHealth.Instance);
        Assert.AreSame(healthComponent, characterHealth.Instance, "Instance should be the same object reference");
    }

    [Test]
    public void Health_InitialValue_Is100()
    {
        // Assert
        Assert.AreEqual(100, healthComponent.Health);
    }

    [Test]
    public void Health_SetValue_ClampsToValidRange()
    {
        // Test upper bound
        healthComponent.Health = 150;
        Assert.AreEqual(100, healthComponent.Health);

        // Test lower bound
        healthComponent.Health = -50;
        Assert.AreEqual(0, healthComponent.Health);

        // Test valid value
        healthComponent.Health = 75;
        Assert.AreEqual(75, healthComponent.Health);
    }

    [Test]
    public void Health_OnHealthChanged_EventTriggered()
    {
        // Arrange
        bool eventTriggered = false;
        int receivedHealth = -1;

        healthComponent.OnHealthChanged += (health) => {
            eventTriggered = true;
            receivedHealth = health;
        };

        // Act
        healthComponent.Health = 50;

        // Assert
        Assert.IsTrue(eventTriggered);
        Assert.AreEqual(50, receivedHealth);
    }

    [Test]
    public void Health_MultipleSubscribers_AllReceiveEvent()
    {
        // Arrange
        int subscriber1Health = -1;
        int subscriber2Health = -1;

        healthComponent.OnHealthChanged += (health) => subscriber1Health = health;
        healthComponent.OnHealthChanged += (health) => subscriber2Health = health;

        // Act
        healthComponent.Health = 25;

        // Assert
        Assert.AreEqual(25, subscriber1Health);
        Assert.AreEqual(25, subscriber2Health);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(healthObject);
    }
}