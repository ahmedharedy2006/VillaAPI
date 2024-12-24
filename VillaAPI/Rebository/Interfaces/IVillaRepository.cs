using System.Linq.Expressions;
using VillaAPI.Models;

namespace VillaAPI.Rebository.Interfaces
{
    public interface IVillaRepository
    {
        

        Task<Villa> UpdateAsync(Villa entity);

        
    }
}
