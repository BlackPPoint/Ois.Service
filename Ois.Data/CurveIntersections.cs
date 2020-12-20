using System.Collections.Generic;

namespace Ois.Data
{
    /// <summary>
    /// Результат поиска пересечения кривых
    /// </summary>
    public class CurveIntersections
    {
        /// <summary>
        /// Найденные точки пересечения
        /// </summary>
        public IList<Point> Points { get; set; }
    }
}
