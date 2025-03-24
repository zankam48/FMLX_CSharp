using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace CobaUnitTest.Tests;

public class RoboticArmControllerTests
{
    [Test]
    public void RotateTo_ValidAngle_UpdatesCurrentAngle()
    {
        var controller = new RoboticArmController();
        controller.RotateTo(90);
        ClassicAssert.AreEqual(90, controller.currentAngle);
        // Assert.That(Is.Equals(90, controller.currentAngle));
    }

    [Test]
    public void IsAtHomePosition_WhenAngleIsZero_ReturnsTrue()
    {
        var controller = new RoboticArmController();
        // ClassicAssert.IsTrue(controller.IsAtHomePosition());
        Assert.That(controller.IsAtHomePosition(), Is.True);
    }

    [Test]
    public void IsAtHomePosition_WhenAngleIsNotZero_ReturnsFalse()
    {
        var controller = new RoboticArmController();
        controller.RotateTo(45);
        // ClassicAssert.IsFalse(controller.IsAtHomePosition());
        Assert.That(controller.IsAtHomePosition(), Is.False);
    }   
}