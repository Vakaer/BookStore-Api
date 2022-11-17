using BookStore.Entities.Entities;
using BookStore.Persistence.Contracts.Contracts;
using BookStore.Services.contracts.Contracts;
using BookStore.Services.Mapper;
using BookStore.WebContracts;
using BookStore.WebContracts.Book.Request;
using BookStore.WebContracts.Book.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Services.Services
{
    public class BookServices : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IAuthorRepository _authorRepository;

        public BookServices(IBookRepository bookRepository,
                            IGenreRepository genreRepository,
                            IAuthorRepository authorRepository)
        {
            _bookRepository = bookRepository;
            _genreRepository = genreRepository;
            _authorRepository = authorRepository;
        }


        //Book:Create
        public async Task<ServiceResult<BookResponse>> CreateBookAsync(CreateBookRequest request)
        {
            
            ServiceResult<BookResponse> serviceResult = new ServiceResult<BookResponse>(System.Net.HttpStatusCode.BadRequest);
            if(request == null || string.IsNullOrEmpty(request.Name))
            {
                serviceResult.AddError("Invalid Request");
                return serviceResult;
            }
            BookEntity bookEntity = await _bookRepository.GetByNameAsync(request.Name);
            if(bookEntity != null)
            {
                serviceResult.AddError("Name already exists");
                return serviceResult;
            }

            bookEntity = new BookEntity
            {
                Name = request.Name,
                Price = (int)request.Price,
                GenreId = request.GenreId,
                AuthorId = request.AuthorId

            };
            await _bookRepository.CreateAsync(bookEntity);
            BookResponse bookResponse = ServiceMappers.MaptToBookEntityResponse(bookEntity);
            return new ServiceResult<BookResponse>(bookResponse);
        }
        //Book:delete
        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            ServiceResult<bool> serviceResult = new ServiceResult<bool>(System.Net.HttpStatusCode.BadRequest);
            BookEntity bookEntity = await _bookRepository.GetByIdAsync(id);
            if(bookEntity == null)
            {
                serviceResult.AddError("Category Not Found");
                return serviceResult;
            }
            await _bookRepository.DeleteAsync(bookEntity);
            return new ServiceResult<bool>(System.Net.HttpStatusCode.OK, true);

            
        }
        //Book:Edit
        public async Task<ServiceResult<BookResponse>> EditBookAsync(UpdatedBookRequest request)
        {
            ServiceResult<BookResponse> serviceResult = new ServiceResult<BookResponse>(System.Net.HttpStatusCode.BadRequest);
            //check if incoming req is null
            if (request == null
                || string.IsNullOrWhiteSpace(request.Title))
            {
                serviceResult.AddError("Book already exists");
                return serviceResult;
            }
            BookResponse response = null;
            //if same title is added again
            BookEntity bookEntity = await _bookRepository.GetByIdAsync(request.Id);
            if (bookEntity.Name.ToUpper().Equals(request.Title.ToUpper()) && bookEntity.Price.Equals(request.Price)
                && bookEntity.AuthorId.Equals(request.AuthorId) && bookEntity.GenreId.Equals(request.GenreId))
            {
                response = ServiceMappers.MaptToBookEntityResponse(bookEntity);
                return new ServiceResult<BookResponse>(response);
            }
            

            //bool AnyAsync =await  _bookRepository.AnyAsync(bookEntity);
            //if (AnyAsync)
            //{
            //    serviceResult.AddError("Title Already exists");
            //    return serviceResult;
            //}

            
            bookEntity.Name = request.Title;
            bookEntity.Price = request.Price;
            bookEntity.AuthorId = request.AuthorId;
            bookEntity.GenreId = request.GenreId;
            await _bookRepository.UpdateAsync(bookEntity);
            response = ServiceMappers.MaptToBookEntityResponse(bookEntity);
            return new ServiceResult<BookResponse>(response);



        }
        //Book:GetId
        public async Task<ServiceResult<BookResponse>> GetAsync(int id)
        {
            ServiceResult<BookResponse> serviceResult = new ServiceResult<BookResponse>(System.Net.HttpStatusCode.BadRequest);
            BookEntity bookEntity = await _bookRepository.GetByIdAsync(id);
            AuthorEntity authorEntity = await _authorRepository.GetByAuthorIdAsync(bookEntity.AuthorId);
            GenresEntity genreEntity = await _genreRepository.GetByIdAsync(bookEntity.GenreId);

            if (bookEntity == null && authorEntity == null && genreEntity==null)
            {
                serviceResult.AddError("Book not Found");
                return serviceResult;
            }
            

            BookResponse response = ServiceMappers.MaptToBookEntityResponse(bookEntity);
            response.AuthorName = authorEntity.AuthorName;
            response.GenreName = genreEntity.Name;
            return new ServiceResult<BookResponse>(response);
        }
        //Book:List
        public async Task<ServiceResult<List<BookResponse>>> GetBooksAsync()
        {
            List<BookEntity> bookEntities = await _bookRepository.GetBookEntitiesAsync();

            int[] genreIds = bookEntities.Select(x => x.GenreId).Distinct().ToArray();
            int[] authorIds= bookEntities.Select(x => x.AuthorId).Distinct().ToArray();

            List<GenresEntity> genresEntities = await _genreRepository.GetGenresByIdsAsync(genreIds);
            Dictionary<int, string> genreEntitiesDict = genresEntities.ToDictionary(a => a.GenreId, a => a.Name);
            
            List<AuthorEntity> authorEntities = await _authorRepository.GetAuthorByIdsAsync(authorIds);
            Dictionary<int, string> authorEntitiesDict = authorEntities.ToDictionary(a => a.AuthorId, a => a.AuthorName);
           


            List<BookResponse> bookResponses = ServiceMappers.MaptoBookResponseList(bookEntities);
            foreach(BookResponse item in bookResponses)
            {
                item.GenreName = genreEntitiesDict.GetValueOrDefault(item.GenreId);
                item.AuthorName = authorEntitiesDict.GetValueOrDefault(item.AuthorId);
            }
            return new ServiceResult<List<BookResponse>>(bookResponses);
        }

        
    }
}
