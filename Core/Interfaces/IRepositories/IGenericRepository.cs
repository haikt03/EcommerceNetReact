﻿using Core.Entities;

namespace Core.Interfaces.IRepositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        bool Exists(int id);
    }
}