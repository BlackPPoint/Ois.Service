using System;

namespace Ois.Data
{
    /// <summary>
    /// Команда поиска пересечения двух кривых
    /// </summary>
    public class FindIntersectionsCommand
    {
        /// <summary>
        /// Идентификатор команды
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
    }
}
