using Ois.Data;
using System;

namespace Ois.Utils
{
    /// <summary>
    /// Методы расширения для <see cref="Segment"/>
    /// </summary>
    public static class SegmentExtension
    {
        /// <summary>
        /// Находит точку пересечения текущего отрезка с указанным
        /// </summary>
        /// <param name="segmentA">Текущий отрезок</param>
        /// <param name="segmentB"></param>
        /// <returns>Точка пересечения отрезков, либо <c>null</c>, если отрезки не пересекаются</returns>
        public static Point Intersect(this Segment segmentA, Segment segmentB)
        {
            // разворачиваем отрезки в направлении оси абсцисс
            if (segmentA.Start.X > segmentA.End.X) segmentA = segmentA.Revert();
            if (segmentB.Start.X > segmentB.End.X) segmentB = segmentB.Revert();

            // обрабатываем случае, когда отрезки лежат далеко друг от друга
            if (segmentA.Start.X > segmentB.End.X || segmentA.End.X < segmentB.Start.X)
                return null; // у отрезков нет точек с одинаковым значением X
            var minYA = Math.Min(segmentA.Start.Y, segmentA.End.Y);
            var maxYA = Math.Max(segmentA.Start.Y, segmentA.End.Y);
            var minYB = Math.Min(segmentB.Start.Y, segmentB.End.Y);
            var maxYB = Math.Max(segmentB.Start.Y, segmentB.End.Y);
            if (minYA > maxYB || maxYA < minYB)
                return null; // у отрезков нет точек с одинаковым значением Y

            // в общем виде, уравнение прямой: Y = slope * X + const
            // получаем угловые коэффициенты прямых, на которых лежат отрезки
            var slopeA = segmentA.GetSlope();
            var slopeB = segmentB.GetSlope();
            // находим свободные члены прямых, на которых лежат отрезки
            var constA = segmentA.Start.Y - slopeA * segmentA.Start.X;
            var constB = segmentB.Start.Y - slopeB * segmentB.Start.X;

            // обрабатываем случай, когда отрезки параллельны
            if (slopeA == slopeB)
            {
                if (slopeA == double.PositiveInfinity && segmentA.Start.X == segmentB.Start.X) // отрезки параллельны оси ординат и лежат на одной прямой
                {
                    if (segmentB.Start.Y >= segmentA.Start.Y) // начало отрезка B лежит на отрезке A
                        return segmentB.Start.Copy();
                    else
                        return segmentA.Start.Copy();
                }
                else if (slopeA != double.PositiveInfinity && constA == constB) // отрезки не параллельны оси ординат, но лежат на одной прямой
                {
                    if (segmentB.Start.X >= segmentA.Start.X) // начало отрезка B лежит на отрезке A
                        return segmentB.Start.Copy();
                    else
                        return segmentA.Start.Copy();
                }
                else
                    return null; // отрезки параллельны, но лежат на разных прямых
            }
            
            // находим точку пересечения полученных прямых
            var intersection = new Point();
            if (slopeA == double.PositiveInfinity) // отрезок А параллелен оси ординат (constA - бесконечность)
            {
                intersection.X = segmentA.Start.X;
                intersection.Y = slopeB * intersection.X + constB;
            }
            else if (slopeB == double.PositiveInfinity) // отрезок B параллелен оси ординат (constB - бесконечность)
            {
                intersection.X = segmentB.Start.X;
                intersection.Y = slopeA * intersection.X + constA;
            }
            else // общий случай (отрезки не параллельны оси ординат)
            {
                intersection.X = (constB - constA) / (slopeA - slopeB);
                intersection.Y = slopeA * intersection.X + constA;
            }

            // проверяем, что точка пересечения попала на оба отрезка и возвращаем результат
            var segmentAContainsIntersection = segmentA.Start.X <= intersection.X && intersection.X <= segmentA.End.X;
            var segmentBContainsIntersection = segmentB.Start.X <= intersection.X && intersection.X <= segmentB.End.X;
            return segmentAContainsIntersection && segmentBContainsIntersection ? intersection : null;
        }

        /// <summary>
        /// Создает новый отрезок с поменяными местами началом и концом
        /// </summary>
        public static Segment Revert(this Segment segment)
            => new Segment { Start = segment.End.Copy(), End = segment.Start.Copy() };

        /// <summary>
        /// Находит угловой коэффициент прямой, на которой лежит отрезок
        /// </summary>
        /// <remarks>Если отрезок параллелен оси ординат, метод вернет <c>double.PositiveInfinity</c></remarks>
        public static double GetSlope(this Segment segment)
            => segment.Start.X == segment.End.X ? double.PositiveInfinity : (segment.End.Y - segment.Start.Y) / (segment.End.X - segment.Start.X);
    }
}
