using System.Collections.Generic;

namespace Ois.Data
{
    /// <summary>
    /// Кривая
    /// </summary>
    public class Curve
    {
        /// <summary>
        /// Название кривой
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Точки кривой
        /// </summary>
        public IList<Point> Points { get; set; }
    }
}
