using System;
using System.ComponentModel.DataAnnotations;

namespace Ois.Service.DAL.Etities
{
    /// <summary>
    /// Результат пересечения кривых
    /// </summary>
    public class IntersectionsResult
    {
        /// <summary>
        /// Идентификатор пересечения
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Точки первой кривой
        /// </summary>
        [Required]
        public PointsCollection Curve1 { get; set; }

        /// <summary>
        /// Точки второй кривой
        /// </summary>
        [Required]
        public PointsCollection Curve2 { get; set; }

        /// <summary>
        /// Точки пересечения
        /// </summary>
        [Required]
        public PointsCollection Intersections { get; set; }
    }
}
