using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking; // import EntityEntry
using NetApp.Entities;

namespace NetApp.Services
{
    public interface IMyService : IDisposable
    {
        // ... методы репозитория ...
        Task<List<MyEntity>> ReadMany();
        Task<MyEntity> ReadById(int id);
        Task Create(MyEntity entity);
        Task<MyEntity> UpdateId(int id, MyEntity e_fromUser);
        Task<MyEntity> Update(MyEntity e, MyEntity e_fromUser);
        EntityEntry<MyEntity> Delete(int Id);
        Task DeleteAll();
    }
}