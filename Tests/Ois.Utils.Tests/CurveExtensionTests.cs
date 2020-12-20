using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ois.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ois.Utils.Tests
{
    [TestClass]
    public class CurveExtensionTests
    {
        [TestMethod]
        public void CurveAsSegments_MustContainsAtLeast1Points()
        {
            // Arrange
            var curve = new Curve { Name = "", Points = new List<Point>() };

            // Act
            var exceptionWasThrown = false;
            try
            {
                var segments = curve.AsSegments().ToArray();
            }
            catch (ArgumentException)
            {
                exceptionWasThrown = true;
            }

            // Assert
            Assert.IsTrue(exceptionWasThrown, "Кривая не содержит точек, но была разделена на отрезки");
        }

        [TestMethod]
        public void CurveAsSegments_ShouldHandleCurveWithSinglePoint()
        {
            // Arrange
            var point = new Point { X = 0, Y = 0 };
            var curve = new Curve { Name = "", Points = new List<Point> { point } };

            // Act
            var segments = curve.AsSegments().ToArray();
            var firstSegment = segments.FirstOrDefault();
            var zeroLength = point.Coincides(firstSegment?.Start) && point.Coincides(firstSegment?.End);

            // Assert
            Assert.AreEqual(1, segments.Length, "Получен не единственный отрезок");
            Assert.IsTrue(zeroLength, "Получен отрезок не нулевой длины");
        }
    }
}
