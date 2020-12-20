namespace Ois.Service.DAL.Entities
{
    /// <summary>
    /// Точка
    /// </summary>
    public class Point
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Координата по оси абсцисс
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Координата по оси ординат
        /// </summary>
        public double Y { get; set; }
    }
}
