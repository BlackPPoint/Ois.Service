using Ois.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ois.Utils
{
    /// <summary>
    /// Методы расширения для <see cref="Curve"/>
    /// </summary>
    public static class CurveExtension
    {
        /// <summary>
        /// Находит все пересечения двух кривых
        /// </summary>
        /// <param name="originCurve">Кривая, относительно которой вызван метод</param>
        /// <param name="curve">Пересекаемая кривая</param>
        /// <returns>Список точек пересечения кривых</returns>
        public static IEnumerable<Point> FindIntersections(this Curve originCurve, Curve curve)
        {
            // разделяем кривые на отрезки и ищем все их пересечения
            Point prevInterception = null;
            var segments = curve.AsSegments().ToList();
            foreach (var originSegment in originCurve.AsSegments())
                foreach (var segment in segments)
                {
                    var intersection = originSegment.Intersect(segment);
                    if (intersection != null && !intersection.Coincides(prevInterception))
                    {
                        yield return intersection;
                        prevInterception = intersection;
                    }
                };
        }

        /// <summary>
        /// Представляет кривую, как последовательность отрезков
        /// </summary>
        /// <param name="curve">Исходная кривая</param>
        /// <exception cref="ArgumentException">Кривая <paramref name="curve"/> содержит менее 2 точек</exception>
        public static IEnumerable<Segment> AsSegments(this Curve curve)
        {
            if (curve.Points.Count == 0)
                throw new ArgumentException(nameof(curve), "Кривая должна содержать как минимум 1 точку!");

            if (curve.Points.Count == 1)
                // возвращаем отрезок нулевой длины
                yield return new Segment { Start = curve.Points[0], End = new Point { X = curve.Points[0].X, Y = curve.Points[0].Y } };
            else
            {
                // сортируем точки относительно оси X и возвращаем получившееся отрезки
                var sortedPoints = curve.Points.OrderBy(p => p.X).ToList();
                for (int i = 0; i < sortedPoints.Count - 1; i++)
                    yield return new Segment { Start = sortedPoints[i], End = sortedPoints[i + 1] };
            }
        }
    }
}
