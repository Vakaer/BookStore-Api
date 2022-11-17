using BookStore.Entities.Entities;
using BookStore.Persistence.Contracts.Contracts;
using BookStore.WebContracts;
using BookStore.WebContracts.Genres.Request;
using BookStore.WebContracts.Genres.Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Persistence.Persistence.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly BookStoreDbContext _bookStoreDbContext;

        public GenreRepository(BookStoreDbContext bookStoreDbContext)
        {
            _bookStoreDbContext = bookStoreDbContext;
        }

        public async Task<bool> AnyAsync(string title)
        {
            return await _bookStoreDbContext.tbl_Genres.AnyAsync(p => p.Name.ToUpper().Equals(title.ToUpper()));
        }

        //Genre: Create
        public async Task<GenresEntity> CreateGenreAsync(GenresEntity entity)
        {
            entity.IsCreatedOn= DateTime.UtcNow;
            entity.IsUpdatedon= DateTime.UtcNow;
            EntityEntry<GenresEntity> entry = await _bookStoreDbContext.tbl_Genres.AddAsync(entity);
            await _bookStoreDbContext.SaveChangesAsync();
            return entry.Entity;
        }

        //Genre: Delete
        public async Task DeleteAsync(GenresEntity entity)
        {
            _bookStoreDbContext.tbl_Genres.Remove(entity);
            await _bookStoreDbContext.SaveChangesAsync();
        }


        //Genre: Get ID
        public async Task<GenresEntity> GetByIdAsync(int id)
        {
            var result = await _bookStoreDbContext.tbl_Genres.FindAsync(id);
            return result;
        }

        public async Task<List<GenresEntity>> GetGenresByIdsAsync(int[] ids)
        {
            return await _bookStoreDbContext.tbl_Genres.Where(x => ids.Contains(x.GenreId)).ToListAsync();
        }

        //Genre: Get By Name
        public async Task<GenresEntity> GetByNameAsync(string name)
        {
            var result = await _bookStoreDbContext.tbl_Genres.FirstOrDefaultAsync(a => a.Name.ToUpper().Equals(name));
            return result;
        }
        //Genre: Get List
        public async Task<List<GenresEntity>> GetGenreEntitiesAsync()
        {
            return await _bookStoreDbContext.tbl_Genres.ToListAsync(); ;
        }
        //Genre: Edit
        public async Task<GenresEntity> UpdateGenreAsync(GenresEntity entity)
        {
            EntityEntry<GenresEntity> entry = _bookStoreDbContext.tbl_Genres.Update(entity);
            await _bookStoreDbContext.SaveChangesAsync();
            return entry.Entity;
        }
        
    }
}
