using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
// Класс отслеживания изменений в сущностях EntityEntry


using NetApp.Contexts; // Подключение ApplicationDbContext
using NetApp.Entities; // Подключение MyEntity
using NetApp.Services; // Подключение IMyService


namespace NetApp.Services // namespace отсылка к папке проекта
{
    public class MyService : IMyService
    {
        // Поле класса с методами БД
        private readonly ApplicationDbContext _context;


        // Конструктор
        public MyService(ApplicationDbContext context)
        {
            _context = context;
        }


        // Метод получения всего
        public async Task<List<MyEntity>> ReadMany()
        {
            List<MyEntity> e_s;

            e_s = await _context.ListEntity.Select(t => new MyEntity
            {
                Id = t.Id,
                Title = t.Title,
                Completed = t.Completed,
                CreatedAt = t.CreatedAt,
                UpdatedAt = t.UpdatedAt
            }).ToListAsync();


            // Что-то для отказоустойчивости
            // else
            // {
            //     e_s = await queryable.Select(t => new MyEntity
            //     {
            //         Id = t.Id,
            //         Title = t.Title,
            //         Completed = t.Completed,
            //         CreatedAt = t.CreatedAt,
            //         UpdatedAt = t.UpdatedAt
            //     }).ToListAsync();
            // }

            return e_s;
        }


        // Метод получения по ID
        public async Task<MyEntity> ReadById(int id)
        {
            return await _context.ListEntity.FirstOrDefaultAsync(t => t.Id == id);
        }


        // Метод сохранения записи
        public async Task Create(MyEntity entity)
        {
            await _context.ListEntity.AddAsync(entity);
            await _context.SaveChangesAsync();
        }


        // Метод обновления записи
        public async Task<MyEntity> UpdateId(int id, MyEntity e_fromUser)
        {
            MyEntity e_fromDb = _context.ListEntity.First(t => t.Id == id);
            e_fromDb.Title = e_fromUser.Title;

            if (e_fromUser.Description != null)
                e_fromDb.Description = e_fromUser.Description;

            e_fromDb.Completed = e_fromUser.Completed;

            _context.Entry(e_fromDb).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return e_fromDb;
        }


        // Метод обновления без ID
        public async Task<MyEntity> Update(MyEntity e, MyEntity e_fromUser)
        {
            e.Title = e_fromUser.Title;

            if (e_fromUser.Description != null)
                e.Description = e_fromUser.Description;

            e.Completed = e_fromUser.Completed;

            _context.Entry(e).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return e;
        }


        // Метод удаления сущности
        public EntityEntry<MyEntity> Delete(int Id)
        {
            var e = _context.ListEntity.Find(Id); // Найти сущность
            EntityEntry<MyEntity> result = _context.ListEntity.Remove(e); // Удалить
            _context.SaveChangesAsync(); // Применить изменение к БД
            return result;
        }



        // Метод удаления всего
        public async Task DeleteAll()
        {
            _context.ListEntity.RemoveRange(_context.ListEntity);
            await _context.SaveChangesAsync();
        }


        // Если наследуетесь от IDisposable, нужно реализовать Dispose()
        // Dispose() - метод очистки памяти от неуправляемых объектов
        // Неуправляемый объект - тот, что создан пользователем
        // У управляемых объектов NET память очищается сама/
        public void Dispose()
        {

            _context.Dispose();
        }
    }
}