using Microsoft.EntityFrameworkCore;
using SALESSYSTEM.DAL.Context;
using SALESSYSTEM.DAL.Interfaces;
using System.Linq.Expressions;

namespace SALESSYSTEM.DAL.Implementation
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly SALESSYSDBContext _context;
        private readonly DbSet<TEntity> _entities;

        public GenericRepository(SALESSYSDBContext context)
        {
            _context = context;
            _entities = _context.Set<TEntity>();
        }


        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> filter)
        {
            try
            {
                var get = await _entities.FirstOrDefaultAsync(filter);
                return get!;
            }catch 
            {
                throw;
            }
        }

        public async Task<TEntity> Create(TEntity entity)
        {
            try 
            { 
                _entities.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch 
            {
                throw;
            }
        }

        public async Task<bool> Update(TEntity entity)
        {
            try
            {
                _entities.Update(entity);
               await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            };
        }



        public async Task<bool> Remove(TEntity entity)
        {
            try
            {
                _entities.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            };
        }

       
        public async Task<IQueryable<TEntity>> GetEntityQuery(Expression<Func<TEntity, bool>>? filter = null)
        {
           IQueryable<TEntity> queryEntity =  filter == null ? _entities : _entities.Where(filter);
            return queryEntity;
        }

    }
}
