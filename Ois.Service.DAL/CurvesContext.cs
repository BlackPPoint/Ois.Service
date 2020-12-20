using Microsoft.EntityFrameworkCore;
using Ois.Service.DAL.Entities;

namespace Ois.Service.DAL
{
    /// <summary>
    /// Контекст данных
    /// </summary>
    public class CurvesContext : DbContext
    {
        /// <summary>
        /// Результаты пересечения кривых
        /// </summary>
        public DbSet<IntersectionsResult> IntersectionsResults { get; set; }

        /// <summary>
        /// Создает экземпляр контекста данных
        /// </summary>
        /// <param name="options">Настройки контекста данных</param>
        public CurvesContext(DbContextOptions<CurvesContext> options)
            : base(options) { }

        /// <summary>
        /// Обработчик создания модели данных
        /// </summary>
        /// <param name="modelBuilder">Построитель модели</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PointsCollection>()
                .HasMany(c => c.Points).WithOne()
                .IsRequired() // отмечаем связь как обязательную
                .OnDelete(DeleteBehavior.Cascade); // удаление коллекции приведет к удалению всех точек
        }
    }
}
