using BookStore.Entities.Entities;
using BookStore.WebContracts;
using BookStore.WebContracts.Genres.Request;
using BookStore.WebContracts.Genres.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Persistence.Contracts.Contracts
{
    public interface IGenreRepository
    {
        Task<GenresEntity> GetByIdAsync(int id);
        Task<GenresEntity> GetByNameAsync(string name);
        Task<bool> AnyAsync(string title);
        Task<GenresEntity> CreateGenreAsync(GenresEntity entity);
        Task<GenresEntity> UpdateGenreAsync(GenresEntity entity);
        Task DeleteAsync(GenresEntity entity);
        Task<List<GenresEntity>> GetGenreEntitiesAsync();
        Task<List<GenresEntity>> GetGenresByIdsAsync(int[] ids);
    }
}
