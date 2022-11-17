using BookStore.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Persistence.Contracts.Contracts
{
    public interface IAuthorRepository
    {
        Task<AuthorEntity> GetByAuthorIdAsync(int id);
        Task<AuthorEntity> GetByAuthorNameAsync(string name);
        Task<AuthorEntity> CreateAuthorAsync(AuthorEntity entity);
        Task<AuthorEntity> UpdateAuthorAsync(AuthorEntity entity);
        Task<bool> AnyAsync(string title);
        Task DeleteAsync(AuthorEntity entity);
        Task<List<AuthorEntity>> GetAuthorEntitiesAsync();
        Task<List<AuthorEntity>> GetAuthorByIdsAsync(int[] ids);
    }
}
