using Ois.Data;
using Ois.Service.DAL.Entities;
using System.Linq;

namespace Ois.Service
{
    /// <summary>
    /// Вспомогательные методы для моделей DAL
    /// </summary>
    internal static class DALEntitiesHelpers
    {
        /// <summary>
        /// Преобразует результат пересечения кривых к <see cref="FindIntersectionsResult"/>
        /// </summary>
        public static FindIntersectionsResult ToFindIntersectionsResult(this IntersectionsResult result)
            => new FindIntersectionsResult
            {
                Guid = result.Id,
                Curve1 = result.Curve1.ToCurve(),
                Curve2 = result.Curve2.ToCurve(),
                Intersections = result.Intersections.Points.OrderBy(p => p.X).Select(ToDataPoint).ToList()
            };

        /// <summary>
        /// Преобразует коллекцию точек к <see cref="Curve"/>
        /// </summary>
        public static Curve ToCurve(this PointsCollection pointsCollection)
            => new Curve
            {
                Name = pointsCollection.Name,
                Points = pointsCollection.Points.OrderBy(p => p.X).Select(ToDataPoint).ToList()
            };

        /// <summary>
        /// Преобразует точку DAL к <see cref="Data.Point"/>
        /// </summary>
        public static Data.Point ToDataPoint(this DAL.Entities.Point point)
            => new Data.Point
            {
                X = point.X,
                Y = point.Y
            };
    }
}
