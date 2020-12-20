using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ois.Service.DAL;
using System;

namespace Ois.Service
{
    /// <summary>
    /// Методы расширения <see cref="IServiceProvider"/>
    /// </summary>
    internal static class IServiceProviderExtension
    {
        /// <summary>
        /// Выполняет инициализацию базы данных
        /// </summary>
        public static void InitializeDatabase(this IServiceProvider serviceProvider)
        {
            try
            {
                var context = serviceProvider.GetRequiredService<CurvesContext>();
                context.Database.EnsureCreated(); // создаст файл базы и таблицы, если его нет
            }
            catch (Exception ex)
            {
                var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "Во время инициализации базы данных произошла ошибка");
            }
        }
    }
}
