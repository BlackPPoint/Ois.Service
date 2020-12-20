using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ois.Service.DAL.Etities
{
    /// <summary>
    /// Коллекция точек
    /// </summary>
    public class PointsCollection
    {
        /// <summary>
        /// Идентификатор коллекции
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название коллекции
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Точки коллекции
        /// </summary>
        public ICollection<Point> Points { get; set; }
            = new List<Point>();
    }
}
