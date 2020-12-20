using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ois.Data;

namespace Ois.Utils.Tests
{
    [TestClass]
    public class PointExtensionTests
    {
        [TestMethod]
        public void PointCoincides_ShouldHandleNull()
        {
            // Arrange
            var point = new Point { X = 0, Y = 0 };

            // Act
            var result = point.Coincides(null);

            // Assert
            Assert.IsFalse(result, "Точка совпала со значением null");
        }

        [TestMethod]
        public void PointCopy_ShouldCreateNewPointInstance()
        {
            // Arrange
            var point = new Point { X = 0, Y = 0 };

            // Act
            var newPoint = point.Copy();

            // Assert
            Assert.AreNotSame(point, newPoint, "Метод создания копии точки вернул тот же объект");
        }
    }
}
