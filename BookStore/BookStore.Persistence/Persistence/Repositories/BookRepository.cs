using BookStore.Entities.Entities;
using BookStore.Persistence.Contracts.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Persistence.Persistence.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreDbContext _bookStoreDbContext;

        public BookRepository(BookStoreDbContext bookStoreDbContext)
        {
            _bookStoreDbContext = bookStoreDbContext;
        }

        public async Task<bool> AnyAsync(BookEntity entity)
        {
            var name =  await _bookStoreDbContext.tbl_books.AnyAsync(p => p.Name.ToUpper().Equals(entity.Name.ToUpper()));
            var price = await _bookStoreDbContext.tbl_books.AnyAsync(p => p.Price.Equals(entity.Price));
            var authorId = await _bookStoreDbContext.tbl_books.AllAsync(p => p.AuthorId.Equals(entity.AuthorId));
            var genreId =  await _bookStoreDbContext.tbl_books.AllAsync(p => p.GenreId.Equals(entity.GenreId));
            if (name && price && authorId & genreId)
            {
                return true;
            }
            else
                return false;
            //AnyAsync<TSource>(IQueryable<TSource>, Expression<Func<TSource, Boolean>>, CancellationToken)
        }

        public async Task<BookEntity> CreateAsync(BookEntity entity)
        {
             EntityEntry<BookEntity> entry = await _bookStoreDbContext.tbl_books.AddAsync(entity);
             await _bookStoreDbContext.SaveChangesAsync();
            return entry.Entity;
        }

        public async Task DeleteAsync(BookEntity entity)
        {
             

                _bookStoreDbContext.tbl_books.Remove(entity);
                await _bookStoreDbContext.SaveChangesAsync();
            

            
        }

        public Task<List<BookEntity>> GetBookEntitiesAsync()
        {
            return _bookStoreDbContext.tbl_books.ToListAsync();
        }
         
        public async Task<BookEntity> GetByIdAsync(int id)
        {
            var result = await _bookStoreDbContext.tbl_books.FindAsync(id);
            return result;
            
        }

        public async Task<BookEntity> GetByNameAsync(string name)
        {
            return await _bookStoreDbContext.tbl_books.FirstOrDefaultAsync(x=>x.Name.ToUpper().Equals(name));
            
        }

        public async Task<BookEntity> UpdateAsync(BookEntity entity)
        {
            EntityEntry<BookEntity> entry = _bookStoreDbContext.Update(entity);
            await _bookStoreDbContext.SaveChangesAsync();
            return entry.Entity;
        }

       
    }
}
