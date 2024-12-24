using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using VillaAPI.data;
using VillaAPI.Models;

namespace VillaAPI.Rebository
{
    public class VillaRepository : IVillaRepository
    {
        private readonly AppDbContext _context;
        public VillaRepository(AppDbContext context) { 
        
            _context = context;
        }
        public async Task CreateAsync(Villa entity)
        {
           _context.Villas.Add(entity);
            await SaveAsync();
        }

        public async Task<Villa> GetAsync(Expression<Func<Villa , bool>> filter = null ,  bool tracked = true)
        {

            IQueryable<Villa> query = _context.Villas;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.FirstOrDefaultAsync();
        }

        

        public async Task<List<Villa>> GetAllAsync(Expression<Func<Villa , bool>> filter = null)
        {
            IQueryable<Villa> query = _context.Villas;

            if(filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }

        public async Task RemoveAsync(Villa entity)
        {
            _context.Villas.Remove(entity);

            await SaveAsync();
        }

        public async Task UpdateAsync(Villa entity)
        {
            _context.Villas.Update(entity);

            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
