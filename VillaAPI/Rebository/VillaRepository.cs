using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using VillaAPI.data;
using VillaAPI.Models;
using VillaAPI.Rebository.Interfaces;

namespace VillaAPI.Rebository
{
    public class VillaRepository : Repository<Villa> , IVillaRepository
    {
        private readonly AppDbContext _context;
        public VillaRepository(AppDbContext context) : base(context) 
        { 
        
            _context = context;
        }
     
        public async Task<Villa> UpdateAsync(Villa entity)
        {
            entity.UpdatedDate = DateTime.Now;

            _context.Villas.Update(entity);

            await _context.SaveChangesAsync();
            
            return entity;
        }

       
    }
}
