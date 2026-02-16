using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore; // импорт UseSqlServer
using NetApp.Contexts;
using NetApp.Services;

namespace NetApp
{
    public class Startup
    {
        // Поле класса
        public IConfiguration Configuration { get; }


        // Конструктор
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            // options.UseSqlServer
            // options.UseSqlite("Data Source=mydb.db")
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(
            Configuration.GetConnectionString("DefaultConnection")
            )
            );


            // Работа с интерфейсами
            services.AddScoped<IMyService, MyService>();

            services.AddControllers(); // Подключаем контроллеры
        }


        // Configure - обязательный метод в Startup.cs 
        // Он определяет как будет обрабатываться HTTP-запрос 
        // Здесь будет предобработка Auth Middlew, Routing Middlew, ErrorHandl Middlew
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Если приложение в IDE, будут выводиться ошибки
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
 
            app.UseRouting();
 
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // подключаем контроллеры
            });
        }

    }
}