﻿using System.Linq.Expressions;
using API_Project_cw2.Interfaces;

namespace API_Project_cw2.Data;

public class Repository : IRepository
{
    private readonly DataContext _context;

    public Repository(DataContext context)
    {
        _context = context;
    }

    public async Task<T> Add<T>(T entity) where T : class
    {
        var entityFromFb = _context.Set<T>().Add(entity);
        await _context.SaveChangesAsync();
        return entityFromFb.Entity;
    }

    public async Task<T> Update<T>(T entity) where T : class
    {
        var updated = _context.Update(entity);
        await _context.SaveChangesAsync();
        return updated.Entity;
    }

    public Task Delete<T>(int id) where T : class
    {
        throw new NotImplementedException();
    }

    public Task<T> GetById<T>(int id) where T : class
    {
        throw new NotImplementedException();
    }

    public IQueryable<T> GetAll<T>() where T : class
    {
        return _context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetQuery<T>(Expression<Func<T, bool>> func) where T : class
    {
        return _context.Set<T>().Where(func);
    }
}