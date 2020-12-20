using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Ois.Service.DAL;
using Ois.Service.DAL.Repositories;
using Ois.Service.Filters;
using System;
using System.IO;
using System.Reflection;

namespace Ois.Service
{
    /// <summary>
    /// Класс запуска приложения
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Настройки приложения
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Создает экземпляр класса запуска приложения
        /// </summary>
        /// <param name="configuration">Настройки приложения</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Настраивает контейнер сервисов приложения
        /// </summary>
        /// <param name="services">Контейнер сервисов приложения</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // добавляем контекст данных
            services.AddDbContext<CurvesContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            // добавляем контроллеры API
            services.AddControllers(options =>
                options.Filters.Add(new HttpResponseExceptionFilter()));

            // добавляем документацию swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Version = "v1", Title = "OIS Service API" });

                // добавляем XML комментарии
                var serviceDocsFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, serviceDocsFilename));
            });

            // добавляем репозитории DAL
            services.AddScoped<IIntersectionsResultRepository, IntersectionsResultRepository>();
        }

        /// <summary>
        /// Настраиваем конвеер приложения
        /// </summary>
        /// <param name="app">Конвеер приложения</param>
        /// <param name="env">Окружение хоста приложения</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // добавляем swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "OIS Service API V1");
            });

            // добавляем обработку запросов API
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
