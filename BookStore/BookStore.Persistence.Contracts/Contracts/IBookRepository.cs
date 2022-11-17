using BookStore.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Persistence.Contracts.Contracts
{
    public interface IBookRepository
    {
        Task<BookEntity> GetByIdAsync(int id);
        Task<BookEntity> GetByNameAsync(string name);
        Task<bool> AnyAsync(BookEntity entity);
        Task<BookEntity> CreateAsync(BookEntity entity);
        Task<BookEntity> UpdateAsync(BookEntity entity);
        Task DeleteAsync(BookEntity entity);
        Task<List<BookEntity>> GetBookEntitiesAsync();

    }
}
