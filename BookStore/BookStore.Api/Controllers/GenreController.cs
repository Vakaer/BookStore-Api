using BookStore.Entities.Entities;
using BookStore.Services.contracts.Contracts;
using BookStore.Services.Services;
using BookStore.WebContracts.Author.Request;
using BookStore.WebContracts.Author.Response;
using BookStore.WebContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BookStore.WebContracts.Genres.Response;
using BookStore.WebContracts.Genres.Request;

namespace BookStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : BookStoreBaseController
    {
        private readonly IGenreServices _genreServices;
        private readonly ILogger<GenreController> _logger;

        public GenreController(IGenreServices genreServices
                                ,ILogger<GenreController> logger):base(logger)
        {
            _genreServices = genreServices;
            _logger = logger;
        }


        [HttpGet("{id:int}", Name = "Genre-Get")]
        public async Task<IActionResult> GetGenreByIdAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ServiceResult<GenreResponse> serviceResult = await _genreServices.GetGenreAsync(id);
            if (serviceResult.IsError)
            {
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
            return new ObjectResult(serviceResult.Result);
        }
        [HttpPost("Add", Name = "Genre-Add")]

        public async Task<IActionResult> AddGenreAsync([FromBody] CreateGenreRequest entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ServiceResult<GenreResponse> serviceResult = await _genreServices.CreateGenreAsync(entity);
            if (serviceResult.IsError)
            {
                return new StatusCodeResult(StatusCodes.Status401Unauthorized);
            }
            return new ObjectResult(serviceResult.Result);
        }

        [HttpPut("Edit")]
        public async Task<IActionResult> UpdateGenreAsync(UpdateGenreRequest entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ServiceResult<GenreResponse> serviceResult = await _genreServices.EditGenreAsync(entity);
            if (serviceResult.IsError)
            {
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
            return new ObjectResult(serviceResult.Result);


        }
        [HttpDelete("Delete", Name = "Genre-Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ServiceResult<bool> serviceResult = await _genreServices.DeleteAsync(id);
            if (serviceResult.IsError)
            {
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
            return new JsonResult(serviceResult.Result);

        }
        //Get:List
        [HttpGet("GetList", Name = "Genre-List")]
        public async Task<ActionResult<List<GenresEntity>>> GetListAsync()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ServiceResult<List<GenreResponse>> serviceResult = await _genreServices.GetGenresAsync();
            if (serviceResult.IsError)
            {
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
            return new ObjectResult(serviceResult.Result);
        }


    }
}
