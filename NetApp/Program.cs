using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using NetApp.Contexts; // Импорт ApplicationDBContext

namespace NetApp // отсылка к папке проекта
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var appDbContext = services.GetRequiredService<ApplicationDbContext>();
                // appDbContext.Database.EnsureCreatedAsync();
                // Создание БД, если ее нет
            }

            host.Run();
        }


        // Использование настроек БД из Startup
        // По умолчанию CreateDefaultBuilder загружает:
        // 1. appsettings.json
        // 2. appsettings.{Environment}.json  
        // 3. User Secrets (только в Development)
        // 4. Переменные окружения
        // 5. Аргументы командной строки
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://localhost:8080", "https://localhost:5001")
                .UseStartup<Startup>();
    }
}