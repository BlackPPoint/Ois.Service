using Ois.Data;
using Ois.Service.DAL.Etities;
using System.Linq;

namespace Ois.Service
{
    /// <summary>
    /// Вспомогательные методы для моделей Data
    /// </summary>
    internal static class DataModelsHelper
    {
        /// <summary>
        /// Преобразует <see cref="Curve"/> к коллекции точек
        /// </summary>
        public static PointsCollection ToPointsCollection(this Curve curve)
            => new PointsCollection
            {
                Name = curve.Name,
                Points = curve.Points.Select(ToDALPoint).ToList()
            };

        /// <summary>
        /// Преобразует <see cref="Data.Point"/> к точке DAL
        /// </summary>
        public static DAL.Etities.Point ToDALPoint(this Data.Point point)
            => new DAL.Etities.Point
            {
                X = point.X,
                Y = point.Y
            };
    }
}
