using Ois.Service.DAL.Etities;
using System;
using System.Threading.Tasks;

namespace Ois.Service.DAL.Repositories
{
    /// <summary>
    /// Репозиторий результатов пересечения кривых
    /// </summary>
    public interface IIntersectionsResultRepository
    {
        /// <summary>
        /// Сохраняет или обновляет результат пересечения кривых
        /// </summary>
        /// <param name="id">Идентификатор исходной команды</param>
        /// <param name="curve1">Первая кривая</param>
        /// <param name="curve2">Вторая кривая</param>
        /// <param name="interceptions">Пересечения кривых</param>
        /// <returns>Созданный (или обновленный) результат пересечения кривых</returns>
        Task<IntersectionsResult> CreateAsync(Guid id, PointsCollection curve1, PointsCollection curve2, PointsCollection interceptions);

        /// <summary>
        /// Получает результат пересечения кривых по идентификатору исходной команды
        /// </summary>
        /// <param name="id">Идентификатор исходной команды</param>
        /// <returns>Результат пересечения кривых, либо <c>null</c></returns>
        Task<IntersectionsResult> GetByIdAsync(Guid id);
    }
}
