using Microsoft.AspNetCore.Mvc;
using Ois.Data;
using Ois.Service.DAL.Entities;
using Ois.Service.DAL.Repositories;
using Ois.Service.Exceptions;
using Ois.Utils;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Ois.Service.Controllers
{
    /// <summary>
    /// Контроллер работы с кривыми
    /// </summary>
    [ApiController, Route("api/curves")]
    public class CurvesController : ControllerBase
    {
        private IIntersectionsResultRepository _repository;

        /// <summary>
        /// Создает контроллер работы с кривыми
        /// </summary>
        /// <param name="repository"></param>
        public CurvesController(IIntersectionsResultRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Находит все пересечения двух кривых, указанных в команде
        /// </summary>
        /// <param name="command">Команда поиска пересечения двух кривых</param>
        /// <returns>Список точек пересечения кривых</returns>
        /// <response code="200">Пересечения двух кривых были расчитаны и сохранены</response>
        /// <response code="400">Ошибка в теле запроса</response>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST api/curves/find
        ///     {
        ///        "guid": "9a91c790-d040-4d80-94a2-20fab31761a5",
        ///        "curve1": {
        ///            "name": "Curve 1",
        ///            "points": [
        ///        	        { "x": 1, "y": 1 },
        ///        	        { "x": 3, "y": 3 },
        ///                 { "x": 5, "y": 1 },
        ///                 { "x": 7, "y": 3 },
        ///                 { "x": 9, "y": 1 } ]
        ///            },
        ///        "curve2": {
        ///            "name": "Curve 2",
        ///            "points": [
        ///        	        { "x": 1, "y": 3 },
        ///        	        { "x": 3, "y": 1 },
        ///                 { "x": 5, "y": 3 },
        ///                 { "x": 7, "y": 1 },
        ///                 { "x": 9, "y": 3 } ]
        ///            }
        ///     }
        /// </remarks>
        [HttpPost("find", Name = "FindIntersectionsPath")]
        public async Task<CurveIntersections> FindIntersections([FromBody] FindIntersectionsCommand command)
        {
            // проверяем команду
            if (command.Curve1 == null) throw HttpResponseException.BadRequest($"Необходимо указать {nameof(FindIntersectionsCommand.Curve1)}");
            if (command.Curve2 == null) throw HttpResponseException.BadRequest($"Необходимо указать {nameof(FindIntersectionsCommand.Curve2)}");

            // находим точки пересечения
            var points = command.Curve1.FindIntersections(command.Curve2).ToList();
            // подготавливаем коллекции точек
            var curve1 = command.Curve1.ToPointsCollection();
            var curve2 = command.Curve2.ToPointsCollection();
            var intersections = new PointsCollection
            {
                Name = $"Intersections {command.Guid:P}",
                Points = points.Select(p => p.ToDALPoint()).ToList()
            };

            // сохраняем результат запроса
            await _repository.CreateAsync(command.Guid, curve1, curve2, intersections);

            var result = new CurveIntersections { Points = points };
            return result;
        }

        /// <summary>
        /// Получает результат ранее найденого пересечения кривых
        /// </summary>
        /// <param name="guid">Идентификатор исходной команды</param>
        /// <returns>Результат поиска пересечений кривых</returns>
        /// <response code="200">Результат был найден</response>
        /// <response code="404">Результат для команды с указанным идентификатором не найден</response>
        [HttpGet("{guid:guid}")]
        public async Task<FindIntersectionsResult> GetIntersectionsResult([FromRoute] Guid guid)
        {
            // получаем ранее полученый результат
            var intersectionsResult = await _repository.GetByIdAsync(guid);
            if (intersectionsResult == null)
                throw HttpResponseException.NotFound($"Результат с Guid {guid:P} не найден");

            var result = intersectionsResult.ToFindIntersectionsResult();
            return result;
        }
    }
}
