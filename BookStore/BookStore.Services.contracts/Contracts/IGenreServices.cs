using BookStore.WebContracts.Author.Request;
using BookStore.WebContracts.Author.Response;
using BookStore.WebContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.WebContracts.Genres.Response;
using BookStore.WebContracts.Genres.Request;

namespace BookStore.Services.contracts.Contracts
{
    public interface IGenreServices
    {
        Task<ServiceResult<GenreResponse>> CreateGenreAsync(CreateGenreRequest request);
        Task<ServiceResult<List<GenreResponse>>> GetGenresAsync();
        Task<ServiceResult<GenreResponse>> EditGenreAsync(UpdateGenreRequest request);
        Task<ServiceResult<bool>> DeleteAsync(int id);
        Task<ServiceResult<GenreResponse>> GetGenreAsync(int id);
    }
}
