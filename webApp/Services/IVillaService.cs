﻿using webApp.Models.DTO;

namespace webApp.Services
{
    public interface IVillaService 
    {
        Task<T> GetAllAsync<T>();

        Task<T> GetAsync<T>(int id);

        Task<T> CreateAsync<T>(VillaCreateDTO villa);

        Task<T> UpdateAsync<T>(VillaUpdateDTO villa);

        Task<T> DeleteAsync<T>(int id);
    }
}
