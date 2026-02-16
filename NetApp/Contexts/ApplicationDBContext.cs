using NetApp.Entities;
using Microsoft.EntityFrameworkCore;


// Подключаем методы для работы с БД
namespace NetApp.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Класс, через который идет связь с таблицей
        public DbSet<MyEntity> ListEntity { get; set; }
    }
}