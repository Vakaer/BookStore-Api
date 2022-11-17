using AutoMapper;
using BookStore.Entities.Entities;
using BookStore.Persistence.Contracts.Contracts;
using BookStore.Services.contracts.Contracts;
using BookStore.WebContracts;
using BookStore.WebContracts.Book.Request;
using BookStore.WebContracts.Book.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace BookStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : BookStoreBaseController
    {
        private readonly ILogger<BookController> _logger;
        private readonly IBookService _bookService;

        public BookController(ILogger<BookController> logger, 
                               IBookService bookService):base(logger)
        {
            _logger = logger;
            _bookService = bookService;
        }


        [HttpGet("{id:int}",Name ="Book-Get")]
        public async Task<IActionResult> GetBookAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ServiceResult<BookResponse> serviceResult = await _bookService.GetAsync(id);
            if (serviceResult.IsError)
            {
                return Respond(serviceResult);
            }
            return new ObjectResult(serviceResult.Result);
        }

        [HttpPost("Add",Name ="Book-Add")]

        public async Task<IActionResult> AddBookAsync([FromBody] CreateBookRequest entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var serviceResult = await _bookService.CreateBookAsync(entity);
            if (serviceResult.IsError)
            {
                return Respond(serviceResult);
            }
            return new JsonResult(serviceResult);

        }
        //Get:List
        [HttpGet("GetList")]
        public async Task<ActionResult<List<BookEntity>>> GetListAsync()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ServiceResult<List<BookResponse>> serviceResult = await _bookService.GetBooksAsync();
            if (serviceResult.IsError)
            {
                return Respond(serviceResult);
            }
            return new ObjectResult(serviceResult.Result);
        }
        
       

        [HttpPut("Edit")]
        public async Task<IActionResult> UpdateBookAsync(UpdatedBookRequest entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ServiceResult<BookResponse> serviceResult= await _bookService.EditBookAsync(entity);
            if (serviceResult.IsError)
            {
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
            return new ObjectResult(serviceResult.Result);


        }
        [HttpDelete("Delete",Name ="Book-Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ServiceResult<bool> serviceResult = await _bookService.DeleteAsync(id);
            if (serviceResult.IsError)
            {
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
            return new JsonResult(serviceResult.Result);

        }




        //public virtual ActionResult Respond(HttpStatusCode status)
        //{
        //    if (status == HttpStatusCode.BadRequest)
        //    {
        //        return BadRequest();
        //    }
        //    if (status == HttpStatusCode.Unauthorized)
        //    {
        //        return Unauthorized();
        //    }
        //    return new StatusCodeResult((int)status);
        //}
        //public virtual ActionResult Respond(HttpStatusCode status, List<ServiceError> errors)
        //{
        //    string content = JsonConvert.SerializeObject(errors);
        //    return new ContentResult
        //    {
        //        StatusCode = (int)status,
        //        Content = content,
        //        ContentType = "application/json"
        //    };
        //}
        //public virtual ActionResult Respond(ServiceResult serviceResults)
        //{
        //    ActionResult result;
        //    if (serviceResults.ErrorMessages.Any())
        //    {
        //        result = Respond(serviceResults.Status, serviceResults.ErrorMessages);
        //    }
        //    else
        //    {
        //        result = Respond(serviceResults);
        //    }
        //    return result;
        //}
    }
}
