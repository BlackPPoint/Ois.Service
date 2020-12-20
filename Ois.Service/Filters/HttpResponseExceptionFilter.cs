using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Ois.Service.Exceptions;

namespace Ois.Service.Filters
{
    /// <summary>
    /// Фильтр обработки исключений <see cref="HttpResponseException"/>
    /// </summary>
    public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        /// <summary>
        /// Порядок обработки фильтра
        /// </summary>
        public int Order { get; } = 1000;

        /// <summary>
        /// Обработчик выполнения действия
        /// </summary>
        /// <param name="context">Контекст действия</param>
        public void OnActionExecuting(ActionExecutingContext context) { }

        /// <summary>
        /// Обработчик завершения выполнения действия
        /// </summary>
        /// <param name="context">Контекст действия</param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is HttpResponseException exception)
            {
                // подготавливаем сообщение об ошибке
                context.Result = new JsonResult(new { exception.Status, exception.Message }) { StatusCode = exception.Status };
                context.ExceptionHandled = true;
            }
        }
    }
}
