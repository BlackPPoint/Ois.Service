using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ois.Data;

namespace Ois.Utils.Tests
{
    [TestClass]
    public class SegmentExtensionTests
    {
        [TestMethod]
        public void SegmentGetSlope_ShouldReturnInfinityWhenParallelToY()
        {
            // Arrange
            var constX = 42;
            var segment = new Segment { Start = new Point { X = constX, Y = 0 }, End = new Point { X = constX, Y = 42 } };

            // Act
            var slope = segment.GetSlope();

            // Assert
            Assert.AreEqual(double.PositiveInfinity, slope, "Отрезки паралельные оси ординат должны иметь угловой коэффициент +∞");
        }

        [TestMethod]
        public void SegmentRevert_ShouldCreateNewSegmentInstance()
        {
            // Arrange
            var segment = new Segment { Start = new Point { X = 0, Y = 0 }, End = new Point { X = 1, Y = 1 } };

            // Act
            var newSegment = segment.Revert();

            // Assert
            Assert.AreNotSame(segment, newSegment, "Метод получения обрабтного отрезка вернул тот же объект");
        }

        [TestMethod]
        public void SegmentIntersect_ShouldHandleSegmentsDirections()
        {
            // Arrange
            var segmentA = new Segment { Start = new Point { X = 1, Y = 1 }, End = new Point { X = 3, Y = 3 } };
            var segmentB = new Segment { Start = new Point { X = 1, Y = 3 }, End = new Point { X = 3, Y = 1 } };
            var segmentARev = segmentA.Revert();
            var segmentBRev = segmentB.Revert();

            // Act
            var interception1 = segmentA.Intersect(segmentB);
            var interception2 = segmentA.Intersect(segmentBRev);
            var interception3 = segmentARev.Intersect(segmentB);
            var interception4 = segmentARev.Intersect(segmentBRev);
            var samePoints = interception1.Coincides(interception2) && interception2.Coincides(interception3) && interception3.Coincides(interception4);

            // Assert
            Assert.IsTrue(samePoints, "Точки пересчечения одинаковых отрезков не совпадают");
        }

        [TestMethod]
        public void SegmentIntersect_ShouldCountSegmentEnds()
        {
            // Arrange
            var segmentA = new Segment { Start = new Point { X = 1, Y = 1 }, End = new Point { X = 3, Y = 3 } };
            var segmentB = new Segment { Start = new Point { X = 2, Y = 2 }, End = new Point { X = 3, Y = 1 } };

            // Act
            var interception = segmentA.Intersect(segmentB);
            var samePoint = segmentB.Start.Coincides(interception);

            // Assert
            Assert.IsNotNull(interception, "Точка пересчечения не найдена");
            Assert.IsTrue(samePoint, "Точка пересечения не совпадает с началом отрезка");
        }

        [TestMethod]
        public void SegmentIntersect_ShouldHandleSegmentsParallelToX()
        {
            // Arrange
            var segmentA = new Segment { Start = new Point { X = 1, Y = 1 }, End = new Point { X = 3, Y = 3 } };
            var segmentB = new Segment { Start = new Point { X = 1, Y = 2 }, End = new Point { X = 3, Y = 2 } };

            // Act
            var interception = segmentA.Intersect(segmentB);
            var checkPoint = interception?.X == 2 && interception?.Y == 2;

            // Assert
            Assert.IsNotNull(interception, "Точка пересчечения не найдена");
            Assert.IsTrue(checkPoint, "Точка пересечения определена неверно");
        }

        [TestMethod]
        public void SegmentIntersect_ShouldHandleSegmentsParallelToY()
        {
            // Arrange
            var segmentA = new Segment { Start = new Point { X = 1, Y = 1 }, End = new Point { X = 3, Y = 3 } };
            var segmentB = new Segment { Start = new Point { X = 2, Y = 1 }, End = new Point { X = 2, Y = 3 } };

            // Act
            var interception = segmentA.Intersect(segmentB);
            var checkPoint = interception?.X == 2 && interception?.Y == 2;

            // Assert
            Assert.IsNotNull(interception, "Точка пересчечения не найдена");
            Assert.IsTrue(checkPoint, "Точка пересечения определена неверно");
        }

        [TestMethod]
        public void SegmentIntersect_ShouldHandleParallelSegments()
        {
            // Arrange
            var segmentA = new Segment { Start = new Point { X = 1, Y = 1 }, End = new Point { X = 3, Y = 3 } };
            var segmentB = new Segment { Start = new Point { X = 2, Y = 1 }, End = new Point { X = 3, Y = 2 } };

            // Act
            var interception = segmentA.Intersect(segmentB);

            // Assert
            Assert.IsNull(interception, "Обнаружено пересечение параллельных отрезков");
        }

        [TestMethod]
        public void SegmentIntersect_ShouldLayOnBothSegments()
        {
            // Arrange
            var segmentA = new Segment { Start = new Point { X = 1, Y = 1 }, End = new Point { X = 5, Y = 5 } };
            var segmentB = new Segment { Start = new Point { X = 4, Y = 2 }, End = new Point { X = 5, Y = 1 } };

            // Act
            // пересечение прямых, на которых лежат отрезки будет в точке (3, 3), но она не лежит на отрезке segmentB
            var interception = segmentA.Intersect(segmentB);

            // Assert
            Assert.IsNull(interception, "Обнаружено пересечение параллельных отрезков");
        }

        [TestMethod]
        public void SegmentIntersect_ShouldHandleZeroLengthSegment()
        {
            // Arrange
            var segmentA = new Segment { Start = new Point { X = 1, Y = 1 }, End = new Point { X = 3, Y = 3 } };
            var segmentB = new Segment { Start = new Point { X = 2, Y = 2 }, End = new Point { X = 2, Y = 2 } };

            // Act
            var interception = segmentA.Intersect(segmentB);
            var checkPoint = interception?.X == 2 && interception?.Y == 2;

            // Assert
            Assert.IsNotNull(interception, "Точка пересчечения не найдена");
            Assert.IsTrue(checkPoint, "Точка пересечения определена неверно");
        }
    }
}
