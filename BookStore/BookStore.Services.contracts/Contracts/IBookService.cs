using BookStore.WebContracts;
using BookStore.WebContracts.Book.Request;
using BookStore.WebContracts.Book.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Services.contracts.Contracts
{
    public interface IBookService
    {
        Task<ServiceResult<BookResponse>> CreateBookAsync(CreateBookRequest request);
        Task<ServiceResult<List<BookResponse>>> GetBooksAsync();
        Task<ServiceResult<BookResponse>> EditBookAsync(UpdatedBookRequest request);
        Task<ServiceResult<bool>> DeleteAsync(int id);
        Task<ServiceResult<BookResponse>> GetAsync(int id);
    }
}
