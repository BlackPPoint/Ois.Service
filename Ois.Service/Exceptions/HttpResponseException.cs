using Ois.Service.Filters;
using System;
using System.Runtime.Serialization;

namespace Ois.Service.Exceptions
{
    /// <summary>
    /// Класс исключения обрабатываемого <see cref="HttpResponseExceptionFilter"/>
    /// </summary>
    [Serializable]
    public class HttpResponseException : Exception
    {
        /// <summary>
        /// HTTP код возвращаемого ответа
        /// </summary>
        public int Status { get; set; } = 500;

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public HttpResponseException() : base("Что-то пошло не так") { }

        /// <summary>
        /// Создает экземпляр объекта исключчения с указанным сообщением об ошибке
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
        public HttpResponseException(string message) : base(message) { }

        /// <summary>
        /// Создает экземпляр объекта исключчения с указанным сообщением об ошибке и вложенным исключением
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="inner">Вложенное исключение</param>
        public HttpResponseException(string message, Exception inner) : base(message, inner) { }

        /// <summary>
        /// Конструктор для бинарной сериализации
        /// </summary>
        protected HttpResponseException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// Создает объект исключения о не найденом объекта (статус 404)
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
        public static HttpResponseException NotFound(string message)
            => new HttpResponseException(message) { Status = 404 };

        /// <summary>
        /// Создает объект исключения о ошибке в запросе (статус 400)
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
        public static HttpResponseException BadRequest(string message)
            => new HttpResponseException(message) { Status = 400 };
    }
}
