using BookStore.Entities.Entities;
using BookStore.Persistence.Contracts.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Persistence.Persistence.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly BookStoreDbContext _bookStoreDbContext;

        public AuthorRepository(BookStoreDbContext bookStoreDbContext)
        {
            _bookStoreDbContext = bookStoreDbContext;
        }

       
        //Author: Create
        public async Task<AuthorEntity> CreateAuthorAsync(AuthorEntity entity)
        {
            entity.IsCreatedOn = DateTime.UtcNow;
            entity.IsUpdatedon = DateTime.UtcNow;
            EntityEntry<AuthorEntity> entry = await _bookStoreDbContext.tbl_Author.AddAsync(entity);
            await _bookStoreDbContext.SaveChangesAsync();
            return entry.Entity;
        }
        //Author: Delete
        public async Task DeleteAsync(AuthorEntity entity)
        {

            _bookStoreDbContext.tbl_Author.Remove(entity);
            await _bookStoreDbContext.SaveChangesAsync();

        }
        //Author: List
        public async Task<List<AuthorEntity>> GetAuthorEntitiesAsync()
        {
            return await _bookStoreDbContext.tbl_Author.ToListAsync();
        }
        //Author: Get Author BY ID
        public async Task<AuthorEntity> GetByAuthorIdAsync(int id)
        {
            var result = await _bookStoreDbContext.tbl_Author.FindAsync(id);
            return result;
        }
        public async Task<bool> AnyAsync(string title)
        {
            return await _bookStoreDbContext.tbl_Author.AnyAsync(p => p.AuthorName.ToUpper().Equals(title.ToUpper()));
        }
        //Author: Get Author By Name
        public async Task<AuthorEntity> GetByAuthorNameAsync(string name)
        {
            var result = await _bookStoreDbContext.tbl_Author.FirstOrDefaultAsync(a => a.AuthorName.ToUpper().Equals(name));
            return result;
        }
        //Author: Update
        public async Task<AuthorEntity> UpdateAuthorAsync(AuthorEntity entity)
        {
            EntityEntry<AuthorEntity> entry = _bookStoreDbContext.tbl_Author.Update(entity);
            await _bookStoreDbContext.SaveChangesAsync();
            return entry.Entity;
        }

        public async Task<List<AuthorEntity>> GetAuthorByIdsAsync(int[] ids)
        {
            return await _bookStoreDbContext.tbl_Author.Where(x => ids.Contains(x.AuthorId)).ToListAsync();
        }
    }
}
