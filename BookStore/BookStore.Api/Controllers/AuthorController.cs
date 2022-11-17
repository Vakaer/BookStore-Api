using BookStore.Entities.Entities;
using BookStore.Persistence.Contracts.Contracts;
using BookStore.Persistence.Persistence.Repositories;
using BookStore.Services.contracts.Contracts;
using BookStore.Services.Services;
using BookStore.WebContracts;
using BookStore.WebContracts.Author.Request;
using BookStore.WebContracts.Author.Response;
using BookStore.WebContracts.Book.Request;
using BookStore.WebContracts.Book.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Exchange.WebServices.Data;
using Newtonsoft.Json;
using System.Net;

namespace BookStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : BookStoreBaseController
    {


        private readonly IAuthorService _authorService;
        private readonly ILogger<AuthorController> _logger;

        public AuthorController(IAuthorService authorService,
            ILogger<AuthorController> logger) :base(logger)
        {
            _authorService = authorService;
            _logger = logger;
        }



        [HttpGet("{id:int}", Name = "Author-Get")]
        public async Task<IActionResult> GetAuthorByIdAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ServiceResult<AuthorResponse> serviceResult = await _authorService.GetAuthorAsync(id);
            if (serviceResult.IsError)
            {
                return Respond(serviceResult);
            }
            return new ObjectResult(serviceResult.Result);
        }
        [HttpPost("Add", Name = "Author-Add")]

        public async Task<IActionResult> AddAuthorAsync([FromBody]CreateAuthorRequest entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ServiceResult<AuthorResponse> serviceResult = await _authorService.CreateAuthorAsync(entity);
            if (serviceResult.IsError)
            {
                return Respond(serviceResult);
                //return new StatusCodeResult(StatusCodes.Status401Unauthorized);
            }
            return new ObjectResult(serviceResult.Result);
        }

        [HttpPut("Edit")]
        public async Task<IActionResult> UpdateAuthorAsync(UpdateAuthorRequest entity)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ServiceResult<AuthorResponse> serviceResult = await _authorService.EditAuthorAsync(entity);
            if (serviceResult.IsError)
            {
                //return new StatusCodeResult(StatusCodes.Status400BadRequest);
                return Respond(serviceResult);
            }
            return new ObjectResult(serviceResult.Result);


        }
        [HttpDelete("Delete", Name = "Author-Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ServiceResult<bool> serviceResult = await _authorService.DeleteAsync(id);
            if (serviceResult.IsError)
            {
                //return new StatusCodeResult(StatusCodes.Status400BadRequest);
                return Respond(serviceResult);
            }
            return new JsonResult(serviceResult.Result);

        }
        //Get:List
        [HttpGet("GetList",Name ="Author-List")]
        public async Task<ActionResult<List<AuthorEntity>>> GetListAsync()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ServiceResult<List<AuthorResponse>> serviceResult = await _authorService.GetAuthorsAsync();
            if (serviceResult.IsError)
            {
                return Respond(serviceResult);
            }
            return new ObjectResult(serviceResult.Result);
        }
    }





}




       

