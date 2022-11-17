using BookStore.Entities.Entities;
using BookStore.WebContracts.Author.Response;
using BookStore.WebContracts.Book.Response;
using BookStore.WebContracts.Genres.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Services.Mapper
{
    public static class ServiceMappers
    {
       public static List<BookResponse> MaptoBookResponseList(List<BookEntity> entities)
        {
            List<BookResponse> books = new List<BookResponse>();
            foreach (BookEntity entity in entities)
            {
                books.Add(MaptToBookEntityResponse(entity));   
            }
            return books;
        }
        public static BookResponse MaptToBookEntityResponse(BookEntity entity)
        {
            
            string authorName = string.Empty;
            if (entity.AuthorEntity != null)
            {
                
                authorName = entity.AuthorEntity.AuthorName;
            }

            string genreName = string.Empty;
            if (entity.GenresEntity != null)
            {
                genreName = entity.GenresEntity.Name;
            }
            return new BookResponse
            {
               
                Id = entity.Id,
                Name = entity.Name,
                Price = entity.Price,
                AuthorId = entity.AuthorId,
                AuthorName = authorName,
                GenreId = entity.GenreId,
                GenreName = genreName
                
                
            };
        }
        public static List<AuthorResponse> MaptoAuthorResponseList(List<AuthorEntity> entities)
        {
            List<AuthorResponse> author = new List<AuthorResponse>();
            foreach (AuthorEntity entity in entities)
            {
                author.Add(MaptoAuthorEntityResponse(entity));
            }
            return author;
        }
        public static AuthorResponse MaptoAuthorEntityResponse(AuthorEntity entity)
        {
            return new AuthorResponse
            {
                AuthorId = entity.AuthorId,
                AuthorName = entity.AuthorName,
                IsCreatedOn = entity.IsCreatedOn,
                IsDeleted = entity.IsDeleted
            };
        }
        public static List<GenreResponse> MaptoGenreResponseList(List<GenresEntity> entities)
        {
            List<GenreResponse> genre = new List<GenreResponse>();
            foreach (GenresEntity entity in entities)
            {
                genre.Add(MaptoGenreEntityResponse(entity));
            }
            return genre;
        }
        public static GenreResponse MaptoGenreEntityResponse(GenresEntity entity)
        {
            return new GenreResponse
            {
                Id = entity.GenreId,
                GenreName = entity.Name,
                IsCreatedOn = entity.IsCreatedOn,
                IsUpdatedOn = entity.IsUpdatedon
            };
        }
    }
    
}
