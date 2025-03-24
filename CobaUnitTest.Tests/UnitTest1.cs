using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace CobaUnitTest.Tests;

public class RoboticArmControllerTests
{
    [TestCase(0)]
    [TestCase(45)]
    [TestCase(90)]
    [TestCase(180)]
    public void RotateTo_ValidAngle_UpdatesCurrentAngle(double angle)
    {
        var controller = new RoboticArmController();
        controller.RotateTo(angle);
        ClassicAssert.AreEqual(angle, controller.currentAngle);
        // Assert.That(Is.Equals(90, controller.currentAngle));
    }

    [Test]
    public void IsAtHomePosition_WhenAngleIsZero_ReturnsTrue()
    {
        var controller = new RoboticArmController();
        // ClassicAssert.IsTrue(controller.IsAtHomePosition());
        Assert.That(controller.IsAtHomePosition(), Is.True);
    }

    [TestCase(-5)]
    [TestCase(183)]
    [TestCase(21312)]
    public void RotateTo_InvalidAngle_ThrowException(double angle)
    {
        var controller = new RoboticArmController();
        Assert.Throws<ArgumentOutOfRangeException>(() => controller.RotateTo(angle));
    }

    [TestCase(45)]
    [TestCase(90)]
    [TestCase(180)]
    public void IsAtHomePosition_WhenAngleIsNotZero_ReturnsFalse(double angle)
    {
        var controller = new RoboticArmController();
        controller.RotateTo(angle);
        // ClassicAssert.IsFalse(controller.IsAtHomePosition());
        Assert.That(controller.IsAtHomePosition(), Is.False);
    }   
}