using System;
using System.Collections.Generic;

namespace Ois.Data
{
    /// <summary>
    /// Результат ранее выполненого поиска пересечения кривых
    /// </summary>
    public class FindIntersectionsResult
    {
        /// <summary>
        /// Идентификатор исходной команды
        /// </summary>
        public Guid Guid { get; set; }

        /// <summary>
        /// Первая кривая
        /// </summary>
        public Curve Curve1 { get; set; }

        /// <summary>
        /// Вторая кривая
        /// </summary>
        public Curve Curve2 { get; set; }

        /// <summary>
        /// Точки пересечения кривых
        /// </summary>
        public IList<Point> Intersections { get; set; }
    }
}
