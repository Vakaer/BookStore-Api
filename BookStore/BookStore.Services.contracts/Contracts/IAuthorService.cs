using BookStore.WebContracts.Book.Request;
using BookStore.WebContracts.Book.Response;
using BookStore.WebContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.WebContracts.Author.Response;
using BookStore.WebContracts.Author.Request;

namespace BookStore.Services.contracts.Contracts
{
    public interface IAuthorService
    {
        Task<ServiceResult<AuthorResponse>> CreateAuthorAsync(CreateAuthorRequest request);
        Task<ServiceResult<List<AuthorResponse>>> GetAuthorsAsync();
        Task<ServiceResult<AuthorResponse>> EditAuthorAsync(UpdateAuthorRequest request);
        Task<ServiceResult<bool>> DeleteAsync(int id);
        Task<ServiceResult<AuthorResponse>> GetAuthorAsync(int id);
    }
}
