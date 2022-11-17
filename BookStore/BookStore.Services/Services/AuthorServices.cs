using BookStore.Entities.Entities;
using BookStore.Persistence.Contracts.Contracts;
using BookStore.Services.contracts.Contracts;
using BookStore.Services.Mapper;
using BookStore.WebContracts;
using BookStore.WebContracts.Author.Request;
using BookStore.WebContracts.Author.Response;
using BookStore.WebContracts.Book.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Services.Services
{
    public class AuthorServices : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorServices(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        //Author: Create
        public async Task<ServiceResult<AuthorResponse>> CreateAuthorAsync(CreateAuthorRequest request)
        {
            ServiceResult<AuthorResponse> serviceResult = new ServiceResult<AuthorResponse>(System.Net.HttpStatusCode.BadRequest);
            //checking nullability of incoming request
            if(request == null || string.IsNullOrEmpty(request.Name))
            {
                serviceResult.AddError("Invalid Request");
                return serviceResult;
            }
            //checking if name already exist
            AuthorEntity authorEntity = await _authorRepository.GetByAuthorNameAsync(request.Name);
            if(authorEntity != null)
            {
                //add error
                serviceResult.AddError("Author Name already exists");
                return serviceResult;
            }
            authorEntity = new AuthorEntity
            {
                AuthorName = request.Name,
                
            };
            //Creating new author
            await _authorRepository.CreateAuthorAsync(authorEntity);
            AuthorResponse author = ServiceMappers.MaptoAuthorEntityResponse(authorEntity);
            return new ServiceResult<AuthorResponse>(author);
        }

        //Author: Delete
        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            ServiceResult<bool> serviceResult = new ServiceResult<bool>(System.Net.HttpStatusCode.BadRequest);
            AuthorEntity authorEntity= await _authorRepository.GetByAuthorIdAsync(id);
            if (authorEntity == null)
            {
                serviceResult.AddError("Author Not Found");
                return serviceResult;
            }
            await _authorRepository.DeleteAsync(authorEntity);
            return new ServiceResult<bool>(System.Net.HttpStatusCode.OK, true);
        }

        //Author: Edit
        public async Task<ServiceResult<AuthorResponse>> EditAuthorAsync(UpdateAuthorRequest request)
        {
            ServiceResult<AuthorResponse> serviceResult = new ServiceResult<AuthorResponse>(System.Net.HttpStatusCode.BadRequest);
            //check if incoming req is null
            if (request == null
                || string.IsNullOrWhiteSpace(request.AuthorName))
            {
                serviceResult.AddError("Book already exists");
                return serviceResult;
            }
            AuthorResponse response = null;
            //if same title is added again
            AuthorEntity authorEntity = await _authorRepository.GetByAuthorIdAsync(request.AuthorId);
            
            if (authorEntity!=null && authorEntity.AuthorName.ToUpper().Equals(request.AuthorName.ToUpper()))
            {
                response = ServiceMappers.MaptoAuthorEntityResponse(authorEntity);
                return new ServiceResult<AuthorResponse>(response);
            }

            bool AnyAsync = await _authorRepository.AnyAsync(request.AuthorName);
            if (AnyAsync)
            {
                serviceResult.AddError("Title Already exists");
                return serviceResult;
            }
            authorEntity.AuthorName = request.AuthorName;
            await _authorRepository.UpdateAuthorAsync(authorEntity);
            response = ServiceMappers.MaptoAuthorEntityResponse(authorEntity);
            return new ServiceResult<AuthorResponse>(response);
        }
        public async Task<ServiceResult<AuthorResponse>> GetAuthorAsync(int id)
        {
            ServiceResult<AuthorResponse> serviceResult = new ServiceResult<AuthorResponse>(System.Net.HttpStatusCode.BadRequest);
            AuthorEntity authorEntity = await _authorRepository.GetByAuthorIdAsync(id);
            if (authorEntity == null)
            {
                serviceResult.AddError("Author not Found");
                return serviceResult;
            }
            AuthorResponse response = ServiceMappers.MaptoAuthorEntityResponse(authorEntity);
            return new ServiceResult<AuthorResponse>(response);
        }

        //Author: getList
        public async Task<ServiceResult<List<AuthorResponse>>> GetAuthorsAsync()
        {
            List<AuthorEntity> authorEntities = await _authorRepository.GetAuthorEntitiesAsync();
            List<AuthorResponse> authorResponses = ServiceMappers.MaptoAuthorResponseList(authorEntities);
            return new ServiceResult<List<AuthorResponse>>(authorResponses);
            
        }
    }
}
