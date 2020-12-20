using Microsoft.EntityFrameworkCore;
using Ois.Service.DAL.Etities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Ois.Service.DAL.Repositories
{
    /// <summary>
    /// Репозиторий результатов пересечения кривых
    /// </summary>
    public class IntersectionsResultRepository : IIntersectionsResultRepository
    {
        private readonly CurvesContext _context; // контекст данных

        /// <summary>
        /// Создает репозиторий результатов пересечения кривых
        /// </summary>
        /// <param name="context">Контекст данных</param>
        public IntersectionsResultRepository(CurvesContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Сохраняет или обновляет результат пересечения кривых
        /// </summary>
        /// <param name="id">Идентификатор исходной команды</param>
        /// <param name="curve1">Первая кривая</param>
        /// <param name="curve2">Вторая кривая</param>
        /// <param name="interceptions">Пересечения кривых</param>
        /// <returns>Созданный (или обновленный) результат пересечения кривых</returns>
        public async Task<IntersectionsResult> CreateAsync(Guid id, PointsCollection curve1, PointsCollection curve2, PointsCollection interceptions)
        {
            // получаем, либо создаем новый результат пересечения кривых
            var intersectionsResult = await _context.IntersectionsResults
                .Include(r => r.Curve1)
                .Include(r => r.Curve2)
                .Include(r => r.Intersections)
                .Where(r => r.Id == id)
                .SingleOrDefaultAsync();

            if (intersectionsResult == null)
            {
                // добавляем новый результат в контекст
                intersectionsResult = new IntersectionsResult { Id = id };
                _context.IntersectionsResults.Add(intersectionsResult);
            }
            else
            {
                // помечаем существующие коллекции точек на удаление
                _context.Entry(intersectionsResult.Curve1).State = EntityState.Deleted;
                _context.Entry(intersectionsResult.Curve2).State = EntityState.Deleted;
                _context.Entry(intersectionsResult.Intersections).State = EntityState.Deleted;
            }

            // обновляем значения получившихся коллекций точек
            intersectionsResult.Curve1 = curve1;
            intersectionsResult.Curve2 = curve2;
            intersectionsResult.Intersections = interceptions;

            // сохраняем изменения
            await _context.SaveChangesAsync();

            return intersectionsResult;
        }

        /// <summary>
        /// Получает результат пересечения кривых по идентификатору исходной команды
        /// </summary>
        /// <param name="id">Идентификатор исходной команды</param>
        /// <returns>Результат пересечения кривых, либо <c>null</c></returns>
        public Task<IntersectionsResult> GetByIdAsync(Guid id)
            => _context.IntersectionsResults
                .Include(r => r.Curve1).ThenInclude(c => c.Points)
                .Include(r => r.Curve2).ThenInclude(c => c.Points)
                .Include(r => r.Intersections).ThenInclude(c => c.Points)
                .Where(r => r.Id == id)
                .SingleOrDefaultAsync();
    }
}
