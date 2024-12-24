using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using VillaAPI.data;
using VillaAPI.Models;
using VillaAPI.Rebository.Interfaces;

namespace VillaAPI.Rebository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;

        internal DbSet<T> dbset;
        public Repository(AppDbContext context)
        {

            _context = context;
            this.dbset = _context.Set<T>();
        }
        public async Task CreateAsync(T entity)
        {
            dbset.Add(entity);
            await SaveAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true)
        {

            IQueryable<T> query = dbset;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.FirstOrDefaultAsync();
        }



        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = dbset;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }

        public async Task RemoveAsync(T entity)
        {
            dbset.Remove(entity);

            await SaveAsync();
        }

     
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
