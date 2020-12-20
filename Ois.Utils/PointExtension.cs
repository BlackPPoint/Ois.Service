using Ois.Data;

namespace Ois.Utils
{
    /// <summary>
    /// Методы расширения для <see cref="Point"/>
    /// </summary>
    public static class PointExtension
    {
        /// <summary>
        /// Определяет, совпадает ли точка с указаной
        /// </summary>
        public static bool Coincides(this Point originPoint, Point point)
            => point != null && originPoint.X == point.X && originPoint.Y == point.Y;

        /// <summary>
        /// Создает копию точки
        /// </summary>
        public static Point Copy(this Point point)
            => new Point { X = point.X, Y = point.Y };
    }
}
