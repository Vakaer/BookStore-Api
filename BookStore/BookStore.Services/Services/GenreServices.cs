using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Entities.Entities;
using BookStore.Persistence.Contracts.Contracts;
using BookStore.Services.contracts.Contracts;
using BookStore.Services.Mapper;
using BookStore.WebContracts;
using BookStore.WebContracts.Author.Response;
using BookStore.WebContracts.Book.Response;
using BookStore.WebContracts.Genres.Request;
using BookStore.WebContracts.Genres.Response;

namespace BookStore.Services.Services
{
    public class GenreServices : IGenreServices
    {
        private readonly IGenreRepository _genreRepository;

        public GenreServices(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<ServiceResult<GenreResponse>> CreateGenreAsync(CreateGenreRequest request)
        {
            ServiceResult<GenreResponse> serviceResult = new ServiceResult<GenreResponse>(System.Net.HttpStatusCode.BadRequest);
            if (request == null || string.IsNullOrEmpty(request.GenreName))
            {
                serviceResult.AddError("Invalid Request");
                return serviceResult;
            }
            GenresEntity genreEntity = await _genreRepository.GetByNameAsync(request.GenreName);
            if (genreEntity != null)
            {
                serviceResult.AddError("Name already exists");
                return serviceResult;
            }

            genreEntity = new GenresEntity
            {
                Name = request.GenreName
            };

            await _genreRepository.CreateGenreAsync(genreEntity);
            GenreResponse genreResponse = ServiceMappers.MaptoGenreEntityResponse(genreEntity);
            return new ServiceResult<GenreResponse>(genreResponse);
        }

        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            ServiceResult<bool> serviceResult = new ServiceResult<bool>(System.Net.HttpStatusCode.BadRequest);
            GenresEntity genreEntity = await _genreRepository.GetByIdAsync(id);
            if (genreEntity == null)
            {
                serviceResult.AddError("Author Not Found");
                return serviceResult;
            }
            await _genreRepository.DeleteAsync(genreEntity);
            return new ServiceResult<bool>(System.Net.HttpStatusCode.OK, true);
        }

        public async Task<ServiceResult<GenreResponse>> EditGenreAsync(UpdateGenreRequest request)
        {
            ServiceResult<GenreResponse> serviceResult = new ServiceResult<GenreResponse>(System.Net.HttpStatusCode.BadRequest);
            //check if incoming req is null
            if (request == null
                || string.IsNullOrWhiteSpace(request.Name))
            {
                serviceResult.AddError("Book already exists");
                return serviceResult;
            }
            GenreResponse response = null;
            //if same title is added again
            GenresEntity genreEntity = await _genreRepository.GetByIdAsync(request.Id);
            if (genreEntity.Name.ToUpper().Equals(request.Name.ToUpper()))
            {
                response = ServiceMappers.MaptoGenreEntityResponse(genreEntity);
                return new ServiceResult<GenreResponse>(response);
            }

            bool AnyAsync = await _genreRepository.AnyAsync(request.Name);
            if (AnyAsync)
            {
                serviceResult.AddError("Title Already exists");
                return serviceResult;
            }
            genreEntity.Name = request.Name;
            await _genreRepository.UpdateGenreAsync(genreEntity);
            response = ServiceMappers.MaptoGenreEntityResponse(genreEntity);
            return new ServiceResult<GenreResponse>(response);
        }

        public async Task<ServiceResult<GenreResponse>> GetGenreAsync(int id)
        {
            ServiceResult<GenreResponse> serviceResult = new ServiceResult<GenreResponse>(System.Net.HttpStatusCode.BadRequest);
            GenresEntity genreEntity = await _genreRepository.GetByIdAsync(id);
            if (genreEntity == null)
            {
                serviceResult.AddError("Genre not Found");
                return serviceResult;
            }
            GenreResponse response = ServiceMappers.MaptoGenreEntityResponse(genreEntity);
            return new ServiceResult<GenreResponse>(response);
        }

        public async Task<ServiceResult<List<GenreResponse>>> GetGenresAsync()
        {
            List<GenresEntity> genresEntities = await _genreRepository.GetGenreEntitiesAsync();
            List<GenreResponse> genresResponses = ServiceMappers.MaptoGenreResponseList(genresEntities);
            return new ServiceResult<List<GenreResponse>>(genresResponses);
        }
    }
}
