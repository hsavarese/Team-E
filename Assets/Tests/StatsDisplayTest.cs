using NUnit.Framework;
using UnityEngine;

public class StatsDisplayTest
{
    [Test]
    public void StatsDisplay_WhenCreated_HasRequiredComponents()
    {
        // Arrange
        var gameObject = new GameObject();
        var statsDisplay = gameObject.AddComponent<StatsDisplay>();

        // Assert
        Assert.IsNotNull(statsDisplay);
        Assert.That(statsDisplay, Is.InstanceOf<MonoBehaviour>());
    }
} 